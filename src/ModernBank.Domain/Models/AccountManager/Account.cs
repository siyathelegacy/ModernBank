using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace ModernBank.Domain.Models.AccountManager
{
    public class Account
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
    }
}
