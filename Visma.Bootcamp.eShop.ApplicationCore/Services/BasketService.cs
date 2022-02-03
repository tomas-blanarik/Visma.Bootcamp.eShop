using System;
using System.Linq;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Models;
using Visma.Bootcamp.eShop.ApplicationCore.Exceptions;
using Visma.Bootcamp.eShop.ApplicationCore.Infrastructure;
using Visma.Bootcamp.eShop.ApplicationCore.Services.Interfaces;

namespace Visma.Bootcamp.eShop.ApplicationCore.Services
{
    internal class BasketService : IBasketService
    {
        private readonly CacheManager _cache;

        public BasketService(CacheManager cache)
        {
            _cache = cache;
        }

        public BasketDto AddItem(Guid basketId, BasketItemModel model)
        {
            /*
            1. checknem ci basket existuje
            - ak nie, vytvorim ho
            2*- skontrolovat ci product existuje (out of scope)
            2. skontrolujem ci productId uz existuje v kosiku
            - ak nie, tak ho vytvorim
            - ak ano, zvysim quantity
            3. ulozim basket spat do pamate
            Errory:
            - ak je quantita <= 0 > 20 vratim 
            - 400 bad request, 409 conflict, 422 unprocessable entity
            */
            const int maxItems = 20;
            if (model.Quantity <= 0 || model.Quantity > maxItems)
            {
                throw new BadRequestException("Quantity must be a number between 0 and 20");
                //return BadRequest("Quantity must be a number between 0 and 20");
            }

            var basket = _cache.Get<BasketDto>(basketId);
            if (basket == null)
            {
                basket = new BasketDto { BasketId = basketId };
            }

            var basketItem = basket.Items
                .SingleOrDefault(x => x.Product.Id == model.ProductId);
            if (basketItem != null)
            {
                basketItem.Quantity += model.Quantity;
                if (basketItem.Quantity > maxItems)
                {
                    throw new UnprocessableEntityException("You can order maximum of 20 items");
                    //return UnprocessableEntity("You can order maximum of 20 items");
                }
            }
            else
            {
                basket.Items.Add(new BasketItemDto
                {
                    Product = new ProductDto
                    {
                        ProductId = model.ProductId.Value,
                    },
                    Quantity = model.Quantity,
                });
            }

            // save back to memory
            _cache.Set(basket);

            return basket;
        }

        public void DeleteItem(Guid basketId, Guid itemId)
        {
            throw new NotImplementedException();
        }

        public BasketDto Get(Guid basketId)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid basketId, BasketModel model)
        {
            throw new NotImplementedException();
        }
    }
}
