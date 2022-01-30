using GringottsBank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GringottsBank.Infrastructure.Persistence.Configurations
{
    public class CustomerConfiguration : EntityBaseConfiguration<Customer>
    {
        public override void Configure(EntityTypeBuilder<Customer> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(p => p.LastName).HasMaxLength(50).IsRequired();
            builder.HasIndex(p => p.EmailAddress).IsUnique();
            builder.Property(p => p.Password).IsRequired();
            builder.ToTable("customers");
        }
    }
}
