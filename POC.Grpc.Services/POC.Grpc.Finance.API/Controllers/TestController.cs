using Microsoft.AspNetCore.Mvc;
using POC.Grpc.Services.Core.DTO;
using Protos.Order;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace POC.Grpc.Finance.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        private List<OrderDTO> GetOrders(int customerId)
        {
            var orders = new List<OrderDTO>();
            List<ItemDTO> items;
            OrderDTO order;
            ItemDTO item;
            for (int i = 0; i < 10; i++)
            {
                items = new List<ItemDTO>();
                order = new OrderDTO
                {
                    OrderId = i + 1,
                    CustomerId = customerId,
                    Comment = $"Order # {i + 1} Comment ...... have 10 items each item have name and price you can show them",
                    DeliveredDate = DateTime.UtcNow,
                    DeliveryAddress = new DeliveryAddressDTO { City = "Giza", HouseNumber = 234, StreetName = "Mourad St. Dokki", ZipCode = "11433" },
                    IsDeleived = false
                };
                for (int j = 0; j < 10; j++)
                {
                    item = new ItemDTO
                    {
                        ItemId = j + 1,
                        ItemPrice = 10 * (j + 1),
                        ItemName = $"item {j + 1}",
                        ItemDescription = $"item #{j + 1} description contains all details about it",
                    };
                    items.Add(item);
                }
                order.TotalPrice = items.Sum(x => x.ItemPrice);
                order.Items= items;
                orders.Add(order);
            }
            return orders;
        }

        [HttpGet]
        [Route("GetOrdersByCustomerId")]
        public async Task<List<OrderDTO>> GetOrdersByCustomerId(int customerId)
        {
            var orders = GetOrders(customerId);
            var res = new OrdersResponse() { Orders = orders, Status = 0, Message = "تم بنجاح" };
            return await Task.FromResult(orders);
        }

        [HttpGet]
        [Route("GetOrdersSizeByCustomerId")]
        public async Task<OrdersSizeResponse> GetOrdersSizeByCustomerId(int customerId)
        {
            var orders = GetOrders(customerId);
            long size = 0;
            //object o = new { Orders = orders};
            using (Stream s = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(s, orders);
                size = s.Length;
            }
            //int size = Marshal.SizeOf(orders);
            var res = new OrdersSizeResponse() { OrdersSizeInBytes = size, Status = 0, Message = "تم بنجاح" };
            return await Task.FromResult(res);
        }
    }
}