using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Models;
using Visma.Bootcamp.eShop.ApplicationCore.Exceptions;
using Visma.Bootcamp.eShop.ApplicationCore.Services.Interfaces;

namespace Visma.Bootcamp.eShop.ApplicationCore.Services
{
    public class BasketService : IBasketService
    {
        private readonly IMemoryCache _cache;

        public BasketService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<BasketDto> AddItemAsync(Guid basketId, BasketItemModel model, CancellationToken ct = default)
        {
            BasketDto basket = await GetBasketAsync(basketId);

            var basketItem = basket.Items.SingleOrDefault(x => x.Product.PublicId == model.ProductId);
            if (basketItem != null)
            {
                basketItem.Quantity += model.Quantity;
            }
            else
            {
                basket.Items.Add(new BasketItemDto
                {
                    Quantity = model.Quantity,
                    Product = new ProductDto
                    {
                        PublicId = model.ProductId
                    }
                });
            }

            SetBasket(basket);
            return basket;
        }

        public async Task DeleteItemAsync(Guid basketId, Guid itemId, CancellationToken ct = default)
        {
            BasketDto basket = await GetBasketAsync(basketId);
            var basketItem = basket.Items.SingleOrDefault(x => x.Product.PublicId == itemId);
            if (basketItem == null)
            {
                throw new EntityNotFoundException<BasketItemDto>(itemId);
            }

            basket.Items.Remove(basketItem);
            SetBasket(basket);
        }

        public async Task<BasketDto> GetAsync(Guid basketId, CancellationToken ct = default)
        {
            return await GetBasketAsync(basketId);
        }

        public async Task UpdateAsync(Guid basketId, BasketModel model, CancellationToken ct = default)
        {
            BasketDto basket = await GetBasketAsync(basketId);
            foreach (var item in model.Items)
            {
                var basketItem = basket.Items.SingleOrDefault(x => x.Product.PublicId == item.ProductId);
                if (basketItem == null)
                {
                    //basket.Items.Add(new BasketItemDto
                    //{
                    //    Product = new ProductDto
                    //    {
                    //        PublicId = item.ProductId
                    //    },
                    //    Quantity = item.Quantity
                    //});
                    throw new EntityNotFoundException<BasketItemDto>(item.ProductId);
                }
                else
                {
                    if (item.Quantity > 0)
                    {
                        basketItem.Quantity = item.Quantity;
                    }
                    else
                    {
                        basket.Items.Remove(basketItem);
                    }
                }
            }

            SetBasket(basket);
        }

        private async Task<BasketDto> GetBasketAsync(Guid basketId)
        {
            return await _cache.GetOrCreateAsync(basketId, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24);
                return Task.FromResult(new BasketDto
                {
                    BasketId = basketId,
                    Items = new List<BasketItemDto>()
                });
            });
        }

        private void SetBasket(BasketDto basket)
        {
            _cache.Set(basket.BasketId, basket, absoluteExpirationRelativeToNow: TimeSpan.FromHours(24));
        }
    }
}
