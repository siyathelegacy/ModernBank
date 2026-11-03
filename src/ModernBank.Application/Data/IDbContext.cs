using Microsoft.EntityFrameworkCore;
using ModernBank.Domain.Models.UserManager;

namespace ModernBank.Application.Data
{
    public interface IDbContext
    {
        DbSet<AppUser> AppUsers { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
