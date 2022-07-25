using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StocksData.Data.Db;
using StocksData.Domain.Abstractions;

namespace StocksData.Data.Repositories;

public abstract class Repository<T> : IRepository<T> where T : class
{
    protected readonly DataContext Db;
    protected readonly DbSet<T> DbSet;

    protected Repository(DataContext context)
    {
        Db = context;
        DbSet = Db.Set<T>();
    }

    public void Delete(T? entity)
    {
        if (entity is { }) DbSet.Remove(entity);
    }

    public void Update(T? item)
    {
        if (item is null) return;
        Db.Entry(item).State = EntityState.Modified;
    }

    public void Create(params T[] items) => DbSet.AddRange(items);

    public Task<List<T>> GetAsync(Expression<Func<T, bool>> expression)
    {
        return DbSet.Where(expression).ToListAsync();
    }
}