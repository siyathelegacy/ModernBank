namespace ModernBank.Contracts.DTOs.AccountManagement;

public sealed record BankAccountDto(
    Guid Id,
    string Name,
    string AccountNumber,
    string Type,
    decimal Balance,
    bool IsPrimary);

public sealed record AllocationSettingsDto(
    int InvestmentPercent,
    int PersonalPercent,
    int TaxPercent);

public sealed record CreateSavingsAccountRequestDto(string Name);
