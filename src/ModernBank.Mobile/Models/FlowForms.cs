using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using ModernBank.Domain.Models;

namespace ModernBank.Models;

public sealed class RegistrationForm : IValidatableObject
{
    [Required(ErrorMessage = "Choose the account type that best fits you.")]
    public CustomerType? CustomerType { get; set; }

    [Required(ErrorMessage = "Enter your full legal name.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Enter a name between 2 and 100 characters.")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Enter an active email address.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    [StringLength(254)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Enter a South African cellphone number.")]
    [StringLength(22)]
    public string MobileNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Choose South African ID or passport.")]
    public IdentityDocumentType? IdentityType { get; set; }

    [Required(ErrorMessage = "Enter your ID or passport number.")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "Enter a valid ID or passport number.")]
    public string IdentityNumber { get; set; } = string.Empty;

    [StringLength(100)]
    public string OrganizationName { get; set; } = string.Empty;

    [RegularExpression(@"^(?:\d{10})?$", ErrorMessage = "Enter a 10-digit tax reference number.")]
    public string TaxReference { get; set; } = string.Empty;

    [Required(ErrorMessage = "Create a password for this demo profile.")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Use at least 8 characters.")]
    public string Password { get; set; } = string.Empty;

    [Compare(nameof(Password), ErrorMessage = "The passwords do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;

    public bool AcceptDemoNotice { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var normalizedPhone = Regex.Replace(MobileNumber ?? string.Empty, @"[\s()-]", string.Empty);
        if (!Regex.IsMatch(normalizedPhone, @"^(?:\+27|0)[6-8]\d{8}$"))
        {
            yield return new ValidationResult(
                "Enter a South African cellphone number, such as 082 123 4567 or +27 82 123 4567.",
                [nameof(MobileNumber)]);
        }

        if (IdentityType == IdentityDocumentType.SouthAfricanId && !SouthAfricanIdentity.IsValid(IdentityNumber))
        {
            yield return new ValidationResult(
                "Enter a valid 13-digit South African ID number.",
                [nameof(IdentityNumber)]);
        }

        if (IdentityType == IdentityDocumentType.Passport
            && !Regex.IsMatch(IdentityNumber ?? string.Empty, @"^[A-Za-z0-9]{6,20}$"))
        {
            yield return new ValidationResult(
                "Enter a passport number using 6 to 20 letters or numbers.",
                [nameof(IdentityNumber)]);
        }

        if (CustomerType is global::ModernBank.Domain.Models.CustomerType.Organization or global::ModernBank.Domain.Models.CustomerType.PrivateCompany
            && string.IsNullOrWhiteSpace(OrganizationName))
        {
            yield return new ValidationResult(
                "Enter the registered organization or company name.",
                [nameof(OrganizationName)]);
        }

        if (!AcceptDemoNotice)
        {
            yield return new ValidationResult(
                "Confirm that you understand this is a demonstration.",
                [nameof(AcceptDemoNotice)]);
        }
    }
}

public sealed class LoginForm
{
    [Required(ErrorMessage = "Enter the email address used for this demo profile.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Enter your password.")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Use at least 8 characters.")]
    public string Password { get; set; } = string.Empty;
}

public sealed class MoneyMovementForm : IValidatableObject
{
    [Required]
    public MoneyMovementType Type { get; set; }

    [Required(ErrorMessage = "Choose the account to use.")]
    public Guid? AccountId { get; set; }

    public Guid? DestinationAccountId { get; set; }

    [Range(typeof(decimal), "1", "1000000000", ErrorMessage = "Enter an amount greater than R 0.")]
    public decimal? Amount { get; set; }

    [StringLength(100)]
    public string Recipient { get; set; } = string.Empty;

    [StringLength(120)]
    public string Reference { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Type == MoneyMovementType.Transfer
            && DestinationAccountId is null
            && string.IsNullOrWhiteSpace(Recipient))
        {
            yield return new ValidationResult("Enter a recipient or choose a destination account.", [nameof(Recipient)]);
        }

        if (Type == MoneyMovementType.Deposit && string.IsNullOrWhiteSpace(Reference))
        {
            yield return new ValidationResult("Add a short deposit note.", [nameof(Reference)]);
        }
    }
}

public sealed class ProfileForm : IValidatableObject
{
    [Required(ErrorMessage = "Enter your full legal name.")]
    [StringLength(100, MinimumLength = 2)]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Enter an active email address.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    [StringLength(254)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Enter a South African cellphone number.")]
    [StringLength(22)]
    public string MobileNumber { get; set; } = string.Empty;

    [RegularExpression(@"^(?:\d{10})?$", ErrorMessage = "Enter a 10-digit tax reference number.")]
    public string TaxReference { get; set; } = string.Empty;

    [StringLength(100)]
    public string OrganizationName { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var normalizedPhone = Regex.Replace(MobileNumber ?? string.Empty, @"[\s()-]", string.Empty);
        if (!Regex.IsMatch(normalizedPhone, @"^(?:\+27|0)[6-8]\d{8}$"))
        {
            yield return new ValidationResult("Enter a valid South African number beginning with 06, 07 or 08.", [nameof(MobileNumber)]);
        }

        if (CustomerType is CustomerType.Organization or CustomerType.PrivateCompany
            && string.IsNullOrWhiteSpace(OrganizationName))
        {
            yield return new ValidationResult("Enter the registered organization or company name.", [nameof(OrganizationName)]);
        }
    }

    public CustomerType CustomerType { get; set; }
}

public sealed class IncomeForm
{
    [Range(typeof(decimal), "1", "1000000000", ErrorMessage = "Enter an income amount greater than R 0.")]
    public decimal? Amount { get; set; }

    [Required(ErrorMessage = "Choose the account where income arrived.")]
    public Guid? AccountId { get; set; }

    [StringLength(120)]
    public string Source { get; set; } = string.Empty;
}

public sealed class SavingsAccountForm
{
    [Required(ErrorMessage = "Give your savings account a name.")]
    [StringLength(32, MinimumLength = 2, ErrorMessage = "Use a name between 2 and 32 characters.")]
    public string Name { get; set; } = string.Empty;
}

public sealed class TaxSettingsForm : IValidatableObject
{
    [Range(0, 100, ErrorMessage = "Choose a percentage between 0% and 100%.")]
    public int TaxReservePercent { get; set; } = 15;

    [Range(0, 100, ErrorMessage = "Choose a percentage between 0% and 100%.")]
    public int InvestmentPercent { get; set; } = 20;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (TaxReservePercent + InvestmentPercent > 100)
        {
            yield return new ValidationResult(
                "Tax and investment portions cannot exceed 100% of incoming income.",
                [nameof(TaxReservePercent), nameof(InvestmentPercent)]);
        }
    }
}

public sealed class TaxPaymentForm : IValidatableObject
{
    [Range(typeof(decimal), "1", "1000000000", ErrorMessage = "Enter an amount greater than R 0.")]
    public decimal? Amount { get; set; }

    public bool ConfirmPayment { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!ConfirmPayment)
        {
            yield return new ValidationResult(
                "Confirm that you want to record this simulated SARS payment.",
                [nameof(ConfirmPayment)]);
        }
    }
}

public static class SouthAfricanIdentity
{
    public static bool IsValid(string? value)
    {
        var digits = value?.Trim() ?? string.Empty;
        if (digits.Length != 13 || !digits.All(char.IsDigit))
        {
            return false;
        }

        if (!DateOnly.TryParseExact(digits[..6], "yyMMdd", out _))
        {
            return false;
        }

        var oddPositionTotal = 0;
        for (var index = 0; index < 12; index += 2)
        {
            oddPositionTotal += digits[index] - '0';
        }

        var doubledEvenPositionTotal = 0;
        for (var index = 1; index < 12; index += 2)
        {
            var doubled = (digits[index] - '0') * 2;
            doubledEvenPositionTotal += doubled / 10 + doubled % 10;
        }

        var checkDigit = (10 - (oddPositionTotal + doubledEvenPositionTotal) % 10) % 10;
        return checkDigit == digits[12] - '0';
    }
}
