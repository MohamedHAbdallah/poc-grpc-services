using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using POC.Grpc.Services.Core.Protos;
using Protos.Order;
using System;
using System.Data;

namespace POC.Grpc.Services.Order.API.Services;

public class OrderService : OrderServiceDef.OrderServiceDefBase
{
    private readonly ILogger<OrderService> _logger;
    public OrderService(ILogger<OrderService> logger)
    {
        _logger = logger;
    }

    private OrdersMsgDef GetOrdersMsgByCustomerId(int customerId)
    {
        var orders = new OrdersMsgDef();
        OrderMsgDef order;
        ItemMsgDef item;
        for (int i = 0; i < 10; i++)
        {
            order = new OrderMsgDef
            {
                OrderId = i + 1,
                CustomerId = customerId,
                Comment = $"Order # {i + 1} Comment ...... have 10 items each item have name and price you can show them",
                DeliveredDate = Timestamp.FromDateTime(DateTime.UtcNow),
                DeliveryAddress = new AddressMsgDef { City = "Giza", HouseNumber = 234, StreetName = "Mourad St. Dokki", ZipCode = "11433" },
                IsDeleived = false
            };
            for (int j = 0; j < 10; j++)
            {
                item = new ItemMsgDef
                {
                    ItemId = j + 1,
                    ItemPrice = 10 * (j + 1),
                    ItemName = $"item {j + 1}",
                    ItemDescription = $"item #{j + 1} description contains all details about it",
                };
                order.Items.Add(item);
            }
            order.TotalPrice = order.Items.Sum(x => x.ItemPrice);
            orders.Orders.Add(order);
        }
        return orders;
    }
    //[Authorize(AuthenticationSchemes = "BasicAuth", Roles = "Device")]
    public override async Task<GetOrdersByCustomerIdResMsgDef> GetOrdersByCustomerId(GetOrdersByCustomerIdReqMsgDef request, ServerCallContext context)
    {
        var res = new GetOrdersByCustomerIdResMsgDef();
        var orders = GetOrdersMsgByCustomerId(request.CustomerId);
        res.Orders = orders;
        res.Res = new Protos.Common.ResponseMsgDef { Status = 0, Message = "تم بنجاح" };
        return await Task.FromResult(res);
    }

    //[Authorize(AuthenticationSchemes = "BasicAuth", Roles = "Device")]
    public override async Task<GetOrdersSizeByCustomerIdResMsgDef> GetOrdersSizeByCustomerId(GetOrdersByCustomerIdReqMsgDef request, ServerCallContext context)
    {
        var res = new GetOrdersSizeByCustomerIdResMsgDef();
        var ordersMsg = GetOrdersMsgByCustomerId(request.CustomerId);
        int size = ordersMsg.CalculateSize();
        res.SizeBytes= size;
        res.Res = new Protos.Common.ResponseMsgDef { Status = 0, Message = "تم بنجاح" };
        return await Task.FromResult(res);
    }
}