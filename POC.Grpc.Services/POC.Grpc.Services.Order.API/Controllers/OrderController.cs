using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using POC.Grpc.Services.Core.Protos;
using Protos.Customer;

namespace POC.Grpc.Services.Order.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        [Route("GetOrderByCustomerId")]
        public IActionResult GetOrderByCustomerId(int customerId)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7077");

            var client = new CustomerServiceDef.CustomerServiceDefClient(channel);

            var req = new GetCustomerByIdReqMsgDef { CustomerId = customerId };
            var res = client.GetCustomerById(req);
            return Ok(res);
        }
    }
}