using Microsoft.AspNetCore.Mvc;
using POC.Grpc.Services.Order.Business;
using System.Text;

namespace POC.Grpc.Services.Order.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }


        [HttpGet]
        [Route("GetOrderByCustomerId")]
        public async Task<IActionResult> GetOrderByCustomerId(int customerId)
        {
            var res = await _orderService.GetOrderByCustomerId(customerId);
            return Ok(res);
        }
    }
}