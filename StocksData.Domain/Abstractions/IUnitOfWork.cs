namespace StocksData.Domain.Abstractions;

public interface IUnitOfWork
{
    public IRepository<HistoricalBar> HistoricalBars { get; }
    public Task SaveChangesAsync();
}