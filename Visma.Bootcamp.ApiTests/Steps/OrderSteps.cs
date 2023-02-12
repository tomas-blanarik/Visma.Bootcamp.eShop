using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow;
using Visma.Bootcamp.ApiTests.Infrastructure;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO;

namespace Visma.Bootcamp.ApiTests.Steps
{
    [Binding]
    public class OrderSteps : BaseSteps
    {
        private RestResponse _lastResponse;
        private RestResponse<List<OrderDto>> _orderListResponse;

        [Given("I retrieve all orders")]
        [When("I retrieve all orders")]
        public void GivenIRetrieveAllOrders()
        {
            RetrieveAllOrders();
            VerifyResponse(_lastResponse, HttpStatusCode.OK);
        }

        [Then("I see successful response with status code-'(.*)'")]
        public void ThenISeeItemsInTheListCount(int statusCode)
        {
            Assert.That((int)_lastResponse.StatusCode, Is.EqualTo(statusCode), $"StatusCode does not match");
        }

        private void RetrieveAllOrders()
        {
            _orderListResponse = Call.Order.GetAllOrders();
            _lastResponse = _orderListResponse;
        }
    }
}