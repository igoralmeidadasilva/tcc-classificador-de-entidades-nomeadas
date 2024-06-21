using Classificador.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Classificador.Api.Infrastructure.Interceptors;

public class SoftDeleteInterceptor : SaveChangesInterceptor
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

        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            if (entry is not { State: EntityState.Deleted, Entity: Entity<object> delete })
            {
                continue;
            }
                
            entry.State = EntityState.Modified;
            delete.Delete();
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
