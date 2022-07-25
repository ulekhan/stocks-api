using StocksData.Domain.Models;

namespace StocksData.Domain;

public class PerformanceCalculator
{
    public StockPerformance Calculate(string symbol, ICollection<HistoricalBar> bars)
    {
        var result = new StockPerformance(symbol, new List<Margin>());
        if (!bars.Any())
            return result;
        
        var firstDayPrice = bars.First();
        
        for (var i = 1; i < bars.Count; i++)
        {
            var current = bars.ElementAt(i);
            result.Margins.Add(new Margin(current.DateTime.Date, current.Close / firstDayPrice.Close * 100 - 100));
        }

        return result;
    }
}