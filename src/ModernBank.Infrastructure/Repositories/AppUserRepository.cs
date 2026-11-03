using Microsoft.EntityFrameworkCore;
using ModernBank.Application.Data;
using ModernBank.Application.Repositories;
using ModernBank.Domain.Models.UserManager;

namespace ModernBank.Infrastructure.Repositories;

public sealed class AppUserRepository(IDbContext dbContext) : IAppUserRepository
{
    public Task<AppUser?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        dbContext.AppUsers.FirstOrDefaultAsync(user => user.Id == id, cancellationToken);

    public Task<AppUser?> GetByEmailAsync(string email, CancellationToken cancellationToken = default) =>
        dbContext.AppUsers.FirstOrDefaultAsync(user => user.Email == email, cancellationToken);

    public async Task AddAsync(AppUser user, CancellationToken cancellationToken = default)
    {
        await dbContext.AppUsers.AddAsync(user, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AppUser user, CancellationToken cancellationToken = default)
    {
        dbContext.AppUsers.Update(user);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(AppUser user, CancellationToken cancellationToken = default)
    {
        dbContext.AppUsers.Remove(user);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
