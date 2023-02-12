using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Models;
using Visma.Bootcamp.eShop.ApplicationCore.Services.Interfaces;

namespace Visma.Bootcamp.eShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OrderDto>))]
        [SwaggerOperation(
            summary: "Retrieve all orders from the system",
            description: "Return all orders",
            OperationId = "GetOrders",
            Tags = new[] { "Order API" })]
        public async Task<IActionResult> GetOrdersAsync(CancellationToken ct, int? pageSize = null)
        {
            List<OrderDto> listOfOrders = await _orderService.GetAllAsync(pageSize, ct);
            if (pageSize != null)
            {
                var pagedList = new PagedListModel<OrderDto>(listOfOrders, pageSize.Value);
                return Ok(pagedList);
            }
            
            return Ok(listOfOrders);
        }

        [HttpGet("{order_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            summary: "Retrieve order based on its Id",
            description: "Return order given by orderId and all its products associated to it",
            OperationId = "GetOrder",
            Tags = new[] { "Order API" })]
        public async Task<IActionResult> GetOrderAsync(
            [Required, FromRoute(Name = "order_id")] Guid? orderId,
            CancellationToken ct)
        {
            OrderDto orderDto = await _orderService.GetAsync(orderId.Value, ct);
            return Ok(orderDto);
        }
    }
}
