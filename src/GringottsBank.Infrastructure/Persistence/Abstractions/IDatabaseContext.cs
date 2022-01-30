using System;
using System.Threading;
using System.Threading.Tasks;
using GringottsBank.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GringottsBank.Infrastructure.Persistence.Abstractions
{
    public interface IDatabaseContext : IDisposable
    {
        DbSet<Account> Accounts { get; set; }
        DbSet<AuditLog> AuditLogs { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Transaction> Transactions { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        void Migrate();
    }
}
