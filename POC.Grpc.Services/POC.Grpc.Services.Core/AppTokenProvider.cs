using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Grpc.Services.Core
{
    public class AppTokenProvider : ITokenProvider
    {
        private string _token;

        public async Task<string> GetTokenAsync()
        {
            if (_token == null)
            {
                // App code to resolve the token here.
                _token = "ZGV2aWNlOlBAc3N3MHJk";
            }

            return _token;
        }
    }
}
