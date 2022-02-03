using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Models;
using Visma.Bootcamp.eShop.ApplicationCore.Exceptions;
using Visma.Bootcamp.eShop.ApplicationCore.Infrastructure;
using Visma.Bootcamp.eShop.ApplicationCore.Services.Interfaces;

namespace Visma.Bootcamp.eShop.ApplicationCore.Services
{
    internal class CatalogService : ICatalogService
    {
        private readonly CacheManager _cache;
        private readonly IMapper _mapper;

        public CatalogService(CacheManager cache, IMapper mapper)
        {
            _cache = cache;
            _mapper = mapper;
        }

        public CatalogDto Create(CatalogModel model)
        {
            var catalogs = _cache.Get<CatalogDto>();
            if (catalogs.Any(x => x.Name == model.Name))
            {
                throw new ConflictException($"Catalog with name {model.Name} already exists");
            }

            var catalogDto = _mapper.Map<CatalogDto>(model);
            catalogDto.CatalogId = Guid.NewGuid();
            _cache.Set(catalogDto);
            return catalogDto;
        }

        public void Delete(Guid catalogId)
        {
            var catalog = _cache.Get<CatalogDto>(catalogId);
            if (catalog == null)
            {
                throw new NotFoundException($"Catalog with ID: {catalogId} doesn't exist");
            }

            _cache.Remove<CatalogDto>(catalogId);
        }

        public List<CatalogDto> Get()
        {
            return _cache.Get<CatalogDto>();
        }

        public CatalogDto Get(Guid catalogId)
        {
            var catalog = _cache.Get<CatalogDto>(catalogId);
            if (catalog == null)
            {
                throw new NotFoundException($"Catalog with ID: {catalogId} doesn't exist");
            }

            return catalog;
        }

        public CatalogDto Update(Guid catalogId, CatalogModel model)
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
