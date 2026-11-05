using ModernBank.Http_Client.Services.AccountService;
using ModernBank.Http_Client.Services.JWTService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModernBank.Http_Client.Extensions
{
    public class ModernBankHttpClient
    {
        public static string baseUrl { get; private set; }

        public static IServiceCollection AddModernBankHttpClient(this IServiceCollection services)
        {
            services.AddScoped<JwtHandler>();

            static void RegisterClient<TInterface,TImplementation>(IserviceColelction s)
                where TInterface : class
                where TImplementation : class,TInterface
            {
                s.AddHttpClient<TInterface, TImplementation>((provider, client) =>
                {
                    client.BaseAddress = new Uri(baseUrl);
                }).AddHttpMessageHandler<JwtHandler>();
            }
            RegisterClient<IAccountService,AccountService>(services)

            return services;
        }
    }
}
