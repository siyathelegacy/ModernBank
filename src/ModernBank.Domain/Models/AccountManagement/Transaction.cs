using System;
using System.Collections.Generic;
using System.Text;

namespace ModernBank.Domain.Models.AccountManagement
{
    public class Transaction
    {
        public Guid TransactionId { get; private set; }

        public Guid BankAccId {  get; private set; }
        public DateTime CreatedAt { get; private set; }
        public String Type { get; private set;  }
        public decimal Amount { get; private set; }
        public string Destination { get; private set; } = string.Empty;
        public BankAccount BankAccount { get; private set; } = new BankAccount();

    }
}
