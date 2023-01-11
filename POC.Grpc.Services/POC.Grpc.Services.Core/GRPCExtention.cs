using Google.Protobuf;
using Grpc.Core;
using Grpc.Net.Client.Configuration;
using Microsoft.Extensions.Logging;
using Protos.Common;
using Protos.Customer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace POC.Grpc.Services.Core
{
    public static class GRPCExtention
    {
        public static IMessage Execute(this ClientBase clientBase, IMessage message)
        {
            IMessage message1 = null;
            return message1;
        }

        public static async Task<G?> ExecuteAsync<T, G, R>(this ClientBase<R> clientBase, ILogger<R> logger, string rpcName, IMessage<T> reqMsg) where T : IMessage<T> where G :  IMessage<G> , new() where R : ClientBase<R>
        {
            G? response = new G();
            try
            {
                Type thisType = clientBase.GetType();
                MethodInfo? theMethod = thisType.GetMethod(rpcName, new Type[] { typeof(T), typeof(Metadata), typeof(DateTime?), typeof(CancellationToken) });
                dynamic? awaitable = theMethod?.Invoke(clientBase, new object[] { reqMsg, Type.Missing, Missing.Value, Missing.Value });
                await awaitable;
                response = awaitable?.GetAwaiter().GetResult();
            }
            catch (RpcException ex)
            {
                PropertyInfo? prop = typeof(G).GetProperty("Res");
                ResponseMsgDef? value = new ResponseMsgDef() {  Message = ex.Message, Status = 99 };
                
                if (prop != null && prop.GetValue(response) != null && prop.GetValue(response) is ResponseMsgDef)
                {
                    value = prop.GetValue(response) as ResponseMsgDef;
                }
                if (prop != null)
                    prop.SetValue(response, value);
                
                logger.LogError(ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
            return response;
        }
    }
}
