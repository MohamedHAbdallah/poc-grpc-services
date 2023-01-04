using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using POC.Grpc.Services.Core;
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
            var req = new GetOrdersByCustomerIdReqMsgDef { CustomerId = customerId };
            var res = GrpcClient.OrderClient.GetOrdersByCustomerId(req);
            return Ok(res);
        }
    }
}