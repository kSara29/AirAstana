using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence.Interceptors;

public sealed class EfInterceptor : SaveChangesInterceptor
{
    private readonly ILogger<EfInterceptor> _logger;

    public EfInterceptor(ILogger<EfInterceptor> logger)
    {
        _logger = logger;
    }
    
     public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        var ctx = eventData.Context;
        if (ctx is null) return base.SavingChanges(eventData, result);

        var entries = ctx.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added
                     || e.State == EntityState.Modified
                     || e.State == EntityState.Deleted)
            .ToList();

        foreach (var e in entries)
        {
            var entity = e.Entity.GetType().Name;
            var key = TryGetKey(e);

            if (e.State == EntityState.Added)
            {
                _logger.LogInformation("DB ADD {Entity} Id={Key} Values={Values}",
                    entity, key, DumpValues(e.CurrentValues));
            }
            else if (e.State == EntityState.Deleted)
            {
                _logger.LogInformation("DB DELETE {Entity} Id={Key} Values={Values}",
                    entity, key, DumpValues(e.OriginalValues));
            }
            else if (e.State == EntityState.Modified)
            {
                var changes = DumpChanges(e);
                if (!string.IsNullOrEmpty(changes))
                    _logger.LogInformation("DB UPDATE {Entity} Id={Key} Changes={Changes}",
                        entity, key, changes);
            }
        }

        return base.SavingChanges(eventData, result);
    }

    private static string TryGetKey(EntityEntry e)
    {
        var idProp = e.Metadata.FindPrimaryKey()?.Properties;
        if (idProp is not null && idProp.Count > 0)
        {
            var parts = idProp.Select(p => $"{p.Name}={e.Property(p.Name).CurrentValue}");
            return string.Join(",", parts);
        }
        return "<no-key>";
    }

    private static string DumpValues(PropertyValues values)
    {
        var sb = new StringBuilder();
        foreach (var p in values.Properties)
        {
            var val = values[p];
            sb.Append(p.Name).Append('=').Append(val ?? "null").Append(';');
        }
        return sb.ToString();
    }

    private static string DumpChanges(EntityEntry e)
    {
        var sb = new StringBuilder();
        foreach (var p in e.Properties.Where(p => p.IsModified))
        {
            sb.Append(p.Metadata.Name)
              .Append(':')
              .Append(p.OriginalValue ?? "null")
              .Append(" -> ")
              .Append(p.CurrentValue ?? "null")
              .Append("; ");
        }
        return sb.ToString();
    }
}