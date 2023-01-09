using Protos.Customer;

namespace POC.Grpc.Services.Order.Business
{
    public interface IOrderService
    {
        Task<GetCustomerByIdResMsgDef> GetOrderByCustomerId(int customerId);
    }
}
