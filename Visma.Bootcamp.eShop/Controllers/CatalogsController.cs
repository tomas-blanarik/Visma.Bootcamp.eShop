using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
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
    public class CatalogsController : ControllerBase
    {
        private readonly ICatalogService _service;

        public CatalogsController(ICatalogService service)
        {
            _service = service;
        }

        public const string GetCatalogRouteName = "getcatalog";

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CatalogDto>))]
        [SwaggerOperation(
            summary: "Retrieve all catalogs from the system",
            description: "Return all catalogs with their products",
            OperationId = "GetCatalogs",
            Tags = new[] { "Catalog API" })]
        public IActionResult GetCatalogs()
        {
            return Ok(_service.Get());
        }

        [HttpGet("{catalog_id}/products", Name = GetCatalogRouteName)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CatalogDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [SwaggerOperation(
            summary: "Retrieve catalogue based on its Id",
            description: "Return catalog given by catalogId and all its products associated to it",
            OperationId = "GetCatalog",
            Tags = new[] { "Product API" })]
        public IActionResult GetCatalog([Required, FromRoute(Name = "catalog_id")] Guid? catalogId)
        {
            return Ok(_service.Get(catalogId.Value));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CatalogDto))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ConflictError))]
        [SwaggerOperation(
            summary: "Create new catalog",
            description: "Create new catalog in the database",
            OperationId = "CreateCatalog",
            Tags = new[] { "Catalog Management" })]
        public IActionResult CreateCatalog([FromBody, Bind] CatalogModel model)
        {
            var catalogDto = _service.Create(model);
            return CreatedAtAction(
                GetCatalogRouteName,
                new { catalog_id = catalogDto.Id },
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
        public IActionResult UpdateCatalog([Required, FromRoute(Name = "catalog_id")] Guid? catalogId,
                                           [FromBody, Bind] CatalogModel model)
        {
            return Ok(_service.Update(catalogId.Value, model));
        }

        [HttpDelete("{catalog_id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [SwaggerOperation(
            summary: "Delete existing catalog",
            description: "Delete catalog from the database with all its products",
            OperationId = "DeleteCatalog",
            Tags = new[] { "Catalog Management" })]
        public IActionResult DeleteCatalog([Required, FromRoute(Name = "catalog_id")] Guid? catalogId)
        {
            _service.Delete(catalogId.Value);
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
        public IActionResult AddProductToCatalog([Required, FromRoute(Name = "catalog_id")] Guid? catalogId)
        {
            return BadRequest("Not implemented");
        }
    }
}
