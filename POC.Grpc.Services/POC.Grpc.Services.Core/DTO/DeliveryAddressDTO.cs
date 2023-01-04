using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Grpc.Services.Core.DTO
{
    [Serializable]
    public class DeliveryAddressDTO
    {
        public string? StreetName { get; set; }
        public int HouseNumber { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
    }
}
