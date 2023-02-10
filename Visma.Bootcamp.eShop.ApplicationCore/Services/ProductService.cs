using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Visma.Bootcamp.eShop.ApplicationCore.Database;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Domain;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Models;
using Visma.Bootcamp.eShop.ApplicationCore.Exceptions;
using Visma.Bootcamp.eShop.ApplicationCore.Infrastructure;
using Visma.Bootcamp.eShop.ApplicationCore.Services.Interfaces;

namespace Visma.Bootcamp.eShop.ApplicationCore.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationContext _context;
        private readonly Infrastructure.CacheManager _cache;
        private readonly IMapper _mapper;

        public ProductService(Infrastructure.CacheManager cache, IMapper mapper, ApplicationContext context)
        {
            _cache = cache;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ProductDto> CreateAsync(Guid catalogId, ProductModel model, CancellationToken ct = default)
        {
            Catalog catalog = await _context.Catalogs
                .AsNoTracking()
                .SingleOrDefaultAsync(c => c.PublicId == catalogId, ct);
            if (catalog == null)
            {
                throw new NotFoundException($"Catalog with ID: {catalogId} doesn't exist");
            }

            Product duplicate = await _context.Products
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Name == model.Name, ct);
            if (duplicate != null)
            {
                throw new ConflictException($"Product with name {model.Name} already exists");
            }

            Product product = model.ToDomain();
            product.PublicId = Guid.NewGuid();
            product.CatalogId = catalog.Id;

            await _context.Products.AddAsync(product, ct);
            await _context.SaveChangesAsync(ct);

            ProductDto dto = product.ToDto();
            return dto;
        }

        public async Task DeleteAsync(Guid productId, CancellationToken ct = default)
        {
            var product = _cache.Get<ProductDto>(productId);
            if (product == null)
            {
                throw new NotFoundException($"Product with ID: {productId} doesn't exist");
            }

            _cache.Remove<ProductDto>(productId);
        }

        public async Task<ProductDto> GetAsync(Guid productId, CancellationToken ct = default)
        {
            var product = _cache.Get<ProductDto>(productId);
            if (product == null)
            {
                throw new NotFoundException($"Product with ID: {productId} doesn't exist");
            }

            return product;
        }

        public async Task<ProductDto> UpdateAsync(Guid productId, ProductModel model, CancellationToken ct = default)
        {
            var product = _cache.Get<ProductDto>(productId);
            if (product == null)
            {
                throw new NotFoundException($"Product with ID: {productId} doesn't exist");
            }

            var products = _cache.Get<ProductDto>();
            if (products.Any(x => x.Name == model.Name && x.Id != productId))
            {
                throw new ConflictException($"Product with name {model.Name} already exists");
            }

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            _cache.Set(product);
            return product;
        }
    }
}
