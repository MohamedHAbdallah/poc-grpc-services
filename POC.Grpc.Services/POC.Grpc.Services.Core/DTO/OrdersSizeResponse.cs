using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Grpc.Services.Core.DTO
{
    public class OrdersSizeResponse : Response
    {
        public long OrdersSizeInBytes { get; set; }
    }
}
