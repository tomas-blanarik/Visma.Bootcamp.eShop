using Microsoft.AspNetCore.Mvc;
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

        /*
            Pre kazdu operaciu v tomto controlleri, nezabudnite pridat
            [SwaggerOperation(
                summary: "Kratky popis endpointu",
                description: "Trosku dlhsi popis endpointu, kludne aj detaily business logiky",
                OperationId = "JedinecneIdOperacie",
                Tags = new[] { "Order API" })]
        */

        // Navrh metod do tohto controllera:
        // - GET {order_id}
        // - DELETE {order_id]
        // - PUT {order_id}
    }
}
