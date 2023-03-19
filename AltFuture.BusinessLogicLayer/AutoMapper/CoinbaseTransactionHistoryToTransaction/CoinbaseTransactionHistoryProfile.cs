using AltFuture.BusinessLogicLayer.AutoMapper.CoinbaseAssetToCryptoResolver;
using AltFuture.BusinessLogicLayer.Models.DTOs;
using AltFuture.DataAccessLayer.Data.Enums;
using AltFuture.Models;
using AutoMapper;

namespace AltFuture.BusinessLogicLayer.AutoMapper.CoinbaseTransactionHistoryToTransaction
{
    public class CoinbaseTransactionHistoryProfile : Profile
    {
        public CoinbaseTransactionHistoryProfile()
        {
            int appUserId = 1;
            DateTime createdDate = DateTime.Now;

            CreateMap<CoinbaseTransactionHistoryDto, Transaction>()
                .ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => appUserId))
                .ForMember(dest => dest.CryptoId, opt => opt.MapFrom<CoinbaseCryptoResolver>())
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => (decimal)src.Quantity))
                .ForMember(dest => dest.ExchangeTransactionTypeId, opt => opt.MapFrom<CoinbaseExchangeTransactionTypeResolver>())
                .ForMember(dest => dest.FromExchangeId, opt => opt.MapFrom(src => (int)ExchangeEnum.Coinbase))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => createdDate))
                ;
        }
    }
}
