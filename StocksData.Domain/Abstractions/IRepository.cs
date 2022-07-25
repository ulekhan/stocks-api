using System.Linq.Expressions;

namespace StocksData.Domain.Abstractions;

public interface IRepository<T> where T : class
{
    void Delete(T? entity);

    void Update(T? item);

    void Create(params T[] items);

    Task<List<T>> GetAsync(Expression<Func<T, bool>> expression);
}
