using System;
using System.Threading;
using System.Threading.Tasks;
using Visma.Bootcamp.eShop.Controllers;
using Xunit;

namespace Visma.Bootcamp.eShop.Tests.Controllers
{
    public class ProductsControllerTests
    {


        public ProductsControllerTests()
        {

        }

        private ProductsController CreateProductsController()
        {
            return new ProductsController();
        }

        [Fact]
        public async Task GetProductAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var productsController = this.CreateProductsController();
            CancellationToken ct = default(global::System.Threading.CancellationToken);

            // Act
            var result = await productsController.GetProductAsync(
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
            CancellationToken ct = default(global::System.Threading.CancellationToken);

            // Act
            var result = await productsController.UpdateProductAsync(
                productId,
                ct);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task DeleteProductAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var productsController = this.CreateProductsController();
            CancellationToken ct = default(global::System.Threading.CancellationToken);

            // Act
            var result = await productsController.DeleteProductAsync(
                ct);

            // Assert
            Assert.True(false);
        }
    }
}
