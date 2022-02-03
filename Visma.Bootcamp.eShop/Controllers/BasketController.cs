using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Models;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Models.Errors;
using Visma.Bootcamp.eShop.ApplicationCore.Exceptions;
using Visma.Bootcamp.eShop.ApplicationCore.Infrastructure;
using Visma.Bootcamp.eShop.ApplicationCore.Services.Interfaces;

namespace Visma.Bootcamp.eShop.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly CacheManager _cache;
        private readonly IBasketService _service;

        public BasketController(IBasketService service)
        {
            _service = service;
        }

        public BasketController(CacheManager cache)
        {
            _cache = cache;
        }

        public const string GetBasketRouteName = "getbasket";

        [HttpPost("{basket_id}/items")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BasketDto))]
        [SwaggerOperation(
            summary: "Add product to the basket",
            description: "Associate given productId with actual basket of the user, which is represented by basketId",
            OperationId = "AddToBasket",
            Tags = new[] { "Basket Management" })]
        public async Task<IActionResult> AddToBasketAsync(
            [Required, FromRoute(Name = "basket_id")] Guid? basketId,
            [Bind, FromBody] BasketItemModel model,
            CancellationToken ct)
        {
            try
            {
                var basket = _service.AddItem(basketId.Value, model);
                return CreatedAtAction(
                    GetBasketRouteName,
                    new { basket_id = basket.Id },
                    basket);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (UnprocessableEntityException e)
            {
                return UnprocessableEntity(e.Message);
            }
        }

        [HttpGet("{basket_id}", Name = GetBasketRouteName)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasketDto))]
        [SwaggerOperation(
            summary: "Retrieve basket",
            description: "Returns basket by given basketId with all products",
            OperationId = "GetBasket",
            Tags = new[] { "Basket API" })]
        public async Task<IActionResult> GetBasketAsync(
            [Required, FromRoute(Name = "basket_id")] Guid? basketId,
            CancellationToken ct)
        {
            // ed7365c1-b7b2-4751-a079-71cbd08d2b8d
            var (_, basket) = GetBasket(basketId.Value, true);
            return Ok(basket);
        }

        [HttpPut("{basket_id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestError))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(UnprocessableEntityError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [SwaggerOperation(
            summary: "Update basket",
            description: "Update basket based on given basketId with items collection and their quantities",
            OperationId = "UpdateBasket",
            Tags = new[] { "Basket Management" })]
        public async Task<IActionResult> UpdateBasketAsync(
            [Required, FromRoute(Name = "basket_id")] Guid? basketId,
            [FromBody, Bind] BasketModel model,
            CancellationToken ct)
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
                return BadRequest("Quantity must be a number between 1 and 20");
            }

            // get basket and check if it exists
            var (basketExists, basket) = GetBasket(basketId.Value);
            if (!basketExists)
            {
                return NotFound($"Basket with ID: {basketId} not found");
            }

            foreach (BasketItemModel item in model.Items)
            {
                var basketItem = basket.Items
                    .SingleOrDefault(x => x.Product.Id == item.ProductId);
                if (basketItem == null)
                {
                    return NotFound($"Product with ID: {item.ProductId} not found in the basket");
                }

                basketItem.Quantity = item.Quantity;
            }

            return NoContent();
        }

        [HttpDelete("{basket_id}/items/{item_id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [SwaggerOperation(
            summary: "Remove item from basket",
            description: "Remove item from the basket by given basketId and itemId",
            OperationId = "RemoveItemFromBasket",
            Tags = new[] { "Basket Management" })]
        public async Task<IActionResult> DeleteFromBasketAsync(
            [Required, FromRoute(Name = "basket_id")] Guid? basketId,
            [Required, FromRoute(Name = "item_id")] Guid? itemId,
            CancellationToken ct)
        {
            // co ak basket neexistuje
            // - hodim not found
            // ak item existuje
            // - zmazem ho
            // ak neexsituje 
            // - hodim not found
            // ak vsetko prebehlo OK, ulozim spat do cache

            var (basketExists, basket) = GetBasket(basketId.Value);
            if (!basketExists)
            {
                return NotFound($"Basket with ID: {basketId} not found");
            }

            var basketItem = basket.Items
                .SingleOrDefault(x => x.Product.Id == itemId);
            if (basketItem == null)
            {
                return NotFound($"Product with ID: {itemId} is not present in the basket");
            }

            basket.Items.Remove(basketItem);
            _cache.Set(basket);
            return NoContent();
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
