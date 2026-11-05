namespace ModernBank.Contracts.DTOs.TxManagement;

public enum MoneyMovementType
{
    Deposit,
    Transfer,
    Withdrawal
}

public sealed record MoneyMovementDto(
    Guid Id,
    string Name,
    string Category,
    DateTime Date,
    decimal Amount,
    bool IsCredit,
    Guid? AccountId,
    string AccountName);

public sealed record IncomeAllocationDto(
    decimal GrossAmount,
    decimal InvestmentAmount,
    decimal PersonalAmount,
    decimal TaxAmount,
    DateTime Date);

public sealed record TaxActivityDto(
    Guid Id,
    string Description,
    string Reference,
    DateTime Date,
    decimal Amount,
    bool IsRefund,
    bool IsPending = false);

public sealed record SarsRefundRequestDto(
    Guid Id,
    string Taxpayer,
    string AccountType,
    string Reference,
    DateTime Submitted,
    decimal Amount,
    bool IsAuthorized = false);

public sealed record SarsPaymentReceiptDto(
    Guid Id,
    string Taxpayer,
    string AccountType,
    string TaxReference,
    string PaymentReference,
    DateTime Received,
    decimal Amount);

public sealed record MovementReceiptDto(string Description, decimal Amount);

public sealed record MoneyMovementRequestDto(
    MoneyMovementType Type,
    Guid AccountId,
    Guid? DestinationAccountId,
    decimal Amount,
    string Recipient,
    string Reference);

public sealed record RecordIncomeRequestDto(
    decimal Amount,
    Guid AccountId,
    string Source);

public sealed record UpdateTaxSettingsRequestDto(
    int InvestmentPercent,
    int TaxPercent);

public sealed record TaxPaymentRequestDto(decimal Amount, bool ConfirmPayment);
