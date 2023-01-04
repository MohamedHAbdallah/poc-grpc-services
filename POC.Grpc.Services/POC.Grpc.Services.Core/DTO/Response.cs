using System;

namespace POC.Grpc.Services.Core.DTO
{
    public class Response
    {
        public int Status { get; set; }
        public string? Message { get; set; }
    }
}