using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Grpc.Services.Core
{
    public interface ITokenProvider
    {
        Task<string> GetTokenAsync();
    }
}
