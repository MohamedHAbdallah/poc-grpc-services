using Grpc.Core;
using POC.Grpc.Services.Core.Protos;
using Protos.Order;

namespace POC.Grpc.Services.Order.API.Services;

public class OrderService : OrderServiceDef.OrderServiceDefBase
{
    private readonly ILogger<OrderService> _logger;
    public OrderService(ILogger<OrderService> logger)
    {
        _logger = logger;
    }

    public override async Task<GetOrdersByCustomerIdResMsgDef> GetOrdersByCustomerId(GetOrdersByCustomerIdReqMsgDef request, ServerCallContext context)
    {
        var res = new GetOrdersByCustomerIdResMsgDef();
        var orders = new OrdersMsgDef();
        orders.Orders.Add(new OrderMsgDef { OrderId = 1, CustomerId = request.CustomerId });
        orders.Orders.Add(new OrderMsgDef { OrderId = 2, CustomerId = request.CustomerId });
        res.Orders = orders;
        res = new GetOrdersByCustomerIdResMsgDef { Orders = orders, Res = new Protos.Common.ResponseMsgDef { Status = 0, Message = "تم بنجاح" } };
        return await Task.FromResult(res);
    }
}