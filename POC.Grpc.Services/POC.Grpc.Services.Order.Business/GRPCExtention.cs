using Google.Protobuf;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Grpc.Services.Order.Business
{
    public static class GRPCExtention
    {
        public static IMessage Execute(this ClientBase clientBase, IMessage message)
        {
            IMessage message1 = null;
            return message1;
        }

        public static async Task<T> ExecuteAsync<T>(this ClientBase clientBase, IMessage message) where T : class
        {
            T message1 = null;
            return message1;
        }
    }
}
