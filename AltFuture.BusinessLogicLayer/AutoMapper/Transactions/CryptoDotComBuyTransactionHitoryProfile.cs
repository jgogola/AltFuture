using AltFuture.BusinessLogicLayer.AutoMapper.Transactions.Resolvers;
using AltFuture.BusinessLogicLayer.Models.ExchangeTransactions;
using AltFuture.DataAccessLayer.Data.Enums;
using AltFuture.DataAccessLayer.Models;
using AutoMapper;

namespace AltFuture.BusinessLogicLayer.AutoMapper.Transactions
{
    public class CryptoDotComBuyTransactionHitoryProfile : Profile
    {
        public CryptoDotComBuyTransactionHitoryProfile()
        {
            int appUserId = 1;
            int exchangeId = (int)ExchangeEnum.CryptoDotCom;
            DateTime createdDate = DateTime.Now;

            CreateMap<CryptoDotComBuyTransactionHistoryDto, Transaction>()
                    .ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => appUserId))
                    .ForMember(dest => dest.CryptoId, opt => opt.MapFrom<CryptoAssetResolver>())
                    .ForMember(dest => dest.ExchangeTransactionTypeId, opt => opt.MapFrom<ExchangeTransactionTypeResolver>())
                    .ForMember(dest => dest.FromExchangeId, opt => opt.MapFrom(src => exchangeId))
                    .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => createdDate))
                    ;
        }

    }
}
