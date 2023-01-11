using Microsoft.Extensions.Logging;
using POC.Grpc.Services.Core;
using Protos.Customer;
using static POC.Grpc.Services.Core.Protos.CustomerServiceDef;

namespace POC.Grpc.Services.Order.Business
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<CustomerServiceDefClient> _logger;
        private readonly CustomerServiceDefClient _client;

        public OrderService(ILogger<CustomerServiceDefClient> logger, CustomerServiceDefClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<GetCustomerByIdResMsgDef> GetOrderByCustomerId(int customerId)
        {
            GetCustomerByIdResMsgDef? res;
            var req = new GetCustomerByIdReqMsgDef { CustomerId = customerId };
            res = await _client.ExecuteAsync<GetCustomerByIdReqMsgDef, GetCustomerByIdResMsgDef, CustomerServiceDefClient>(_logger, "GetCustomerByIdAsync", req);
            return res;
        }
    }
}
