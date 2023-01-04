// See https://aka.ms/new-console-template for more information
using POC.Grpc.Services.Core.DTO;
using System.Net.Http.Headers;
using System.Net.Http.Json;

int customerId;
GrpcRestDTO? res;
HttpResponseMessage response;

long totalGrpcTimeMs = 0,
     totalRestTimeMs = 0,
     totalGrpcBytesSize = 0,
     totalRestBytesSize = 0;
while (true)
{
    Console.Write("Please Enter Count of Loop <enter -1 to ending loop> : ");
    int loopsCount = int.Parse(Console.ReadLine() ?? "0");
    if (loopsCount == -1)
        break;
    for (int i = 0; i < loopsCount; i++)
    {
        customerId = i + 1;
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("http://localhost:5213/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //GET Method
            response = await client.GetAsync($"/Test/SelectBetweenGrpcAndRest?customerId={customerId}");
            if (response.IsSuccessStatusCode)
            {
                res = await response.Content.ReadFromJsonAsync<GrpcRestDTO>();
                totalGrpcTimeMs += res?.GrpcTimeMs ?? 0;
                totalRestTimeMs += res?.RestTimeMs ?? 0;
                totalGrpcBytesSize += res?.GrpcBytesSize ?? 0;
                totalRestBytesSize += res?.RestBytesSize ?? 0;
            }
            else
            {
                Console.WriteLine("Internal server Error");
            }
        }

    }

    double presentTime = (totalGrpcTimeMs / (double)totalRestTimeMs) * 100;
    double presentSize = (totalGrpcBytesSize / (double)totalRestBytesSize) * 100;
    presentTime = Math.Round(presentTime, 2);
    presentSize = Math.Round(presentSize, 2);
    Console.WriteLine($"Grpc Time {presentTime} % of Rest Time");
    Console.WriteLine($"Grpc Size {presentSize} % of Rest Size");
    Console.WriteLine($"\n\n\n");

}
Console.WriteLine($"\n\nplease enter any key to exit ......");
Console.Read();