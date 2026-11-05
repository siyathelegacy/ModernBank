using Microsoft.EntityFrameworkCore;
using ModernBank.Application.Data;
using ModernBank.Application.Repositories;
using ModernBank.Domain.Models.AccountManagement;

namespace ModernBank.Infrastructure.Repositories;

public sealed class AccountRepository(IDbContext dbContext) : IAccountRepository
{
    public Task<UserAccount?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        dbContext.Accounts.FirstOrDefaultAsync(account => account.UserId == id, cancellationToken);

    public Task<UserAccount?> GetByEmailAsync(string email, CancellationToken cancellationToken = default) =>
        dbContext.Accounts.FirstOrDefaultAsync(account => account.Email == email, cancellationToken);

    public async Task AddAsync(UserAccount account, CancellationToken cancellationToken = default)
    {
        await dbContext.Accounts.AddAsync(account, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(UserAccount account, CancellationToken cancellationToken = default)
    {
        dbContext.Accounts.Update(account);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(UserAccount account, CancellationToken cancellationToken = default)
    {
        dbContext.Accounts.Remove(account);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
