using ModernBank.Domain.Models.UserManagement;

namespace ModernBank.Domain.Models.AccountManagement;

public sealed class UserAccount
{
    public Guid UserId { get; set; }
    public CustomerType? CustomerType { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string MobileNumber { get; set; } = string.Empty;
    public IdentityDocumentType? IdentityType { get; set; }
    public string IdentityNumber { get; set; } = string.Empty;
    public string OrganizationName { get; set; } = string.Empty;
    public string TaxReference { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public ICollection<BankAccount> BankAccounts { get; set;} = new HashSet<BankAccount>();
}
