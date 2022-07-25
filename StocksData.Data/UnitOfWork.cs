using StocksData.Data.Db;
using StocksData.Domain;
using StocksData.Domain.Abstractions;

namespace StocksData.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _dataContext;
    
    public UnitOfWork(IRepository<HistoricalBar> historicalBars, DataContext dataContext)
    {
        HistoricalBars = historicalBars;
        _dataContext = dataContext;
    }

    public IRepository<HistoricalBar> HistoricalBars { get; }

    public async Task SaveChangesAsync()
    {
        await _dataContext.SaveChangesAsync();
    }
}