using AutoMapper;

namespace AltFuture.MarketDataConsumer.AutoMapper
{
    public class CoinMarketCapPriceDataProfile : Profile
    {
        public CoinMarketCapPriceDataProfile()
        {
            CreateMap<Models.CoinMarketCap.CoinMarketCapPriceData, Models.MarketDataClient.MarketPriceData>()
                .ForMember(dest => dest.CryptoName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.TickerSymbol, opt => opt.MapFrom(src => src.Symbol))
                .ForMember(dest => dest.MarketRank, opt => opt.MapFrom(src => src.CmcRank))
                .ForMember(dest => dest.FiatSymbol, opt => opt.MapFrom(src => src.quote.USD.FiatSymbol))
                .ForMember(dest => dest.MarketPrice, opt => opt.MapFrom(src => src.quote.USD.Price))
                .ForMember(dest => dest.Volume24h, opt => opt.MapFrom(src => src.quote.USD.Volume24h))
                .ForMember(dest => dest.VolumeChange24h, opt => opt.MapFrom(src => src.quote.USD.VolumeChange24h))
                .ForMember(dest => dest.PercentChange1h, opt => opt.MapFrom(src => src.quote.USD.PercentChange1h))
                .ForMember(dest => dest.PercentChange24h, opt => opt.MapFrom(src => src.quote.USD.PercentChange24h))
                .ForMember(dest => dest.PercentChange7d, opt => opt.MapFrom(src => src.quote.USD.PercentChange7d))
                .ForMember(dest => dest.PercentChange30d, opt => opt.MapFrom(src => src.quote.USD.PercentChange30d))
                .ForMember(dest => dest.PercentChange60d, opt => opt.MapFrom(src => src.quote.USD.PercentChange60d))
                .ForMember(dest => dest.PercentChange90d, opt => opt.MapFrom(src => src.quote.USD.PercentChange90d))
                .ForMember(dest => dest.MarketCap, opt => opt.MapFrom(src => src.quote.USD.MarketCap))
                .ForMember(dest => dest.MarketCapDominance, opt => opt.MapFrom(src => src.quote.USD.MarketCapDominance))
                .ForMember(dest => dest.DateRecorded, opt =>
                    opt.MapFrom((src, dest, destMember, context) =>
                        context.Items.ContainsKey("Timestamp")
                            ? (DateTime)context.Items["Timestamp"]
                            : throw new Exception("AutoMapper CoinMarketCapPriceDataProfile is missing the TimeStamp context option!")
                    )
                )
                ;

        }
    }
}
