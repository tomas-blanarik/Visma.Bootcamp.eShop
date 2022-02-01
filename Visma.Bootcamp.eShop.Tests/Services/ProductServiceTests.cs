using System;
using System.Threading;
using System.Threading.Tasks;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Models;
using Visma.Bootcamp.eShop.ApplicationCore.Services;
using Xunit;

namespace Visma.Bootcamp.eShop.Tests.Services
{
    public class ProductServiceTests
    {


        public ProductServiceTests()
        {

        }

        private ProductService CreateService()
        {
            return new ProductService();
        }

        [Fact]
        public async Task CreateAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            Guid catalogId = default(global::System.Guid);
            ProductModel model = null;
            CancellationToken ct = default(global::System.Threading.CancellationToken);

            // Act
            var result = await service.CreateAsync(
                catalogId,
                model,
                ct);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task DeleteAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            Guid productId = default(global::System.Guid);
            CancellationToken ct = default(global::System.Threading.CancellationToken);

            // Act
            await service.DeleteAsync(
                productId,
                ct);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task GetAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            Guid productId = default(global::System.Guid);
            CancellationToken ct = default(global::System.Threading.CancellationToken);

            // Act
            var result = await service.GetAsync(
                productId,
                ct);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task UpdateAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            Guid productId = default(global::System.Guid);
            ProductModel model = null;
            CancellationToken ct = default(global::System.Threading.CancellationToken);

            // Act
            var result = await service.UpdateAsync(
                productId,
                model,
                ct);

            // Assert
            Assert.True(false);
        }
    }
}
