using AutoMapper;
using System;
using System.Collections.Generic;
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
    public class CatalogService : ICatalogService
    {
        private readonly CacheManager _cache;
        private readonly IMapper _mapper;

        public CatalogService(CacheManager cache, IMapper mapper)
        {
            _cache = cache;
            _mapper = mapper;
        }

        public async Task<CatalogDto> CreateAsync(
            CatalogModel model,
            CancellationToken ct = default)
        {
            var catalogs = _cache.Get<CatalogDto>();
            if (catalogs.Any(x => x.Name == model.Name))
            {
                throw new ConflictException($"Catalog with name {model.Name} already exists");
            }

            var catalogDto = _mapper.Map<CatalogDto>(model);
            catalogDto.PublicId = Guid.NewGuid();
            _cache.Set(catalogDto);
            return catalogDto;
        }

        public async Task DeleteAsync(
            Guid catalogId,
            CancellationToken ct = default)
        {
            var catalog = _cache.Get<CatalogDto>(catalogId);
            if (catalog == null)
            {
                throw new NotFoundException($"Catalog with ID: {catalogId} doesn't exist");
            }

            _cache.Remove<CatalogDto>(catalogId);
        }

        public async Task<List<CatalogDto>> GetAllAsync(CancellationToken ct = default)
        {
            return _cache.Get<CatalogDto>();
        }

        public async Task<CatalogDto> GetAsync(
            Guid catalogId,
            CancellationToken ct = default)
        {
            var catalog = _cache.Get<CatalogDto>(catalogId);
            if (catalog == null)
            {
                throw new NotFoundException($"Catalog with ID: {catalogId} doesn't exist");
            }

            return catalog;
        }

        public async Task<CatalogDto> UpdateAsync(
            Guid catalogId,
            CatalogModel model,
            CancellationToken ct = default)
        {
            var catalogs = _cache.Get<CatalogDto>();
            if (catalogs.Any(x => x.Name == model.Name && x.Id != catalogId))
            {
                throw new ConflictException($"Catalog with name {model.Name} already exists");
            }

            var catalog = catalogs.SingleOrDefault(x => x.Id == catalogId);
            if (catalog == null)
            {
                throw new NotFoundException($"Catalog with ID: {catalogId} doesn't exist");
            }

            catalog.Name = model.Name;
            catalog.Description = model.Description;
            _cache.Set(catalog);
            return catalog;
        }
    }
}
