using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using POC.Grpc.Services.Core.Protos;
using Protos.Customer;

namespace POC.Grpc.Services.Customer.API.Services;

public class CustomerService : CustomerServiceDef.CustomerServiceDefBase
{
    private readonly ILogger<CustomerService> _logger;
    public CustomerService(ILogger<CustomerService> logger)
    {
        _logger = logger;
    }

   [Authorize(AuthenticationSchemes ="BasicAuth",Roles ="Device")]
    public override async Task<GetCustomerByIdResMsgDef> GetCustomerById(GetCustomerByIdReqMsgDef request, ServerCallContext context)
    {
        //throw new Exception("throw Error for Testing");
        var res = new GetCustomerByIdResMsgDef {Res = new Protos.Common.ResponseMsgDef {Status = 0 , Message = "تم بنجاح" }, Customer = new CustomerMsgDef { CustomerId = 1, CustomerName = "Mohamed Abdallah" }  };
        return await Task.FromResult(res);
    }

}