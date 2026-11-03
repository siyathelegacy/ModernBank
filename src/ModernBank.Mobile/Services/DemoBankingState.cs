using System.Globalization;
using ModernBank.Domain.Models;
using ModernBank.Models;

namespace ModernBank.Services;

public sealed class DemoBankingState
{
    private readonly List<BankAccount> _accounts =
    [
        new()
        {
            Id = Guid.Parse("ad058a12-ae5a-4210-8786-701e8a4c9651"),
            Name = "Everyday account",
            AccountNumber = "•••• 4829",
            Type = "Primary",
            Balance = 18_640.50m,
            IsPrimary = true
        },
        new()
        {
            Id = Guid.Parse("42bbff81-223f-420f-b267-0ffec12b6194"),
            Name = "Rainy day savings",
            AccountNumber = "•••• 2196",
            Type = "Savings",
            Balance = 6_220m
        },
        new()
        {
            Id = Guid.Parse("c34d1bf0-001a-48b3-a420-46bdfe2c8854"),
            Name = "Investment pocket",
            AccountNumber = "•••• 0834",
            Type = "Investment",
            Balance = 9_880m
        }
    ];

    private readonly List<MoneyMovement> _activity =
    [
        new(Guid.NewGuid(), "Salary", "Personal income", DateTime.Today.AddHours(-2), 32_500m, true, Guid.Parse("ad058a12-ae5a-4210-8786-701e8a4c9651"), "Everyday account"),
        new(Guid.NewGuid(), "Woolworths", "Groceries", DateTime.Today.AddHours(-6), -1_284.50m, false, Guid.Parse("ad058a12-ae5a-4210-8786-701e8a4c9651"), "Everyday account"),
        new(Guid.NewGuid(), "SARS tax reserve", "Tax allocation", DateTime.Today.AddDays(-1), -4_875m, false, null, "SARS Balance"),
        new(Guid.NewGuid(), "Monthly savings", "Investment", DateTime.Today.AddDays(-2), -6_500m, false, Guid.Parse("c34d1bf0-001a-48b3-a420-46bdfe2c8854"), "Investment pocket"),
        new(Guid.NewGuid(), "Discovery Medical", "Medical", DateTime.Today.AddDays(-3), -2_450m, false, Guid.Parse("ad058a12-ae5a-4210-8786-701e8a4c9651"), "Everyday account")
    ];

    private readonly List<IncomeAllocation> _incomeAllocations =
    [
        new(32_500m, 6_500m, 21_125m, 4_875m, DateTime.Today.AddHours(-2)),
        new(18_000m, 3_600m, 11_700m, 2_700m, DateTime.Today.AddDays(-9)),
        new(22_000m, 4_400m, 14_300m, 3_300m, DateTime.Today.AddDays(-26))
    ];

    private readonly List<TaxActivity> _taxActivities =
    [
        new(Guid.NewGuid(), "Tax reserve added", "Income allocation", DateTime.Today.AddHours(-2), 4_875m, false),
        new(Guid.NewGuid(), "2025 provisional payment", "SARS-PR-2025-01", DateTime.Today.AddMonths(-2), 2_300m, false),
        new(Guid.NewGuid(), "Tax refund received", "SARS-RF-2024-06", DateTime.Today.AddMonths(-5), 620m, true)
    ];

    private readonly List<SarsRefundRequest> _refundRequests =
    [
        new(Guid.NewGuid(), "Cape Studio (Pty) Ltd", "Private company", "TX-486-220-91", DateTime.Today.AddDays(-2), 8_420m),
        new(Guid.NewGuid(), "Nomsa Dlamini", "Individual", "TX-114-780-63", DateTime.Today.AddDays(-1), 1_850m),
        new(Guid.NewGuid(), "Ubuntu Learning Trust", "Organization", "TX-937-441-20", DateTime.Today, 12_600m)
    ];

    private readonly List<SarsPaymentReceipt> _sarsPayments =
    [
        new(Guid.NewGuid(), "Nomsa Dlamini", "Individual", "1010101010", "DEMO-SARS-62831", DateTime.Today.AddHours(-2), 4_875m),
        new(Guid.NewGuid(), "Cape Studio (Pty) Ltd", "Private company", "2020202020", "DEMO-SARS-49106", DateTime.Today.AddDays(-1), 18_400m),
        new(Guid.NewGuid(), "Ubuntu Learning Trust", "Organization", "3030303030", "DEMO-SARS-77352", DateTime.Today.AddDays(-3), 9_250m)
    ];

    public event Action? Changed;

