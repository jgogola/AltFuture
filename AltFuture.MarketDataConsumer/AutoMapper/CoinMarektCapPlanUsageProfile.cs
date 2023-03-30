using AutoMapper;


namespace AltFuture.MarketDataConsumer.AutoMapper
{
    public class CoinMarektCapPlanUsageProfile : Profile
    {
        public CoinMarektCapPlanUsageProfile()
        {
            CreateMap<Models.CoinMarketCap.CoinMarketCapPlanUsage, Models.MarketDataClient.DataPlanUsage>()
                .ForMember(dest => dest.DataPlanName, opt => opt.MapFrom(src => src.DataPlanName))
                .ForMember(dest => dest.Plan, opt => opt.MapFrom(src => src.Plan))
                .ForMember(dest => dest.Usage, opt => opt.MapFrom(src => src.Usage)); ;

            CreateMap<Models.CoinMarketCap.Plan, Models.MarketDataClient.Plan>();
            CreateMap<Models.CoinMarketCap.Usage, Models.MarketDataClient.Usage>();
            CreateMap<Models.CoinMarketCap.CurrentMinute, Models.MarketDataClient.CurrentMinute>();
            CreateMap<Models.CoinMarketCap.CurrentDay, Models.MarketDataClient.CurrentDay>();
            CreateMap<Models.CoinMarketCap.CurrentMonth, Models.MarketDataClient.CurrentMonth>();
        }
    }
}
