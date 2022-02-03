using AutoMapper;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Models;
using Visma.Bootcamp.eShop.ApplicationCore.Exceptions;
using Visma.Bootcamp.eShop.ApplicationCore.Infrastructure;
using Visma.Bootcamp.eShop.ApplicationCore.Services.Interfaces;

namespace Visma.Bootcamp.eShop.ApplicationCore.Services
{
    public class ProductService : IProductService
    {
        private readonly CacheManager _cache;
        private readonly IMapper _mapper;

        public ProductService(CacheManager cache, IMapper mapper)
        {
            _cache = cache;
            _mapper = mapper;
        }

        public async Task<ProductDto> CreateAsync(Guid catalogId, ProductModel model, CancellationToken ct = default)
        {
            var products = _cache.Get<ProductDto>();
            if (products.Any(x => x.Name == model.Name))
            {
                throw new ConflictException($"Product with name {model.Name} already exists");
            }

            var product = _mapper.Map<ProductDto>(model);
            product.PublicId = Guid.NewGuid();
            _cache.Set(product);
            return product;
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
