using Microsoft.Extensions.DependencyInjection;
using ModernBank.Http_Client.Services.AccountService;
using ModernBank.Http_Client.Services.JWTService;

namespace ModernBank.Http_Client.Extensions
{
    public static class ModernBankHttpClient
    {
        public static string baseUrl { get; private set; } = string.Empty;

        public static IServiceCollection AddModernBankHttpClient(this IServiceCollection services)
        {
            services.AddScoped<JwtHandler>();

            static void RegisterClient<TInterface,TImplementation>(IServiceCollection s)
                where TInterface : class
                where TImplementation : class, TInterface
            {
                s.AddHttpClient<TInterface, TImplementation>((provider, client) =>
                {
                    client.BaseAddress = new Uri(baseUrl);
                }).AddHttpMessageHandler<JwtHandler>();
            }
            RegisterClient<IAccountService, AccountService>(services);

            return services;
        }
    }
}
