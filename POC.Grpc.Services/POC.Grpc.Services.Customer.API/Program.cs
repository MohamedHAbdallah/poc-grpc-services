using Microsoft.AspNetCore.Authentication;
using POC.Grpc.Services.Core;
using POC.Grpc.Services.Customer.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc(options =>
{
    options.Interceptors.Add<AuthorizationHeaderInterceptor>();
});
builder.Services.AddSingleton<AuthorizationHeaderInterceptor>();
builder.Services.AddGrpcReflection();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddAuthentication("BasicAuth")
//    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuth",opt =>
//    {

//    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGrpcService<CustomerService>();

app.MapGrpcReflectionService();

app.UseAuthorization();

app.MapControllers();

app.Run();
