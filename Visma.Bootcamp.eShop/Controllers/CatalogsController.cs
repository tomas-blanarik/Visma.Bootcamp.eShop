using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Models;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Models.Errors;

namespace Visma.Bootcamp.eShop.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CatalogsController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CatalogDto>))]
        [SwaggerOperation(
            summary: "Retrieve all catalogs from the system",
            description: "Return all catalogs with their products",
            OperationId = "GetCatalogs",
            Tags = new[] { "Catalog API" })]
        public async Task<IActionResult> GetCatalogsAsync(CancellationToken ct)
        {
            var list = new List<CatalogDto>
            {
                new CatalogDto
                {
                    CatalogId = Guid.NewGuid(),
                    Name = "White electronics",
                    Description = "All white electronics in the shop",
                    Products = new List<ProductDto>
                    {
                        new ProductDto
                        {
                            ProductId = Guid.NewGuid(),
                            Name = "product #1",
                            Description = "description of product #1",
                            Price = 49.99M
                        },
                        new ProductDto
                        {
                            ProductId = Guid.NewGuid(),
                            Name = "product #2",
                            Description = "description of product #2",
                            Price = 59.99M
                        },
                        new ProductDto
                        {
                            ProductId = Guid.NewGuid(),
                            Name = "product #3",
                            Description = "description of product #3",
                            Price = 19.99M
                        }
                    }
                },
                new CatalogDto
                {
                    CatalogId = Guid.NewGuid(),
                    Name = "Black electronics",
                    Description = "All black electronics in the shop",
                    Products = new List<ProductDto>
                    {
                        new ProductDto
                        {
                            ProductId = Guid.NewGuid(),
                            Name = "product #4",
                            Description = "description of product #4",
                            Price = 100M
                        },
                        new ProductDto
                        {
                            ProductId = Guid.NewGuid(),
                            Name = "product #5",
                            Description = "description of product #5",
                            Price = 9.99M
                        },
                    }
                },
                new CatalogDto
                {
                    CatalogId = Guid.NewGuid(),
                    Name = "Computers",
                    Description = "All computers in the shop - gaming, work, stations, Apple",
                    Products = new List<ProductDto>
                    {
                        new ProductDto
                        {
                            ProductId = Guid.NewGuid(),
                            Name = "product #6",
                            Description = "description of product #6",
                            Price = 12.59M
                        },
                        new ProductDto
                        {
                            ProductId = Guid.NewGuid(),
                            Name = "product #7",
                            Description = "description of product #7",
                            Price = 0M
                        },
                    }
                }
            };

            return Ok(list);
        }

        [HttpGet("{catalog_id}/products")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CatalogDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [SwaggerOperation(
            summary: "Retrieve catalogue based on its Id",
            description: "Return catalog given by catalogId and all its products associated to it",
            OperationId = "GetCatalog",
            Tags = new[] { "Product API" })]
        public async Task<IActionResult> GetCatalogAsync(
            [Required, FromRoute(Name = "catalog_id")] Guid? catalogId,
            CancellationToken ct)
        {
            return BadRequest("Not implemented");
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CatalogDto))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ConflictError))]
        [SwaggerOperation(
            summary: "Create new catalog",
            description: "Create new catalog in the database",
            OperationId = "CreateCatalog",
            Tags = new[] { "Catalog Management" })]
        public async Task<IActionResult> CreateCatalogAsync(
            [FromBody, Bind] CatalogModel model,
            CancellationToken ct)
        {
            return BadRequest("Not implemented");
        }

        [HttpPut("{catalog_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CatalogDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ConflictError))]
        [SwaggerOperation(
            summary: "Update existing catalog",
            description: "Create new catalog in the database",
            OperationId = "UpdateCatalog",
            Tags = new[] { "Catalog Management" })]
        public async Task<IActionResult> UpdateCatalogAsync(
            [Required, FromRoute(Name = "catalog_id")] Guid? catalogId,
            [FromBody, Bind] CatalogModel model,
            CancellationToken ct)
        {
            return BadRequest("Not implemented");
        }

        [HttpDelete("{catalog_id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [SwaggerOperation(
            summary: "Delete existing catalog",
            description: "Delete catalog from the database with all its products",
            OperationId = "DeleteCatalog",
            Tags = new[] { "Catalog Management" })]
        public async Task<IActionResult> DeleteCatalogAsync(
            [Required, FromRoute(Name = "catalog_id")] Guid? catalogId,
            CancellationToken ct)
        {
            return BadRequest("Not implemented");
        }

        [HttpPost("{catalog_id}/products")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [SwaggerOperation(
            summary: "Create product in existing catalog",
            description: "Create product in the database and associate it with catalog",
            OperationId = "CreateProductWithCatalogId",
            Tags = new[] { "Product Management" })]
        public async Task<IActionResult> AddProductToCatalogAsync(
            [Required, FromRoute(Name = "catalog_id")] Guid? catalogId,
            CancellationToken ct)
        {
            return BadRequest("Not implemented");
        }
    }
}
