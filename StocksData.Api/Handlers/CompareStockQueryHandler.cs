using MediatR;
using StocksData.Api.Services;
using StocksData.Domain;
using StocksData.Domain.Models;

namespace StocksData.Api.Handlers;

public class CompareStockQueryHandler : IRequestHandler<CompareStockQuery, ICollection<StockPerformance>>
{
    private readonly IStocksDataProvider _dataProvider;
    private readonly PerformanceCalculator _calculator;

    public CompareStockQueryHandler(
        IStocksDataProvider dataProvider,
        PerformanceCalculator calculator)
    {
        _dataProvider = dataProvider;
        _calculator = calculator;
    }

    public async Task<ICollection<StockPerformance>> Handle(
        CompareStockQuery request,
        CancellationToken cancellationToken)
    {
        var data = await Task.WhenAll(
            _dataProvider.GetHistoricalDataAsync(request.BaseSymbol, request.FromDate, request.ToDate),
            _dataProvider.GetHistoricalDataAsync(request.Symbol, request.FromDate, request.ToDate));

        return new List<StockPerformance>
        {
            _calculator.Calculate(request.BaseSymbol, data.ElementAt(0)),
            _calculator.Calculate(request.Symbol, data.ElementAt(1))
        };
    }
}