using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Grpc.Services.Core.DTO
{
    public class GrpcRestDTO
    {
        public long GrpcBytesSize { get; set; }
        public long GrpcTimeMs { get; set; }
        public long RestBytesSize { get; set; }
        public long RestTimeMs { get; set; }
    }
}
