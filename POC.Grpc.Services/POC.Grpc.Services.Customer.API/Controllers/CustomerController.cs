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
        private readonly OrderServiceDef.OrderServiceDefClient _client;
        public CustomerController(ILogger<CustomerController> logger, OrderServiceDef.OrderServiceDefClient client)
        {
            _logger = logger;
            _client = client;
        }

        
        [HttpGet]
        [Route("GetCustomerById")]
        public IActionResult GetCustomerById(int customerId)
        {
            var req = new GetOrdersByCustomerIdReqMsgDef { CustomerId = customerId };
            var res = _client.GetOrdersByCustomerId(req);
            return Ok(res);
        }
    }
}