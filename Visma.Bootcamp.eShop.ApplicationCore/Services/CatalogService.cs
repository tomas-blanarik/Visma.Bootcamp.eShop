using EFCoreSecondLevelCacheInterceptor;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
    public class CatalogService : ICatalogService
    {
        private readonly ApplicationContext _context;

        public CatalogService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<CatalogDto> CreateAsync(CatalogModel model,
                                                  CancellationToken ct = default)
        {
            Catalog duplicate = await _context.Catalogs
                .AsNoTracking()
                .SingleOrDefaultAsync(c => c.Name == model.Name, ct);

            if (duplicate != null)
            {
                throw new ConflictException($"Catalog with name {model.Name} already exists");
            }

            Catalog catalog = model.ToDomain();
            catalog.PublicId = Guid.NewGuid();

            await _context.Catalogs.AddAsync(catalog, ct);
            await _context.SaveChangesAsync(ct);

            CatalogDto catalogDto = catalog.ToDto();
            return catalogDto;
        }

        public async Task DeleteAsync(Guid catalogId,
                                      CancellationToken ct = default)
        {
            Catalog catalog = await _context.Catalogs
                .SingleOrDefaultAsync(c => c.PublicId == catalogId, ct);
            if (catalog == null)
            {
                throw new NotFoundException($"Catalog with ID: {catalogId} doesn't exist");
            }

            _context.Catalogs.Remove(catalog);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<List<CatalogDto>> GetAllAsync(CancellationToken ct = default)
        {
            List<Catalog> list = await _context.Catalogs
                .AsNoTracking()
                .Cacheable()
                .ToListAsync(ct);

            List<CatalogDto> dtos = list.Select(c => c.ToDto()).ToList();
            return dtos;
        }

        public async Task<CatalogDto> GetAsync(Guid catalogId,
                                               CancellationToken ct = default)
        {
            Catalog catalog = await _context.Catalogs
                .AsNoTracking()
                .Include(c => c.Products)
                .Cacheable()
                .SingleOrDefaultAsync(c => c.PublicId == catalogId, ct);

            if (catalog == null)
            {
                throw new NotFoundException($"Catalog with ID: {catalogId} doesn't exist");
            }

            CatalogDto dto = catalog.ToDto(includeProducts: true);
            return dto;
        }

        public async Task<CatalogDto> UpdateAsync(Guid catalogId,
                                                  CatalogModel model,
                                                  CancellationToken ct = default)
        {
            Catalog catalog = await _context.Catalogs
                .SingleOrDefaultAsync(x => x.PublicId == catalogId, ct);
            if (catalog == null)
            {
                throw new NotFoundException($"Catalog with ID: {catalogId} doesn't exist");
            }

            Catalog duplicate = await _context.Catalogs
                .AsNoTracking()
                .SingleOrDefaultAsync(c => c.Name == model.Name && c.PublicId != catalogId, ct);

            if (duplicate != null)
            {
                throw new ConflictException($"Catalog with name {model.Name} already exists");
            }

            catalog.Name = model.Name;
            catalog.Description = model.Description;

            _context.Catalogs.Update(catalog);
            await _context.SaveChangesAsync(ct);
            CatalogDto dto = catalog.ToDto();
            return dto;
        }
    }
}
