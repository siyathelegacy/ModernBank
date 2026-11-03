namespace ModernBank.Components;

public sealed record SpendingBar(string Label, int Height, decimal Amount);

public sealed record FinanceTransaction(
    string Name,
    string Category,
    string Date,
    string Amount,
    bool IsIncome,
    string Icon,
    string Account);
