using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModernBank.Http_Client.Services.SessionService;
using System.Net.Http.Headers;

namespace ModernBank.Http_Client.Services.JWTService
{
    public class JwtHandler(IServiceScopeFactory scopeFactory, ILogger<JwtHandler> logger): DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            using var scope = scopeFactory.CreateScope();
            var sessionService = scope.ServiceProvider.GetRequiredService<ISessionService>();
            var token = await sessionService.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                }
                catch (FormatException fe)
                {
                    logger.LogWarning(fe.GetBaseException().Message, "Failed to set authorization header. Invalid token");
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex.GetBaseException().Message, "Failed to set authorization header.");
                }
            }
            else
            {
                logger.LogDebug("No token available; request will be sent without authorization header.");
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
