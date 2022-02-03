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

            var (_, basket) = GetBasket(basketId, true);

            var basketItem = basket.Items
                .SingleOrDefault(x => x.Product.Id == model.ProductId);
            if (basketItem != null)
            {
                basketItem.Quantity += model.Quantity;
                if (basketItem.Quantity > maxItems)
                {
                    throw new UnprocessableEntityException("You can order maximum of 20 items");
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
            // co ak basket neexistuje
            // - hodim not found
            // ak item existuje
            // - zmazem ho
            // ak neexsituje 
            // - hodim not found
            // ak vsetko prebehlo OK, ulozim spat do cache

            var (basketExists, basket) = GetBasket(basketId);
            if (!basketExists)
            {
                throw new NotFoundException($"Basket with ID: {basketId} not found");
            }

            var basketItem = basket.Items
                .SingleOrDefault(x => x.Product.Id == itemId);
            if (basketItem == null)
            {
                throw new NotFoundException($"Product with ID: {itemId} is not present in the basket");
            }

            basket.Items.Remove(basketItem);
            _cache.Set(basket);
        }

        public BasketDto Get(Guid basketId)
        {
            var (_, basket) = GetBasket(basketId, true);
            return basket;
        }

        public void Update(Guid basketId, BasketModel model)
        {
            // 1. ak basket neexistuje co s tym?
            // 2. preforeachujem itemy a zistim
            // - ci item existuje?
            //   - ak ano updatnem quantity
            //   - ak nie, co potom?
            // 3. ulozim kosik

            // check if item quantities are within range
            if (model.Items.Any(x => x.Quantity <= 0 || x.Quantity > 20))
            {
                throw new BadRequestException("Quantity must be a number between 1 and 20");
            }

            // get basket and check if it exists
            var (basketExists, basket) = GetBasket(basketId);
            if (!basketExists)
            {
                throw new NotFoundException($"Basket with ID: {basketId} not found");
            }

            foreach (BasketItemModel item in model.Items)
            {
                var basketItem = basket.Items
                    .SingleOrDefault(x => x.Product.Id == item.ProductId);
                if (basketItem == null)
                {
                    throw new NotFoundException($"Product with ID: {item.ProductId} not found in the basket");
                }

                basketItem.Quantity = item.Quantity;
            }
        }

        private (bool, BasketDto) GetBasket(Guid basketId, bool createBasket = false)
        {
            BasketDto basket = _cache.Get<BasketDto>(basketId);
            if (basket == null)
            {
                if (!createBasket) return (false, null);
                else
                {
                    basket = new BasketDto { BasketId = basketId };
                    _cache.Set(basket);
                }
            }

            return (true, basket);
        }
    }
}
