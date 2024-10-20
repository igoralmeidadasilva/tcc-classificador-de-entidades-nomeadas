using Classificador.Api.Domain.Core.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Classificador.Api.Infrastructure.Interceptors;

public sealed class SoftDeleteInterceptor : SaveChangesInterceptor, ISingletonInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
        {
            return base.SavingChangesAsync(
                eventData, result, cancellationToken);
        }

        IEnumerable<EntityEntry<ISoftDeletableEntity>> entries =
            eventData
                .Context
                .ChangeTracker
                .Entries<ISoftDeletableEntity>()
                .Where(e => e.State == EntityState.Deleted);

        foreach (EntityEntry<ISoftDeletableEntity> softDeletable in entries)
        {
            softDeletable.State = EntityState.Modified;
            softDeletable.Entity.Delete();
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
