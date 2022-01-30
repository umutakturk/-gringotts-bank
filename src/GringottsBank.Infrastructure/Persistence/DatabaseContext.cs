using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GringottsBank.Common.Extensions;
using GringottsBank.Domain.Entities;
using GringottsBank.Infrastructure.Identity.Abstractions;
using GringottsBank.Infrastructure.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GringottsBank.Infrastructure.Persistence
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        private readonly IUserContext _userContext;

        public DatabaseContext(DbContextOptions<DatabaseContext> options, IUserContext userContext)
            : base(options)
        {
            _userContext = userContext;
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public void Migrate() => Database.Migrate();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
            var auditLogs = new List<AuditLog>();

            foreach (var modifiedEntity in entities)
            {
                if (modifiedEntity.Entity is IEntity entity)
                {
                    var now = DateTime.Now;
                    switch (modifiedEntity.State)
                    {
                        case EntityState.Added:
                            entity.CreatedAt = now;
                            entity.LastUpdatedAt = now;
                            break;
                        case EntityState.Modified:
                            Entry(entity).Property(x => x.CreatedAt).IsModified = false;
                            entity.LastUpdatedAt = now;
                            break;
                    }
                }
                auditLogs.Add(CreateAuditLog(modifiedEntity));
            }

            AuditLogs.AddRange(auditLogs);
            return await base.SaveChangesAsync(cancellationToken);
        }

        private AuditLog CreateAuditLog(EntityEntry entry)
        {
            var oldValues = new Dictionary<string, object>();
            var newValues = new Dictionary<string, object>();

            foreach (var property in entry.Properties)
            {
                var propertyName = property.Metadata.Name;

                switch (entry.State)
                {
                    case EntityState.Added:
                        newValues[propertyName] = property.CurrentValue;
                        break;
                    case EntityState.Modified when property.IsModified:
                        oldValues[propertyName] = property.OriginalValue;
                        newValues[propertyName] = property.CurrentValue;
                        break;
                }
            }

            return new AuditLog
            {
                UserId = _userContext.CurrentUserId,
                TableName = entry.Metadata.GetTableName(),
                Action = entry.State.ToString(),
                OldValues = oldValues.Any() ? oldValues.ToJson() : null,
                NewValues = newValues.Any() ? newValues.ToJson() : null,
                CreatedAt = DateTime.Now
            };
        }
    }
}
