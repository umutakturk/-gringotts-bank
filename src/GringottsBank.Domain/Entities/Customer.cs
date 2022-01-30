using System.Collections.Generic;

namespace GringottsBank.Domain.Entities
{
    public class Customer : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
