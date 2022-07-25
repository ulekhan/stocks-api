using Alpaca.Markets;
using AutoMapper;
using StocksData.Domain;
using StocksData.Domain.Abstractions;

namespace StocksData.Api.Services;

public class StocksDataProvider : IStocksDataProvider
{
    private readonly IAlpacaDataClient _alpacaDataClient;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public StocksDataProvider(
        IAlpacaDataClient alpacaDataClient,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _alpacaDataClient = alpacaDataClient;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ICollection<HistoricalBar>> GetHistoricalDataAsync(
        string symbol,
        DateTime fromDate,
        DateTime toDate)
    {
        var data = await _unitOfWork.HistoricalBars
            .GetAsync(p => p.Symbol == symbol.ToUpper() && p.DateTime >= fromDate && p.DateTime <= toDate);

        if (data.Count >= (toDate - fromDate).Days)
            return data;

        var symbolRequest = new HistoricalBarsRequest(symbol, fromDate.Date, toDate.Date, BarTimeFrame.Day);
        var bars = (await _alpacaDataClient.GetHistoricalBarsAsync(symbolRequest))
            .Items.First().Value
            .Select(b => _mapper.Map<HistoricalBar>(b))
            .ToList();

        foreach (var bar in bars.Where(bar => data.All(p => p.DateTime.Date != bar.DateTime)))
            _unitOfWork.HistoricalBars.Create(_mapper.Map<HistoricalBar>(bar));

        await _unitOfWork.SaveChangesAsync();

        return bars;
    }
}