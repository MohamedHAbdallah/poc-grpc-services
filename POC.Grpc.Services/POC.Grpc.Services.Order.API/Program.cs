using Microsoft.AspNetCore.Authentication;
using POC.Grpc.Services.Core;
using POC.Grpc.Services.Core.Protos;
using POC.Grpc.Services.Order.Business;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddGrpc(options =>
{
    //unary server interceptor
    options.Interceptors.Add<AuthorizationHeaderInterceptor>();
});
builder.Services.AddSingleton<AuthorizationHeaderInterceptor>();
builder.Services.AddGrpcReflection();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//don't need if you want call it for each request each reaquest each request what ever have auth attribute
builder.Services.AddAuthentication("BasicAuth")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuth", opt =>
    {

    });

builder.Services.AddTransient<IOrderService, POC.Grpc.Services.Order.Business.OrderService>();

builder.Services.AddScoped<ITokenProvider, AppTokenProvider>();

//.AddInterceptor<AuthorizationHeaderInterceptor>(InterceptorScope.Client)
builder.Services.AddGrpcClient<CustomerServiceDef.CustomerServiceDefClient>(o =>
{
    o.Address = new Uri("https://localhost:7077");
})
.AddCallCredentials(async (context, metadata, serviceProvider) =>
{
    var provider = serviceProvider.GetRequiredService<ITokenProvider>();
    var token = await provider.GetTokenAsync();
    if (!string.IsNullOrEmpty(token))
    {
        metadata.Add("Authorization", $"Basic {token}");
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGrpcService<POC.Grpc.Services.Order.API.Services.OrderService>();

app.MapGrpcReflectionService();

app.UseAuthorization();

app.MapControllers();

app.Run();
