
namespace TaskManagementSystem.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    IQueryable<T> Query();
    Task<T?> GetByIdAsync(string id);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}

