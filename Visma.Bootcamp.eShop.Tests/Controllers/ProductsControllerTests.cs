using NSubstitute;
using System;
using System.Threading;
using System.Threading.Tasks;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Models;
using Visma.Bootcamp.eShop.ApplicationCore.Services.Interfaces;
using Visma.Bootcamp.eShop.Controllers;
using Xunit;

namespace Visma.Bootcamp.eShop.Tests.Controllers
{
    public class ProductsControllerTests
    {
        private IProductService subProductService;

        public ProductsControllerTests()
        {
            this.subProductService = Substitute.For<IProductService>();
        }

        private ProductsController CreateProductsController()
        {
            return new ProductsController(
                this.subProductService);
        }

        [Fact]
        public async Task GetProductAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var productsController = this.CreateProductsController();
            Guid? productId = null;
            CancellationToken ct = default(global::System.Threading.CancellationToken);

            // Act
            var result = await productsController.GetProductAsync(
                productId,
                ct);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task UpdateProductAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var productsController = this.CreateProductsController();
            Guid? productId = null;
            ProductModel model = null;
            CancellationToken ct = default(global::System.Threading.CancellationToken);

            // Act
            var result = await productsController.UpdateProductAsync(
                productId,
                model,
                ct);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task DeleteProductAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var productsController = this.CreateProductsController();
            Guid? productId = null;
            CancellationToken ct = default(global::System.Threading.CancellationToken);

            // Act
            var result = await productsController.DeleteProductAsync(
                productId,
                ct);

            // Assert
            Assert.True(false);
        }
    }
}
