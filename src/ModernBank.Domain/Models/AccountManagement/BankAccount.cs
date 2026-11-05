namespace ModernBank.Domain.Models.AccountManagement;

public sealed class BankAccount
{
    public Guid UserId { get; set; }
    public Guid BankAccId { get; set; }
    public string Name { get; init; } = string.Empty;
    public string AccountNumber { get; init; } = string.Empty;
    public string Type { get; init; } = string.Empty;
    public decimal Balance { get; set; }
    public bool IsPrimary { get; init; }
    public UserAccount UserAccount { get; init; } = new UserAccount();
    public ICollection<Transaction> Transactions { get; private set; } =new HashSet<Transaction>();
}

public sealed record AllocationSettings(int InvestmentPercent, int PersonalPercent, int TaxPercent)
{
    public bool IsValid => InvestmentPercent >= 0
        && PersonalPercent >= 0
        && TaxPercent >= 0
        && InvestmentPercent + PersonalPercent + TaxPercent == 100;
}
