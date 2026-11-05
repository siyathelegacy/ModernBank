namespace ModernBank.Domain.Models.TxManagement;

public enum MoneyMovementType
{
    Deposit,
    Transfer,
    Withdrawal
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

public sealed record MovementReceipt(string Description, decimal Amount);
