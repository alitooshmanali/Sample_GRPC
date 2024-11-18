using Application;
using Domain;
using Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Infrastructure.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WriteDbContext dbContext;

        private readonly IMediator mediator;

        private readonly ISystemDateTime systemDateTime;

        public UnitOfWork(WriteDbContext dbContext,
            IMediator mediator,
            ISystemDateTime systemDateTime)
        {
            this.dbContext = dbContext;
            this.mediator = mediator;
            this.systemDateTime = systemDateTime;
        }

        public Task BeginTransaction(CancellationToken cancellationToken = default)
        {
            return dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransaction(CancellationToken cancellationToken = default)
        {
            await PublishDomainEvent(cancellationToken).ConfigureAwait(false);

            await dbContext.Database.CommitTransactionAsync(cancellationToken).ConfigureAwait(false);
        }        

        public Task RollbackTransaction(CancellationToken cancellationToken = default)
        {
            return dbContext.Database.RollbackTransactionAsync(cancellationToken);
        }

        private async Task PublishDomainEvent(CancellationToken cancellationToken)
        {
            while (true)
            {
                var entries = dbContext.ChangeTracker.Entries<Entity>().ToList();

                if (entries.Any(i => i.State == EntityState.Deleted && !i.Entity.CanBeDeleted()))
                    throw new InvalidOperationException();

                SetAuditingProperties();
                WriteAudits();

                var domainEvents = entries
                   .Where(i => i.Entity.DomainEvents.Any())
                   .SelectMany(i => i.Entity.DomainEvents)
                   .ToArray();

                entries.ForEach(i => i.Entity.ClearEvents());

                await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                if (!domainEvents.Any())
                    break;

                foreach (var domainEvent in domainEvents)
                    await mediator.Publish(domainEvent, cancellationToken).ConfigureAwait(false);
            }

            await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        private void SetAuditingProperties()
        {
            var entries = dbContext.ChangeTracker
                .Entries<Entity>()
                .Where(i => i.State == EntityState.Added || i.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                var versionProperty = entry.Property("Version");
                versionProperty.CurrentValue = (int)versionProperty.OriginalValue + 1;

                if (entry.State == EntityState.Added)
                {
                    entry.Property("Created").CurrentValue = systemDateTime.UtcNow;
                }

                if (entry.State != EntityState.Modified)
                    continue;

                entry.Property("Updated").CurrentValue = systemDateTime.UtcNow;
            }
        }

        private void WriteAudits()
        {
            var domainEvents = dbContext.ChangeTracker.Entries<Entity>()
                .Where(i => i.Entity.DomainEvents.Any())
                .SelectMany(i => i.Entity.DomainEvents)
                .ToArray();

            foreach (var domainEvent in domainEvents)
                dbContext.Audits.Add(new()
                {
                    Id = Guid.NewGuid(),
                    AggregateId = domainEvent.AggregateId,
                    Action = domainEvent.GetType().Name,
                    Time = domainEvent.EventTime,
                    Data = JsonSerializer.Serialize(domainEvent.Flatten())
                });
        }
    }
}
