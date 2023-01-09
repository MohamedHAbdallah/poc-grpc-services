using Grpc.Core;
using Microsoft.Extensions.Logging;
using POC.Grpc.Services.Core;
using POC.Grpc.Services.Core.Protos;
using Protos.Customer;

namespace POC.Grpc.Services.Order.Business
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly CustomerServiceDef.CustomerServiceDefClient _client;

        public OrderService(ILogger<OrderService> logger, CustomerServiceDef.CustomerServiceDefClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<GetCustomerByIdResMsgDef> GetOrderByCustomerId( int customerId)
        {
            GetCustomerByIdResMsgDef res = new GetCustomerByIdResMsgDef();
            //try
          //  {
                var req = new GetCustomerByIdReqMsgDef { CustomerId = customerId };

                res = await _client.GetCustomerByIdAsync(req);
                //.ExecuteAsync<GetCustomerByIdResMsgDef>(req);

                return res;
        //}
            //catch (RpcException ex)
            //{
            //    if (ex.StatusCode == StatusCode.Unauthenticated) { }
            //    _logger.LogError(ex.Message);
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex.Message);
            //}
return res;
        }
    }
}
