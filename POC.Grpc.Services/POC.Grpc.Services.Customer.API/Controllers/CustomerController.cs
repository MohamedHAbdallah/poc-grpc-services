using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using POC.Grpc.Services.Core.Protos;
using Protos.Order;

namespace POC.Grpc.Services.Customer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ILogger<CustomerController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        [Route("GetCustomerById")]
        public IActionResult GetCustomerById(int customerId)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7104");

            var client = new OrderServiceDef.OrderServiceDefClient(channel);

            var req = new GetOrdersByCustomerIdReqMsgDef { CustomerId = customerId };
            var res = client.GetOrdersByCustomerId(req);
            return Ok(res);
        }
    }
}