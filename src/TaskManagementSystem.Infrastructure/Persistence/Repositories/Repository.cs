
namespace TaskManagementSystem.Infrastructure.Persistence.Repositories;

public class Repository<T>(ApplicationDbContext dbContext) : IRepository<T> where T : class
{

    public void Add(T entity)
        => dbContext.Set<T>().Add(entity);


    public void Delete(T entity)
        => dbContext.Set<T>().Remove(entity);

    public async Task<T?> GetByIdAsync(string id)
        => await dbContext.Set<T>().FindAsync(id);

    public IQueryable<T> Query() 
        => dbContext.Set<T>();

    public void Update(T entity) 
        => dbContext.Set<T>().Update(entity);
}

