using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Models;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Models.Errors;
using Visma.Bootcamp.eShop.ApplicationCore.Services.Interfaces;

namespace Visma.Bootcamp.eShop.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _service;

        public BasketController(IBasketService service)
        {
            _service = service;
        }

        public const string GetBasketRouteName = "getbasket";

        [HttpPost("{basket_id}/items")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BasketDto))]
        [SwaggerOperation(
            summary: "Add product to the basket",
            description: "Associate given productId with actual basket of the user, which is represented by basketId",
            OperationId = "AddToBasket",
            Tags = new[] { "Basket Management" })]
        public IActionResult AddToBasket([Required, FromRoute(Name = "basket_id")] Guid? basketId,
                                         [Bind, FromBody] BasketItemModel model)
        {
            var basket = _service.AddItem(basketId.Value, model);
            return CreatedAtAction(
                GetBasketRouteName,
                new { basket_id = basket.Id },
                basket);
        }

        [HttpGet("{basket_id}", Name = GetBasketRouteName)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasketDto))]
        [SwaggerOperation(
            summary: "Retrieve basket",
            description: "Returns basket by given basketId with all products",
            OperationId = "GetBasket",
            Tags = new[] { "Basket API" })]
        public IActionResult GetBasket([Required, FromRoute(Name = "basket_id")] Guid? basketId)
        {
            // ed7365c1-b7b2-4751-a079-71cbd08d2b8d
            return Ok(_service.Get(basketId.Value));
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
        public IActionResult UpdateBasket([Required, FromRoute(Name = "basket_id")] Guid? basketId,
                                          [FromBody, Bind] BasketModel model)
        {
            _service.Update(basketId.Value, model);
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
        public IActionResult DeleteFromBasket([Required, FromRoute(Name = "basket_id")] Guid? basketId,
                                              [Required, FromRoute(Name = "item_id")] Guid? itemId)
        {
            _service.DeleteItem(basketId.Value, itemId.Value);
            return NoContent();
        }
    }
}
