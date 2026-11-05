using Microsoft.EntityFrameworkCore;
using ModernBank.Domain.Models.AccountManagement;

namespace ModernBank.Application.Data
{
    public interface IDbContext
    {
        DbSet<UserAccount> Accounts { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
