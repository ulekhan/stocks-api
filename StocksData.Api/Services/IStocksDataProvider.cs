using StocksData.Domain;

namespace StocksData.Api.Services;

public interface IStocksDataProvider
{
    Task<ICollection<HistoricalBar>> GetHistoricalDataAsync(
        string symbol, 
        DateTime fromDate,
        DateTime toDate);
}