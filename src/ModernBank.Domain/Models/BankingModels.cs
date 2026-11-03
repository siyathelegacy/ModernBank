namespace ModernBank.Domain.Models;

public enum CustomerType
{
    Individual,
    Organization,
    PrivateCompany
}

public enum IdentityDocumentType
{
    SouthAfricanId,
    Passport
}

public enum MoneyMovementType
{
    Deposit,
    Transfer,
    Withdrawal
}

public sealed class BankAccount
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string AccountNumber { get; init; }
    public required string Type { get; init; }
    public decimal Balance { get; set; }
    public bool IsPrimary { get; init; }
}

public sealed record MoneyMovement(
    Guid Id,
    string Name,
    string Category,
    DateTime Date,
    decimal Amount,
    bool IsCredit,
    Guid? AccountId,
    string AccountName);

public sealed record IncomeAllocation(
    decimal GrossAmount,
    decimal InvestmentAmount,
    decimal PersonalAmount,
    decimal TaxAmount,
    DateTime Date);

public sealed record TaxActivity(
    Guid Id,
    string Description,
    string Reference,
    DateTime Date,
    decimal Amount,
    bool IsRefund,
    bool IsPending = false);

public sealed record SarsRefundRequest(
    Guid Id,
    string Taxpayer,
    string AccountType,
    string Reference,
    DateTime Submitted,
    decimal Amount,
    bool IsAuthorized = false);

public sealed record SarsPaymentReceipt(
    Guid Id,
    string Taxpayer,
    string AccountType,
    string TaxReference,
    string PaymentReference,
    DateTime Received,
    decimal Amount);

public sealed record AllocationSettings(int InvestmentPercent, int PersonalPercent, int TaxPercent)
{
    public bool IsValid => InvestmentPercent >= 0
        && PersonalPercent >= 0
        && TaxPercent >= 0
        && InvestmentPercent + PersonalPercent + TaxPercent == 100;
}

public sealed record MovementReceipt(string Description, decimal Amount);
