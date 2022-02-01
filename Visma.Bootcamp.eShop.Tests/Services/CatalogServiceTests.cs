using System;
using System.Threading;
using System.Threading.Tasks;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Models;
using Visma.Bootcamp.eShop.ApplicationCore.Services;
using Xunit;

namespace Visma.Bootcamp.eShop.Tests.Services
{
    public class CatalogServiceTests
    {


        public CatalogServiceTests()
        {

        }

        private CatalogService CreateService()
        {
            return new CatalogService();
        }

        [Fact]
        public async Task CreateAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            CatalogModel model = null;
            CancellationToken ct = default(global::System.Threading.CancellationToken);

            // Act
            var result = await service.CreateAsync(
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
            Guid catalogId = default(global::System.Guid);
            CancellationToken ct = default(global::System.Threading.CancellationToken);

            // Act
            await service.DeleteAsync(
                catalogId,
                ct);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task GetAllAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            CancellationToken ct = default(global::System.Threading.CancellationToken);

            // Act
            var result = await service.GetAllAsync(
                ct);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task GetAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            Guid catalogId = default(global::System.Guid);
            CancellationToken ct = default(global::System.Threading.CancellationToken);

            // Act
            var result = await service.GetAsync(
                catalogId,
                ct);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task UpdateAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            Guid catalogId = default(global::System.Guid);
            CatalogModel model = null;
            CancellationToken ct = default(global::System.Threading.CancellationToken);

            // Act
            var result = await service.UpdateAsync(
                catalogId,
                model,
                ct);

            // Assert
            Assert.True(false);
        }
    }
}
