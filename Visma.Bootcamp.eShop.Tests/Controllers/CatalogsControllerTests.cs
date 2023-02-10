using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO;
using Visma.Bootcamp.eShop.ApplicationCore.Services;
using Visma.Bootcamp.eShop.ApplicationCore.Services.Interfaces;
using Visma.Bootcamp.eShop.Controllers;
using Xunit;

namespace Visma.Bootcamp.eShop.Tests.Controllers
{
    public class CatalogsControllerTests
    {
        // NSubstitute
        // xUnit

        private ICatalogService _catalogService;
        private CatalogsController _controller;

        public CatalogsControllerTests()
        {
            _catalogService = Substitute.For<ICatalogService>();
            _controller = new CatalogsController(_catalogService);
        }

        [Fact]
        public async Task GetCatalogsAsync_ExpectedResult()
        {
            // arrange
            CancellationToken ct = CancellationToken.None;
            var list = new List<CatalogDto>
            {
                new CatalogDto { PublicId = Guid.NewGuid(), Name = "test1" },
                new CatalogDto { PublicId = Guid.NewGuid(), Name = "test2" }
            };
            _catalogService.GetAllAsync().Returns(list);

            // act
            IActionResult result = await _controller.GetCatalogsAsync(ct);

            // assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            OkObjectResult castedResult = (OkObjectResult)result;
            Assert.Equal(200, castedResult.StatusCode);
            Assert.IsType<List<CatalogDto>>(castedResult.Value);
            List<CatalogDto> catalogs = (List<CatalogDto>)castedResult.Value;
            Assert.NotNull(catalogs);
            Assert.NotEmpty(catalogs);
            Assert.Equal(list.Count, catalogs.Count);
        }
    }
}