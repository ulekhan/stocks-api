namespace StocksData.Domain;

public class HistoricalBar
{
    internal HistoricalBar()
    {}
    
    public HistoricalBar(string symbol, decimal high, decimal low, decimal open, decimal close, DateTime dateTime)
    {
        if (string.IsNullOrEmpty(symbol) || symbol.Length > 5)
            throw new DomainException($"Invalid symbol name {symbol}");
        Symbol = symbol;
        High = high;
        Low = low;
        Open = open;
        Close = close;
        DateTime = dateTime;
    }

    public long Id { get; private set; }
    public string? Symbol { get; private set; }
    public decimal High { get; private set; }
    public decimal Low { get; private set; }
    public decimal Open { get; private set; }
    public decimal Close { get; private set; }
    public DateTime DateTime { get; private set; }
}