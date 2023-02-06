using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Models;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Models.Errors;

namespace Visma.Bootcamp.eShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet("{product_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [SwaggerOperation(
            summary: "Create product in existing catalog",
            description: "Create product in the database and associate it with catalog",
            OperationId = "CreateProduct",
            Tags = new[] { "Product API" })]
        public IActionResult GetProduct(
            [Required, FromRoute(Name = "product_id")] Guid? productId,
            CancellationToken ct)
        {
            var productDto = new ProductDto
            {
                ProductId = Guid.NewGuid(),
                Name = "product #1",
                Description = "product description #2",
                Price = 123.99M
            };

            return Ok(productDto);
        }

        [HttpPut("{product_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ConflictError))]
        [SwaggerOperation(
            summary: "Update product",
            description: "Update product in the database",
            OperationId = "UpdateProduct",
            Tags = new[] { "Product Management" })]
        public IActionResult UpdateProduct(
            [Required, FromRoute(Name = "product_id")] Guid? productId,
            [Bind, FromBody] ProductModel model,
            CancellationToken ct)
        {
            return BadRequest("Not implemented");
        }

        [HttpDelete("{product_id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [SwaggerOperation(
            summary: "Delete product",
            description: "Delete product from the database",
            OperationId = "DeleteProduct",
            Tags = new[] { "Product Management" })]
        public IActionResult DeleteProduct(
            [Required, FromRoute(Name = "product_id")] Guid? productId,
            CancellationToken ct)
        {
            return BadRequest("Not implemented");
        }
    }
}
