using Grpc.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace POC.Grpc.Services.Core
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                if (Request.Headers.ContainsKey("Authorization"))
                {
                    var headerValue = Request.Headers["Authorization"].ToString();
                    if (headerValue.StartsWith("Basic"))
                    {
                        //username:password
                        var token = headerValue.Split(" ")[1];

                        var bytes = Convert.FromBase64String(token);
                        var plainText = Encoding.UTF8.GetString(bytes);

                        int seperator = plainText.IndexOf(':');
                        var username = plainText.Substring(0, seperator);
                        var password = plainText.Substring(seperator + 1);

                        if (username == "device" && password == "P@ssw0rd")
                        {
                            var claimPrincipal = new ClaimsPrincipal(
                                    new ClaimsIdentity(new List<Claim>
                                    {
                                    new Claim(ClaimTypes.Name, username),
                                    new Claim(ClaimTypes.Role, "Device")
                                    }
                                ));
                            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimPrincipal, Scheme.Name)));
                        }
                    }

                }
            }catch(Exception ex)
            {

            }
            return Task.FromResult(AuthenticateResult.Fail("UnAuthorized"));
        }
    }
}
