using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;
using Visma.Bootcamp.ApiTests.Infrastructure;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO;

namespace Visma.Bootcamp.ApiTests.Actions
{
    public class OrderActions : MainAppActionBase
    {
        public OrderActions() : base("Orders")
        {
        }

        public RestResponse<OrderDto> GetOrder(Guid id)
        {
            var request = CreateRequest(Method.Get, $"{id}");
            var result = Execute<OrderDto>(request);

            return result;
        }

        public RestResponse<List<OrderDto>> GetAllOrders()
        {
            var request = CreateRequest(Method.Get, "");
            var result = Execute<List<OrderDto>>(request);

            return result;
        }
    }
}
