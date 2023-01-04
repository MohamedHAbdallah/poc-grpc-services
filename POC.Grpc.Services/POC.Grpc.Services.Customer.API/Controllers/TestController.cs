using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using POC.Grpc.Services.Core;
using POC.Grpc.Services.Core.DTO;
using POC.Grpc.Services.Core.Protos;
using Protos.Order;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace POC.Grpc.Services.Customer.API.Controllers
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


        private async Task<long?> GetOrdersSizeByCustomerId(int customerId)
        {
            long? contentSize = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5122/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method
                HttpResponseMessage response = await client.GetAsync($"/Test/GetOrdersByCustomerId?customerId={customerId}");
                if (response.IsSuccessStatusCode)
                {
                    List<OrderDTO>? res = await response.Content.ReadFromJsonAsync<List<OrderDTO>>();
                    contentSize = response.Content.Headers.ContentLength;
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
            }
            return contentSize;
        }

        [HttpGet]
        [Route("SelectBetweenGrpcAndRest")]
        public async Task<IActionResult> SelectBetweenGrpcAndRest(int customerId)
        {
            long grpcTimeInMs, restTimeInMs ;
            long grpcSizeInBytes, restSizeInBytes ;
            Stopwatch? watch;
            GetOrdersSizeByCustomerIdResMsgDef resGrpc;
            long? resRest;

            var reqGrpc = new GetOrdersByCustomerIdReqMsgDef { CustomerId = customerId };
            
            watch = Stopwatch.StartNew();
            resGrpc = GrpcClient.OrderClient.GetOrdersSizeByCustomerId(reqGrpc);
            watch.Stop();
            grpcTimeInMs = watch.ElapsedMilliseconds;
            grpcSizeInBytes = resGrpc.SizeBytes;

            watch = Stopwatch.StartNew();
            resRest = await GetOrdersSizeByCustomerId(customerId);
            watch.Stop();
            restTimeInMs = watch.ElapsedMilliseconds;
            restSizeInBytes = resRest ?? 0;


            var res = new { grpcBytesSize = grpcSizeInBytes, grpcTimeMs = grpcTimeInMs, restBytesSize = restSizeInBytes, restTimeMs = restTimeInMs };
            return Ok(res);
        }
    }
}