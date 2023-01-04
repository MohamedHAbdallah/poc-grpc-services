using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Grpc.Services.Core.DTO
{
    public class OrdersResponse : Response
    {
        public List<OrderDTO>? Orders { get; set; }
    }
}
