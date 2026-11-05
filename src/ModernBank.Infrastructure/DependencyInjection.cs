using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModernBank.Application.Data;
using ModernBank.Application.Repositories;
using ModernBank.Infrastructure.Data;
using ModernBank.Infrastructure.Repositories;

namespace ModernBank.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ModernBankDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("ModernBank");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "Configure the database using the ConnectionStrings:ModernBank setting.");
            }

            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IDbContext>(provider => provider.GetRequiredService<ModernBankDbContext>());
        services.AddScoped<IAccountRepository, AccountRepository>();

        return services;
    }
}
