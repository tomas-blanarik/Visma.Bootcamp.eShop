using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Models;
using Visma.Bootcamp.eShop.ApplicationCore.Services.Interfaces;

namespace Visma.Bootcamp.eShop.ApplicationCore.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;

        public CatalogService(IMemoryCache cache, IMapper mapper)
        {
            _cache = cache;
            _mapper = mapper;
        }

        public Task<CatalogDto> CreateAsync(CatalogModel model, CancellationToken ct = default)
        {
            CatalogDto catalog = _mapper.Map<CatalogDto>(model);
            catalog.PublicId = Guid.NewGuid();
            _cache.Set(catalog.PublicId, catalog, TimeSpan.FromHours(24));
        }

        public Task DeleteAsync(Guid catalogId, CancellationToken ct = default)
        {
            throw new NotImplementedException("Not implemented");
        }

        public async Task<List<CatalogDto>> GetAllAsync(CancellationToken ct = default)
        {
            return new List<CatalogDto>
            {
                new CatalogDto
                {
                    PublicId = Guid.NewGuid(),
                    Name = "White electronics",
                    Description = "All white electronics in the shop",
                    Products = new List<ProductDto>
                    {
                        new ProductDto
                        {
                            PublicId = Guid.NewGuid(),
                            Name = "product #1",
                            Description = "description of product #1",
                            Price = 49.99M
                        },
                        new ProductDto
                        {
                            PublicId = Guid.NewGuid(),
                            Name = "product #2",
                            Description = "description of product #2",
                            Price = 59.99M
                        },
                        new ProductDto
                        {
                            PublicId = Guid.NewGuid(),
                            Name = "product #3",
                            Description = "description of product #3",
                            Price = 19.99M
                        }
                    }
                },
                new CatalogDto
                {
                    PublicId = Guid.NewGuid(),
                    Name = "Black electronics",
                    Description = "All black electronics in the shop",
                    Products = new List<ProductDto>
                    {
                        new ProductDto
                        {
                            PublicId = Guid.NewGuid(),
                            Name = "product #4",
                            Description = "description of product #4",
                            Price = 100M
                        },
                        new ProductDto
                        {
                            PublicId = Guid.NewGuid(),
                            Name = "product #5",
                            Description = "description of product #5",
                            Price = 9.99M
                        },
                    }
                },
                new CatalogDto
                {
                    PublicId = Guid.NewGuid(),
                    Name = "Computers",
                    Description = "All computers in the shop - gaming, work, stations, Apple",
                    Products = new List<ProductDto>
                    {
                        new ProductDto
                        {
                            PublicId = Guid.NewGuid(),
                            Name = "product #6",
                            Description = "description of product #6",
                            Price = 12.59M
                        },
                        new ProductDto
                        {
                            PublicId = Guid.NewGuid(),
                            Name = "product #7",
                            Description = "description of product #7",
                            Price = 0M
                        },
                    }
                }
            };
        }

        public Task<CatalogDto> GetAsync(Guid catalogId, CancellationToken ct = default)
        {
            throw new NotImplementedException("Not implemented");
        }

        public Task<CatalogDto> UpdateAsync(Guid catalogId, CatalogModel model, CancellationToken ct = default)
        {
            throw new NotImplementedException("Not implemented");
        }
    }
}
