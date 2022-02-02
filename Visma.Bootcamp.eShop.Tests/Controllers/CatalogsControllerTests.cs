using System;
using System.Threading;
using System.Threading.Tasks;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Models;
using Visma.Bootcamp.eShop.Controllers;
using Xunit;

namespace Visma.Bootcamp.eShop.Tests.Controllers
{
    public class CatalogsControllerTests
    {


        public CatalogsControllerTests()
        {

        }

        private CatalogsController CreateCatalogsController()
        {
            return new CatalogsController();
        }

        [Fact]
        public async Task GetCatalogsAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var catalogsController = this.CreateCatalogsController();
            CancellationToken ct = default(global::System.Threading.CancellationToken);

            // Act
            var result = await catalogsController.GetCatalogsAsync(
                ct);

            // Assert
            Assert.True(false);
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
            var catalogsController = this.CreateCatalogsController();
            CatalogModel model = null;
            CancellationToken ct = default(global::System.Threading.CancellationToken);

            // Act
            var result = await catalogsController.CreateCatalogAsync(
                model,
                ct);

            // Assert
            Assert.True(false);
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
