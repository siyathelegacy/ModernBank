using ModernBank.Contracts.DTOs.UserManagement;

namespace ModernBank.Contracts.DTOs.AccountManagement
{
    public sealed class AccountDto
    {
        public Guid Id { get; init; }
        public CustomerType? CustomerType { get; init; }
        public string FullName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string MobileNumber { get; init; } = string.Empty;
        public IdentityDocumentType? IdentityType { get; init; }
        public string IdentityNumber { get; init; } = string.Empty;
        public string OrganizationName { get; init; } = string.Empty;
        public string TaxReference { get; init; } = string.Empty;
    }
}
