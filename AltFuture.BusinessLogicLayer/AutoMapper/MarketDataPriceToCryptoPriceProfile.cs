using AltFuture.DataAccessLayer.Models;
using AltFuture.MarketDataConsumer.Models.MarketDataClient;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltFuture.BusinessLogicLayer.AutoMapper
{
    public class MarketDataPriceToCryptoPriceProfile : Profile
    {
        public MarketDataPriceToCryptoPriceProfile()
        {
            CreateMap<MarketPriceData,CryptoPrice>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.MarketPrice))
                ;
        }
    }
}
