namespace ModernBank.Contracts.Users;

public sealed record AppUserDto(
    Guid Id,
    string Email,
    string PhoneNumber,
    string IdentityNumber,
    string TaxNumber);