    public string FullName { get; private set; } = "Maya Chen";
    public string Email { get; private set; } = "maya@example.co.za";
    public string MobileNumber { get; private set; } = "082 123 4567";
    public string OrganizationName { get; private set; } = string.Empty;
    public string TaxReference { get; private set; } = "9123456789";
    public CustomerType CustomerType { get; private set; } = CustomerType.Individual;
    public bool IsAuthenticated { get; private set; }
    public bool IsSarsAdmin { get; private set; }
    public string? Notice { get; private set; }
    public bool IsBalanceVisible { get; set; } = true;
    public AllocationSettings Allocation { get; private set; } = new(20, 65, 15);
    public decimal SarsBalance { get; private set; } = 10_875m;
    public decimal TaxableIncomeThisYear { get; private set; } = 132_500m;
    public decimal TaxPaidToSars { get; private set; } = 2_300m;

    public IReadOnlyList<BankAccount> Accounts => _accounts.AsReadOnly();
    public IReadOnlyList<MoneyMovement> Activity => _activity.AsReadOnly();
    public IReadOnlyList<IncomeAllocation> IncomeAllocations => _incomeAllocations.AsReadOnly();
    public IReadOnlyList<TaxActivity> TaxActivities => _taxActivities.AsReadOnly();
    public IReadOnlyList<SarsRefundRequest> RefundRequests => _refundRequests.AsReadOnly();
    public IReadOnlyList<SarsPaymentReceipt> SarsPayments => _sarsPayments.AsReadOnly();
    public decimal AvailableBalance => _accounts.Where(account => account.Type != "Investment").Sum(account => account.Balance);
    public decimal InvestmentBalance => _accounts.Where(account => account.Type == "Investment").Sum(account => account.Balance);
    public decimal TotalMoney => AvailableBalance + InvestmentBalance + SarsBalance;
    public decimal EstimatedTaxRate { get; private set; } = 15m;
    public decimal EstimatedTaxLiability => Math.Max(0, TaxableIncomeThisYear * EstimatedTaxRate / 100);
    public decimal EstimatedTaxGap => Math.Max(0, EstimatedTaxLiability - TaxPaidToSars - SarsBalance);
    public decimal EstimatedTaxSurplus => Math.Max(0, SarsBalance + TaxPaidToSars - EstimatedTaxLiability);
    public string Currency => "ZAR";

    public void CompleteRegistration(RegistrationForm form)
    {
        FullName = form.FullName.Trim();
        Email = form.Email.Trim();
        MobileNumber = NormalizeSouthAfricanPhone(form.MobileNumber);
        TaxReference = form.TaxReference.Trim();
        OrganizationName = form.OrganizationName.Trim();
        CustomerType = form.CustomerType ?? CustomerType.Individual;
        IsAuthenticated = true;
        IsSarsAdmin = false;
        SetNotice("Your demo profile is ready. No real account has been opened.");
    }

    public void SignInDemo(string email)
    {
        FullName = "Maya Chen";
        Email = email.Trim();
        MobileNumber = "082 123 4567";
        OrganizationName = string.Empty;
        TaxReference = "9123456789";
        CustomerType = CustomerType.Individual;
        IsAuthenticated = true;
        IsSarsAdmin = false;
        SetNotice("You’re signed in to the local demonstration profile.");
    }

    public void SignInSarsAdminDemo()
    {
        FullName = "SARS review officer";
        Email = "sars.review@example.gov.za";
        MobileNumber = string.Empty;
        OrganizationName = string.Empty;
        TaxReference = string.Empty;
        CustomerType = CustomerType.Organization;
        IsAuthenticated = true;
        IsSarsAdmin = true;
        SetNotice("SARS administrator preview is simulated and has no access to SARS systems.");
    }

    public void SignOut()
    {
        IsAuthenticated = false;
        IsSarsAdmin = false;
        SetNotice("You have signed out of the demo.");
    }

    public void ClearNotice()
    {
        Notice = null;
        Changed?.Invoke();
    }

    public void UpdateProfile(string fullName, string email, string mobile, string taxReference, string organizationName)
    {
        FullName = fullName.Trim();
        Email = email.Trim();
        MobileNumber = NormalizeSouthAfricanPhone(mobile);
        TaxReference = taxReference.Trim();
        OrganizationName = organizationName.Trim();
        SetNotice("Demo profile details saved on this device.");
    }

