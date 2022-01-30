using System;
using GringottsBank.Domain.Types;

namespace GringottsBank.Domain.Entities
{
    public class Transaction : EntityBase
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
    }
}
