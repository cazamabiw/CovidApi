using CovidApi.Data;
using Microsoft.EntityFrameworkCore;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly CovidDbContext _context;
    private readonly DbSet<T> _entities;

    public Repository(CovidDbContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }

    public IEnumerable<T> GetAll()
    {
        return _entities.ToList();
    }

    public T GetById(int id)
    {
        return _entities.Find(id);
    }

    public void Add(T entity)
    {
        _entities.Add(entity);
    }

    public void Update(T entity)
    {
        _entities.Update(entity);
    }

    public void Delete(int id)
    {
        var entity = _entities.Find(id);
        if (entity != null)
            _entities.Remove(entity);
    }
}
