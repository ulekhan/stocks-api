namespace StocksData.Domain.Models;

public record StockPerformance (string Stock, ICollection<Margin> Margins);