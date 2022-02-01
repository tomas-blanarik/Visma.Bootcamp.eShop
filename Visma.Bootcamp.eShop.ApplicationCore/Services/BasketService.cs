using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Models;
using Visma.Bootcamp.eShop.ApplicationCore.Services.Interfaces;

namespace Visma.Bootcamp.eShop.ApplicationCore.Services
{
    public class BasketService : IBasketService
    {
        public Task<BasketDto> AddItemAsync(Guid basketId, BasketItemModel model, CancellationToken ct = default)
        {
            throw new NotImplementedException("Not implemented");
        }

        public Task DeleteItemAsync(Guid basketId, Guid itemId, CancellationToken ct = default)
        {
            throw new NotImplementedException("Not implemented");
        }

        public async Task<BasketDto> GetAsync(Guid basketId, CancellationToken ct = default)
        {
            return new BasketDto
            {
                BasketId = Guid.NewGuid(),
                Items = new List<ProductDto>
                {
                    new ProductDto
                    {
                        PublicId = Guid.NewGuid(),
                        Name = "test product #1",
                        Description = "test decription #1",
                        Price = 128.34M
                    },
                    new ProductDto
                    {
                        PublicId = Guid.NewGuid(),
                        Name = "test product #2",
                        Description = "test description #2",
                        Price = 25.99M
                    },
                    new ProductDto
                    {
                        PublicId = Guid.NewGuid(),
                        Name = "test product #3",
                        Description = "test description #3",
                        Price = 49.99M
                    }
                }
            };
        }

        public Task UpdateAsync(Guid basketId, BasketModel model, CancellationToken ct = default)
        {
            throw new NotImplementedException("Not implemented");
        }
    }
}
