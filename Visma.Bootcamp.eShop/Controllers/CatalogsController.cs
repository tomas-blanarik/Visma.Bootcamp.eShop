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
using Visma.Bootcamp.eShop.ApplicationCore.Services.Interfaces;

namespace Visma.Bootcamp.eShop.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CatalogsController : ControllerBase
    {
        private readonly ICatalogService _catalogService;

        public CatalogsController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public const string GetCatalogRouteName = "getcatalog";

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CatalogDto>))]
        [SwaggerOperation(
            summary: "Retrieve all catalogs from the system",
            description: "Return all catalogs with their products",
            OperationId = "GetCatalogs",
            Tags = new[] { "Catalog API" })]
        public async Task<IActionResult> GetCatalogsAsync(CancellationToken ct)
        {
            return Ok(await _catalogService.GetAllAsync(ct));
        }

        [HttpGet("{catalog_id}/products", Name = GetCatalogRouteName)]
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
            return Ok(_catalogService.GetAsync(catalogId.Value, ct));
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
            var catalogDto = await _catalogService.CreateAsync(model, ct);
            return CreatedAtAction(
                GetCatalogRouteName,
                new { catalog_id = catalogDto.PublicId },
                catalogDto);
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
            return Ok(await _catalogService.UpdateAsync(catalogId.Value, model, ct));
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
            await _catalogService.DeleteAsync(catalogId.Value, ct);
            return NoContent();
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
            [Bind, FromBody] ProductModel model,
            [FromServices] IProductService productService,
            CancellationToken ct)
        {
            var productDto = await productService.CreateAsync(catalogId.Value, model, ct);
            return CreatedAtAction(
                ProductsController.GetProductRouteName,
                new { product_id = productDto.PublicId },
                productDto);
        }
    }
}
