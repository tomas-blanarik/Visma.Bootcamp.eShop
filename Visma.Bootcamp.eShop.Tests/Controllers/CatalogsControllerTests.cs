using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
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
        public void GetCatalogs_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var catalogsController = this.CreateCatalogsController();
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
            var result = catalogsController.GetCatalogs();

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
        public void GetCatalog_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var catalogsController = this.CreateCatalogsController();
            Guid? catalogId = null;

            // Act
            var result = catalogsController.GetCatalog(catalogId);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void CreateCatalog_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            const string catalogName = "Test name #1";
            const string catalogDescription = "Test Description #1";

            var catalogsController = this.CreateCatalogsController();

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
            var result = catalogsController.CreateCatalog(model);

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
        public void CreateCatalog_StateUnderTest_ConflictException()
        {
            // Arrange
            var catalogsController = this.CreateCatalogsController();
            CatalogModel model = new CatalogModel();

            subCatalogService.Create(Arg.Any<CatalogModel>())
                .Returns(x =>
                {
                    throw new ConflictException($"Catalog with name {model.Name} already exists");
                });

            // Act + Assert
            Assert.Throws<ConflictException>(() =>
            {
                catalogsController.CreateCatalog(model);
            });

        }

        [Fact]
        public void UpdateCatalog_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var catalogsController = this.CreateCatalogsController();
            Guid? catalogId = null;
            CatalogModel model = null;

            // Act
            var result = catalogsController.UpdateCatalog(catalogId,
                                                          model);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void DeleteCatalog_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var catalogsController = this.CreateCatalogsController();
            Guid? catalogId = null;

            // Act
            var result = catalogsController.DeleteCatalog(catalogId);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void AddProductToCatalog_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var catalogsController = this.CreateCatalogsController();
            Guid? catalogId = null;

            // Act
            var result = catalogsController.AddProductToCatalog(catalogId);

            // Assert
            Assert.True(false);
        }
    }
}
