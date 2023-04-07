using AltFuture.BusinessLogicLayer.ExchangeTransactionCsvImport.AutoMapper.Resolvers;
using AltFuture.BusinessLogicLayer.ExchangeTransactionCsvImport.Models;
using AltFuture.DataAccessLayer.Models;
using AutoMapper;

namespace AltFuture.BusinessLogicLayer.ExchangeTransactionCsvImport.AutoMapper
{
    public class CoinbaseTransactionDtoTransactionProfile : Profile
    {
        public CoinbaseTransactionDtoTransactionProfile()
        {
            DateTime createdDate = DateTime.Now;

            CreateMap<CoinbaseTransactionDto, Transaction>()
                .ForMember(dest => dest.FromExchangeId, opt => opt.MapFrom(src => src.ExchangeId))
                .ForMember(dest => dest.CryptoId, opt => opt.MapFrom<CryptoAssetResolver>())
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.ExchangeTransactionTypeId, opt => opt.MapFrom<ExchangeTransactionTypeResolver>())
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => createdDate))
                .BeforeMap((src, dest, context) =>
                {
                    if (context.Items.ContainsKey("AppUserId"))
                    {
                        int appUserId = (int)context.Items["AppUserId"];
                        dest.AppUserId = appUserId;
                    }

                })
                ;
        }
    }
}
