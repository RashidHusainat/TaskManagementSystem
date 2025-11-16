namespace TaskManagementSystem.Infrastructure.Persistence.Interceptors;

public class AuditableEntityInterceptor(IUserAccessor userAccessor) : SaveChangesInterceptor
{

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }


    public void UpdateEntities(DbContext? dbContext)
    {
        foreach (var entry in dbContext.ChangeTracker.Entries<AuditableEntity>().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
        {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.CreatedBy = "System";
                    //entry.Entity.CreatedBy = userAccessor.GetUserName();
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModified = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = "System";
                    //entry.Entity.LastModifiedBy = userAccessor.GetUserName();
                }
        }
    }
}
