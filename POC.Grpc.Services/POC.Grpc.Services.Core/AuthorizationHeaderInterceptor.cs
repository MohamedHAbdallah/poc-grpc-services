using Grpc.Core.Interceptors;
using Grpc.Core;
using System.Text;
using Newtonsoft.Json;

namespace POC.Grpc.Services.Core
{
    public class AuthorizationHeaderInterceptor : Interceptor
    {
        public AuthorizationHeaderInterceptor()
        {
            var x = 0;
        }

        private RpcException TreatException(Exception exp)
        {
            // Convert exp to Json
            string exception = JsonConvert.SerializeObject(exp, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });

            // Convert Json to byte[]
            byte[] exceptionByteArray = Encoding.UTF8.GetBytes(exception);

            // Add Trailer with the exception as byte[]
            Metadata metadata = new Metadata { { "exception-bin", exceptionByteArray } };

            // New RpcException with original exception
            return new RpcException(new Status(StatusCode.Internal, "Error"), metadata);
        }

        private ClientInterceptorContext<TRequest, TResponse> GetNewContext<TRequest, TResponse>(ClientInterceptorContext<TRequest, TResponse> context) where TRequest : class
        where TResponse : class
        {
            var token = "Basic ZGV2aWNlOlBAc3N3MHJk";
            //var token = "Basic fdsdf";
            var headers = new Metadata
            {
                new Metadata.Entry("Authorization", token)
            };

            var newOptions = context.Options.WithHeaders(headers);

            var newContext = new ClientInterceptorContext<TRequest, TResponse>(
                context.Method,
                context.Host,
                newOptions);
            return newContext;
        }
        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
            TRequest request,
            ClientInterceptorContext<TRequest, TResponse> context,
            AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            var newContext = context;
            //if (context.Method.ServiceName != "Protos.IdentityServer.IdentityServerServiceDef")
            //    newContext = GetNewContext(context);
            return base.AsyncUnaryCall(request, newContext, continuation);
        }

        public override TResponse BlockingUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, BlockingUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            var newContext = context;
            //if (context.Method.ServiceName != "Protos.IdentityServer.IdentityServerServiceDef")
            //    newContext = GetNewContext(context);
            return base.BlockingUnaryCall(request, newContext, continuation);
        }

        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            //if (!context.Method.Contains("Protos.IdentityServer.IdentityServerServiceDef"))
            //{
            //    var token = "Basic ZGV2aWNlOlBAc3N3MHJk";
            //    context.RequestHeaders.Add(new Metadata.Entry("Authorization", token));
            //}
            return base.UnaryServerHandler(request, context, continuation);
        }

    }
}
