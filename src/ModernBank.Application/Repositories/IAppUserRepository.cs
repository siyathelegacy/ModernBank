using ModernBank.Domain.Models.UserManager;

namespace ModernBank.Application.Repositories;

public interface IAppUserRepository
{
    Task<AppUser?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<AppUser?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task AddAsync(AppUser user, CancellationToken cancellationToken = default);

    Task UpdateAsync(AppUser user, CancellationToken cancellationToken = default);

    Task DeleteAsync(AppUser user, CancellationToken cancellationToken = default);
}
