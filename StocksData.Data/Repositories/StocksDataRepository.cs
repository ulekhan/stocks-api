using StocksData.Data.Db;
using StocksData.Domain;

namespace StocksData.Data.Repositories;

public class StocksDataRepository : Repository<HistoricalBar>
{
    public StocksDataRepository(DataContext context) : base(context)
    {
    }
}