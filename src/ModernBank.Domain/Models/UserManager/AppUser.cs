using System;
using System.Collections.Generic;
using System.Text;

namespace ModernBank.Domain.Models.UserManager
{
    public class AppUser
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set;  } = string.Empty;
        public string IdentityNumber { get; set; } = string.Empty;
        public string TaxNumber { get; set;  } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

    }
}
