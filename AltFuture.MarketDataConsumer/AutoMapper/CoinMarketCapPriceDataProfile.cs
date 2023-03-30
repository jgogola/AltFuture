using AutoMapper;

namespace AltFuture.MarketDataConsumer.AutoMapper
{
    public class CoinMarketCapPriceDataProfile : Profile
    {
        public CoinMarketCapPriceDataProfile()
        {
            DateTime dateRecorded = DateTime.Now;

            CreateMap<Models.CoinMarketCap.CoinMarketCapPriceData, Models.MarketDataClient.MarketPriceData>()
                .ForMember(dest => dest.CryptoName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.TickerSymbol, opt => opt.MapFrom(src => src.Symbol))
                .ForMember(dest => dest.MarketRank, opt => opt.MapFrom(src => src.CmcRank))
                .ForMember(dest => dest.FiatSymbol, opt => opt.MapFrom(src => src.FiatPrice.FiatSymbol))
                .ForMember(dest => dest.DateRecorded, opt => opt.MapFrom(src => dateRecorded))
                .ForMember(dest => dest.FiatSymbol, opt => opt.MapFrom(src => src.FiatPrice.FiatSymbol))
                .ForMember(dest => dest.MarketPrice, opt => opt.MapFrom(src => src.FiatPrice.Price))
                .ForMember(dest => dest.Volume24h, opt => opt.MapFrom(src => src.FiatPrice.Volume24h))
                .ForMember(dest => dest.VolumeChange24h, opt => opt.MapFrom(src => src.FiatPrice.VolumeChange24h))
                .ForMember(dest => dest.PercentChange1h, opt => opt.MapFrom(src => src.FiatPrice.PercentChange1h))
                .ForMember(dest => dest.PercentChange24h, opt => opt.MapFrom(src => src.FiatPrice.PercentChange24h))
                .ForMember(dest => dest.PercentChange7d, opt => opt.MapFrom(src => src.FiatPrice.PercentChange7d))
                .ForMember(dest => dest.PercentChange30d, opt => opt.MapFrom(src => src.FiatPrice.PercentChange30d))
                .ForMember(dest => dest.PercentChange60d, opt => opt.MapFrom(src => src.FiatPrice.PercentChange60d))
                .ForMember(dest => dest.PercentChange90d, opt => opt.MapFrom(src => src.FiatPrice.PercentChange90d))
                .ForMember(dest => dest.MarketCap, opt => opt.MapFrom(src => src.FiatPrice.MarketCap))
                .ForMember(dest => dest.MarketCapDominance, opt => opt.MapFrom(src => src.FiatPrice.MarketCapDominance))
                ;

        }
    }
}
