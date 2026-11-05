using ModernBank.Domain.Models.AccountManagement;

namespace ModernBank.Application.Repositories
{
    public interface IAccountRepository
    {
        Task<UserAccount?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<UserAccount?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

        Task AddAsync(UserAccount account, CancellationToken cancellationToken = default);

        Task UpdateAsync(UserAccount account, CancellationToken cancellationToken = default);

        Task DeleteAsync(UserAccount account, CancellationToken cancellationToken = default);
    }
}
