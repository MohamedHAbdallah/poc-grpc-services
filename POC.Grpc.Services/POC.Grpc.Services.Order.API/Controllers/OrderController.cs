using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using POC.Grpc.Services.Core;
using POC.Grpc.Services.Core.Protos;
using Protos.Customer;
using System.Text;

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
            try
            {
                var req = new GetCustomerByIdReqMsgDef { CustomerId = customerId };
                var res = GrpcClient.CustomerClient.GetCustomerByIdAsync(req);
                
                return Ok(res);
            }
            catch (RpcException ex)
            {
                _logger.LogError(ex.Message);
                return Ok();
            }
        }
    }
}