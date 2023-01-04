using Protos.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Grpc.Services.Core.DTO
{
    [Serializable]
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public double TotalPrice { get; set; }
        public bool IsDeleived { get; set; }
        public string? Comment { get; set; }
        public DateTime DeliveredDate { get; set; }
        public DeliveryAddressDTO? DeliveryAddress { get; set; }
        public List<ItemDTO>? Items { get; set; }
    }
}
