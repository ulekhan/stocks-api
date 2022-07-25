using Alpaca.Markets;
using AutoMapper;
using StocksData.Domain;

namespace StocksData.Api.Mappers;

public class HistoricalBarMappingProfile : Profile
{
    public HistoricalBarMappingProfile()
    {
        CreateMap<IBar, HistoricalBar>()
            .ForMember(d => d.DateTime, s => s.MapFrom(s => s.TimeUtc.Date));
    }
}