using System.Collections.Generic;
using GringottsBank.Domain.Exceptions;
using GringottsBank.Domain.Types;

namespace GringottsBank.Domain.Entities
{
    public class Account : EntityBase
    {
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

        public Account Deposit(decimal amount)
        {
            if (amount <= decimal.Zero)
            {
                throw new DomainException("Invalid deposit amount.");
            }

            Balance += amount;

            Transactions.Add(new Transaction
            {
                AccountId = Id,
                Type = TransactionType.Deposit,
                Amount = amount
            });

            return this;
        }

        public Account Withdraw(decimal amount)
        {
            if (Balance < amount)
            {
                throw new DomainException("Insufficient balance.");
            }

            if (amount <= decimal.Zero)
            {
                throw new DomainException("Invalid withdraw amount");
            }

            Balance -= amount;

            Transactions.Add(new Transaction
            {
                AccountId = Id,
                Type = TransactionType.Withdraw,
                Amount = amount
            });

            return this;
        }
    }
}
