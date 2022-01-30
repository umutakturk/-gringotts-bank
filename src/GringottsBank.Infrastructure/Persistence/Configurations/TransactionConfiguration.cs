using System;
using GringottsBank.Domain.Entities;
using GringottsBank.Domain.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GringottsBank.Infrastructure.Persistence.Configurations
{
    public class TransactionConfiguration : EntityBaseConfiguration<Transaction>
    {
        public override void Configure(EntityTypeBuilder<Transaction> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Amount)
                .HasColumnType("numeric(15,2)")
                .IsRequired();

            builder.Property(p => p.Type)
                .HasConversion(t => t.ToString(), s => (TransactionType)Enum.Parse(typeof(TransactionType), s, true))
                .IsRequired();

            builder.ToTable("transactions");
        }
    }
}
