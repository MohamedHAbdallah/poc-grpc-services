using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using POC.Grpc.Services.Core.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Grpc.Services.Core
{
    public static class GrpcClient
    {
        private static OrderServiceDef.OrderServiceDefClient? _orderClient = null;
        private static CustomerServiceDef.CustomerServiceDefClient? _customerClient = null;
        public static OrderServiceDef.OrderServiceDefClient OrderClient
        {
            get
            {
                if (_orderClient == null)
                {
                    var channel = GrpcChannel.ForAddress("https://localhost:7104");

                    //var invoker = channel.Intercept(new AuthorizationHeaderInterceptor());
                    //_orderClient = new OrderServiceDef.OrderServiceDefClient(invoker);

                    _orderClient = new OrderServiceDef.OrderServiceDefClient(channel);
                }
                return _orderClient;
            }
        }

        public static CustomerServiceDef.CustomerServiceDefClient CustomerClient
        {
            get
            {
                if (_customerClient == null)
                {
                    var channel = GrpcChannel.ForAddress("https://localhost:7077");

                    //var invoker = channel.Intercept(new AuthorizationHeaderInterceptor());
                    //_customerClient = new CustomerServiceDef.CustomerServiceDefClient(invoker);

                    _customerClient = new CustomerServiceDef.CustomerServiceDefClient(channel);
                }
                return _customerClient;
            }
        }
    }
}
