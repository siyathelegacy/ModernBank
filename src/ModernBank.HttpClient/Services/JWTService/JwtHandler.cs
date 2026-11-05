using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Xml;

namespace ModernBank.Http_Client.Services.JWTService
{
    public class JwtHandler(IserviceScopeFactory scopeFactory, ILogger<JwtHandler> logger): DelegatingHandler
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
                    request.Headers.Authorization = AuthenticationHeaderValue("Bearer", token);
              
                catch (FormatException fe)
                {
                    logger.LogWarning(fx.GetBaseException().Message, "Failed to set authorization header. Invalid token");
                }catch(Exception ex)
                {
                    logger.LogWarning(ex.GetBaseException().Message,"Failed to set authorization header.");
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
