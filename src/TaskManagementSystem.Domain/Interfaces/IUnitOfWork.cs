


namespace TaskManagementSystem.Domain.Interfaces;

public interface IUnitOfWork
{
    IRepository<TaskItem> Tasks { get; }

    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
}

