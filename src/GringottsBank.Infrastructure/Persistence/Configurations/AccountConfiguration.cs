using GringottsBank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GringottsBank.Infrastructure.Persistence.Configurations
{
    public class AccountConfiguration : EntityBaseConfiguration<Account>
    {
        public override void Configure(EntityTypeBuilder<Account> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Balance).HasColumnType("numeric(15,2)");
            builder.ToTable("accounts");
        }
    }
}