    public void UpdateAllocation(int investmentPercent, int taxPercent)
    {
        var personalPercent = 100 - investmentPercent - taxPercent;
        if (investmentPercent is < 0 or > 100 || taxPercent is < 0 or > 100 || personalPercent < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(investmentPercent), "Income allocation percentages must total 100%.");
        }

        Allocation = new AllocationSettings(investmentPercent, personalPercent, taxPercent);
        SetNotice("Your demo income allocation was updated.");
    }

    public IncomeAllocation AllocateIncome(decimal grossAmount, Guid sourceAccountId, string source)
    {
        if (grossAmount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(grossAmount), "Income must be greater than zero.");
        }

        var sourceAccount = FindAccount(sourceAccountId);
        var tax = decimal.Round(grossAmount * Allocation.TaxPercent / 100m, 2, MidpointRounding.ToEven);
        var investment = decimal.Round(grossAmount * Allocation.InvestmentPercent / 100m, 2, MidpointRounding.ToEven);
        var personal = grossAmount - tax - investment;
        var investmentAccount = _accounts.SingleOrDefault(account => account.Type == "Investment")
            ?? AddAccount("Investment pocket", "Investment", 0);

        investmentAccount.Balance += investment;
        sourceAccount.Balance += personal;
        SarsBalance += tax;
        TaxableIncomeThisYear += grossAmount;

        var allocation = new IncomeAllocation(grossAmount, investment, personal, tax, DateTime.Now);
        _incomeAllocations.Insert(0, allocation);
        _activity.Insert(0, new MoneyMovement(
            Guid.NewGuid(),
            string.IsNullOrWhiteSpace(source) ? "Incoming income" : source.Trim(),
            "Income received",
            DateTime.Now,
            grossAmount,
            true,
            sourceAccount.Id,
            sourceAccount.Name));
        AddActivity("Investment allocation", "Investment", -investment, investmentAccount);
        AddActivity("SARS tax reserve", "Tax allocation", -tax, null);
        _taxActivities.Insert(0, new TaxActivity(Guid.NewGuid(), "Tax reserve added", "Income allocation", DateTime.Now, tax, false));
        SetNotice($"Income split: {Money(personal)} available, {Money(investment)} invested and {Money(tax)} reserved for SARS.");
        return allocation;
    }

    public BankAccount CreateSavingsAccount(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Enter a name for the savings account.", nameof(name));
        }

        if (_accounts.Any(account => account.Type == "Savings"
            && account.Name.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException("A savings account with that name already exists.");
        }

        var account = AddAccount(name.Trim(), "Savings", 0);
        SetNotice($"“{account.Name}” is ready. Its starting balance is R 0,00.");
        return account;
    }

    public MovementReceipt MoveMoney(MoneyMovementForm form)
    {
        if (form.AccountId is null || form.Amount is null || form.Amount <= 0)
        {
            throw new ArgumentException("Choose an account and enter a valid amount.");
        }

        var account = FindAccount(form.AccountId.Value);
        var amount = decimal.Round(form.Amount.Value, 2, MidpointRounding.ToEven);
        var destination = form.Type == MoneyMovementType.Transfer && form.DestinationAccountId is { } destinationId
            ? _accounts.SingleOrDefault(candidate => candidate.Id == destinationId)
                ?? throw new InvalidOperationException("Choose a destination account that still exists.")
            : null;
        if (destination?.Id == account.Id)
        {
            throw new InvalidOperationException("Choose a different destination account.");
        }

        switch (form.Type)
        {
            case MoneyMovementType.Deposit:
                account.Balance += amount;
                AddActivity(string.IsNullOrWhiteSpace(form.Reference) ? "Demo deposit" : form.Reference.Trim(), "Deposit", amount, account);
                break;
            case MoneyMovementType.Withdrawal:
                EnsureSufficientFunds(account, amount);
                account.Balance -= amount;
                AddActivity("Cash withdrawal", "Withdrawal", -amount, account);
                break;
            case MoneyMovementType.Transfer:
                EnsureSufficientFunds(account, amount);
                account.Balance -= amount;
                if (destination is not null)
                {
                    destination.Balance += amount;
                }

                var recipient = destination?.Name ?? form.Recipient.Trim();
                AddActivity($"Transfer to {recipient}", "Transfer", -amount, account);
                if (destination is not null)
                {
                    AddActivity($"Transfer from {account.Name}", "Transfer received", amount, destination);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(form.Type), "Choose a supported money movement.");
        }

        var description = form.Type switch
        {
            MoneyMovementType.Deposit => "Demo deposit recorded",
            MoneyMovementType.Withdrawal => "Demo withdrawal recorded",
            _ => "Demo transfer recorded"
        };
        SetNotice($"{description}: {Money(amount)}. No external payment was made.");
        return new MovementReceipt(description, amount);
    }

    public TaxActivity PaySars(decimal amount)
    {
        if (IsSarsAdmin)
        {
            throw new InvalidOperationException("The SARS administrator preview cannot use taxpayer payment actions.");
        }

        if (amount <= 0 || amount > SarsBalance)
        {
            throw new InvalidOperationException("Choose an amount available in the frozen SARS Balance.");
        }

        SarsBalance -= amount;
        TaxPaidToSars += amount;
        var reference = $"DEMO-{DateTime.Now:yyMMdd}-{Guid.NewGuid():N}"[..18].ToUpperInvariant();
        var payment = new TaxActivity(Guid.NewGuid(), "Simulated SARS payment", reference, DateTime.Now, amount, false);
        _taxActivities.Insert(0, payment);
        AddActivity("SARS payment (demo)", "Tax payment", -amount, null);
        SetNotice($"Demo SARS payment of {Money(amount)} recorded. No payment was sent to SARS.");
        return payment;
    }

    public void UpdateTaxEstimateRate(decimal rate)
    {
        if (rate is < 1 or > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(rate), "Choose a tax reserve estimate between 1% and 100%.");
        }

        EstimatedTaxRate = rate;
        SetNotice($"Illustrative estimate updated to {rate:0.##}% of recorded taxable income.");
    }

    public SarsRefundRequest AuthorizeRefund(Guid requestId)
    {
        if (!IsSarsAdmin)
        {
            throw new UnauthorizedAccessException("Only the SARS administrator preview can authorize demo refund requests.");
        }

        var index = _refundRequests.FindIndex(request => request.Id == requestId);
        if (index < 0)
        {
            throw new KeyNotFoundException("The refund request no longer exists.");
        }

        var request = _refundRequests[index];
        if (request.IsAuthorized)
        {
            throw new InvalidOperationException("This demo refund request has already been authorized.");
        }

        var authorized = request with { IsAuthorized = true };
        _refundRequests[index] = authorized;
        SetNotice($"Demo refund {request.Reference} marked as authorized. No refund was paid.");
        return authorized;
    }

    public string ConsumeNotice()
    {
        var notice = Notice ?? string.Empty;
        Notice = null;
        return notice;
    }

    public static string Money(decimal amount) =>
        $"R {amount.ToString("#,##0.00;−#,##0.00", CultureInfo.GetCultureInfo("en-ZA"))}";

    public static string CustomerTypeName(CustomerType type) => type switch
    {
        CustomerType.Individual => "Individual",
        CustomerType.Organization => "Organization",
        CustomerType.PrivateCompany => "Business / private company",
        _ => "Individual"
    };

    private BankAccount AddAccount(string name, string type, decimal balance)
    {
        var account = new BankAccount
        {
            Id = Guid.NewGuid(),
            Name = name,
            AccountNumber = $"•••• {Random.Shared.Next(1000, 9999)}",
            Type = type,
            Balance = balance
        };
        _accounts.Add(account);
        return account;
    }

    private BankAccount FindAccount(Guid id) =>
        _accounts.SingleOrDefault(account => account.Id == id && account.Type != "Investment")
        ?? throw new InvalidOperationException("Choose an available account. Investment and SARS reserves cannot be withdrawn from this flow.");

    private static void EnsureSufficientFunds(BankAccount account, decimal amount)
    {
        if (amount > account.Balance)
        {
            throw new InvalidOperationException($"Not enough available money in {account.Name}. Check the balance and try a smaller amount.");
        }
    }

    private void AddActivity(string name, string category, decimal amount, BankAccount? account)
    {
        _activity.Insert(0, new MoneyMovement(
            Guid.NewGuid(),
            name,
            category,
            DateTime.Now,
            amount,
            amount > 0,
            account?.Id,
            account?.Name ?? "SARS Balance"));
    }

    private void SetNotice(string message)
    {
        Notice = message;
        Changed?.Invoke();
    }

    private static string NormalizeSouthAfricanPhone(string value)
    {
        var digits = new string(value.Where(char.IsDigit).ToArray());
        if (digits.StartsWith("27", StringComparison.Ordinal) && digits.Length == 11)
        {
            return $"+{digits[..2]} {digits[2..4]} {digits[4..7]} {digits[7..]}";
        }

        return digits.Length == 10
            ? $"{digits[..3]} {digits[3..6]} {digits[6..]}"
            : value.Trim();
    }
}
