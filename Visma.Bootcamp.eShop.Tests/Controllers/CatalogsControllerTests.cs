using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Models;
using Visma.Bootcamp.eShop.ApplicationCore.Exceptions;
using Visma.Bootcamp.eShop.ApplicationCore.Services.Interfaces;
using Visma.Bootcamp.eShop.Controllers;
using Xunit;

namespace Visma.Bootcamp.eShop.Tests.Controllers
{
    public class CatalogsControllerTests
    {
        private ICatalogService subCatalogService;

        public CatalogsControllerTests()
        {
            this.subCatalogService = Substitute.For<ICatalogService>();
        }

        private CatalogsController CreateCatalogsController()
        {
            return new CatalogsController(
                this.subCatalogService);
        }

        [Fact]
        public async Task GetCatalogsAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var catalogsController = this.CreateCatalogsController();
            CancellationToken ct = default(global::System.Threading.CancellationToken);
            var list = new List<CatalogDto>
            {
                new CatalogDto
                {
                    Name = "Catalog #1",
                    CatalogId = Guid.NewGuid(),
                    Description = "Test description #1",
                    Products = new List<ProductDto>()
                }
            };

            subCatalogService.Get().Returns(list);

            // Act
            var result = await catalogsController.GetCatalogsAsync(
                ct);

            // Assert
            subCatalogService.Received().Get();
            subCatalogService.DidNotReceiveWithAnyArgs().Get(Arg.Any<Guid>());

            Assert.IsType<OkObjectResult>(result);
            OkObjectResult okResult = (OkObjectResult)result;
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<List<CatalogDto>>(okResult.Value);
            List<CatalogDto> okResultList = (List<CatalogDto>)okResult.Value;
            Assert.Collection(okResultList, item =>
            {
                Assert.NotEqual(default(Guid), item.CatalogId);
                Assert.NotNull(item.Description);
                Assert.NotNull(item.Name);
                Assert.NotNull(item.Products);
            });
        }

        [Fact]
        public async Task GetCatalogAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var catalogsController = this.CreateCatalogsController();
            Guid? catalogId = null;
            CancellationToken ct = default(global::System.Threading.CancellationToken);

            // Act
            var result = await catalogsController.GetCatalogAsync(
                catalogId,
                ct);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task CreateCatalogAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            const string catalogName = "Test name #1";
            const string catalogDescription = "Test Description #1";

            var catalogsController = this.CreateCatalogsController();
            CancellationToken ct = default(global::System.Threading.CancellationToken);

            CatalogModel model = new CatalogModel
            {
                Name = catalogName,
                Description = catalogDescription
            };

            var catalogDto = new CatalogDto
            {
                CatalogId = Guid.NewGuid(),
                Name = catalogName,
                Description = catalogDescription
            };

            subCatalogService.Create(Arg.Any<CatalogModel>())
                .Returns(catalogDto);

            // Act
            var result = await catalogsController.CreateCatalogAsync(
                model,
                ct);

            subCatalogService.Received().Create(Arg.Is(model));

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
            CreatedAtActionResult createdResult = (CreatedAtActionResult)result;
            Assert.IsType<CatalogDto>(createdResult.Value);
            CatalogDto createdCatalogDto = (CatalogDto)createdResult.Value;
            Assert.Equal(catalogName, createdCatalogDto.Name);
            Assert.Equal(catalogDescription, createdCatalogDto.Description);
            Assert.Equal(createdCatalogDto.Id, catalogDto.Id);
        }

        [Fact]
        public async Task CreateCatalogAsync_StateUnderTest_ConflictException()
        {
            // Arrange
            var catalogsController = this.CreateCatalogsController();
            CancellationToken ct = default(global::System.Threading.CancellationToken);
            CatalogModel model = new CatalogModel();

            subCatalogService.Create(Arg.Any<CatalogModel>())
                .Returns(x =>
                {
                    throw new ConflictException($"Catalog with name {model.Name} already exists");
                });

            // Act + Assert
            await Assert.ThrowsAsync<ConflictException>(async () =>
            {
                await catalogsController.CreateCatalogAsync(
                    model,
                    ct);
            });

        }

        [Fact]
        public async Task UpdateCatalogAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var catalogsController = this.CreateCatalogsController();
            Guid? catalogId = null;
            CatalogModel model = null;
            CancellationToken ct = default(global::System.Threading.CancellationToken);

            // Act
            var result = await catalogsController.UpdateCatalogAsync(
                catalogId,
                model,
                ct);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task DeleteCatalogAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var catalogsController = this.CreateCatalogsController();
            Guid? catalogId = null;
            CancellationToken ct = default(global::System.Threading.CancellationToken);

            // Act
            var result = await catalogsController.DeleteCatalogAsync(
                catalogId,
                ct);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task AddProductToCatalogAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var catalogsController = this.CreateCatalogsController();
            Guid? catalogId = null;
            CancellationToken ct = default(global::System.Threading.CancellationToken);

            // Act
            var result = await catalogsController.AddProductToCatalogAsync(
                catalogId,
                ct);

            // Assert
            Assert.True(false);
        }
    }
}
