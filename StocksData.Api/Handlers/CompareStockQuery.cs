using MediatR;
using StocksData.Domain.Models;

namespace StocksData.Api.Handlers;

public record CompareStockQuery
    (string BaseSymbol, string Symbol, DateTime FromDate, DateTime ToDate) : IRequest<ICollection<StockPerformance>>;