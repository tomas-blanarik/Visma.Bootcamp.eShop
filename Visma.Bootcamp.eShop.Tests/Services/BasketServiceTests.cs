using System;
using System.Threading;
using System.Threading.Tasks;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Models;
using Visma.Bootcamp.eShop.ApplicationCore.Services;
using Xunit;

namespace Visma.Bootcamp.eShop.Tests.Services
{
    public class BasketServiceTests
    {


        public BasketServiceTests()
        {

        }

        private BasketService CreateService()
        {
            return new BasketService();
        }

        [Fact]
        public async Task AddItemAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            Guid basketId = default(global::System.Guid);
            BasketItemModel model = null;
            CancellationToken ct = default(global::System.Threading.CancellationToken);

            // Act
            var result = await service.AddItemAsync(
                basketId,
                model,
                ct);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task DeleteItemAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            Guid basketId = default(global::System.Guid);
            Guid itemId = default(global::System.Guid);
            CancellationToken ct = default(global::System.Threading.CancellationToken);

            // Act
            await service.DeleteItemAsync(
                basketId,
                itemId,
                ct);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task GetAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            Guid basketId = default(global::System.Guid);
            CancellationToken ct = default(global::System.Threading.CancellationToken);

            // Act
            var result = await service.GetAsync(
                basketId,
                ct);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task UpdateAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            Guid basketId = default(global::System.Guid);
            BasketModel model = null;
            CancellationToken ct = default(global::System.Threading.CancellationToken);

            // Act
            await service.UpdateAsync(
                basketId,
                model,
                ct);

            // Assert
            Assert.True(false);
        }
    }
}
