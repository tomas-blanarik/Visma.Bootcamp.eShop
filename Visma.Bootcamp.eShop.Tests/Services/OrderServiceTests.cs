using NSubstitute;
using System;
using Visma.Bootcamp.eShop.ApplicationCore.Services;
using Xunit;

namespace Visma.Bootcamp.eShop.Tests.Services
{
    public class OrderServiceTests
    {


        public OrderServiceTests()
        {

        }

        private OrderService CreateService()
        {
            return new OrderService();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var service = this.CreateService();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
