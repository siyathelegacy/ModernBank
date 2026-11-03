using Microsoft.EntityFrameworkCore;
using ModernBank.Application.Data;
using ModernBank.Domain.Models.UserManager;

namespace ModernBank.Infrastructure.Data;

public sealed class ModernBankDbContext(DbContextOptions<ModernBankDbContext> options)
    : DbContext(options), IDbContext
{
    public DbSet<AppUser> AppUsers => Set<AppUser>();
}
