namespace ModernBank.Contracts.DTOs.UserManagement;

public sealed record RegisterUserRequestDto(
    CustomerType CustomerType,
    string FullName,
    string Email,
    string MobileNumber,
    IdentityDocumentType IdentityType,
    string IdentityNumber,
    string OrganizationName,
    string TaxReference,
    string Password,
    bool AcceptDemoNotice);

public sealed record LoginRequestDto(string Email, string Password);

public sealed record UpdateUserProfileDto(
    string FullName,
    string Email,
    string MobileNumber,
    string TaxReference,
    string OrganizationName,
    CustomerType CustomerType);
