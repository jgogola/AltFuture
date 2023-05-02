using AutoMapper;
using AltFuture.DataAccessLayer.Models;
using AltFuture.WebApp.Areas.Portfolios.ViewModels;

namespace AltFuture.WebApp.Areas.Portfolios.AutoMapper;

public class TransactionTransactionDetailProfile : Profile
{

    public TransactionTransactionDetailProfile()
    {
        CreateMap<Transaction, TransactionDetail>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser.UserName))
            .ForMember(dest => dest.CryptoName, opt => opt.MapFrom(src => src.Crypto.CryptoName))
            .ForMember(dest => dest.ExchangeTransactionTypeName, opt => opt.MapFrom(src => src.ExchangeTransactionType.ExchangeTransactionTypeName))
            .ForMember(dest => dest.FromExchangeName, opt => opt.MapFrom(src => src.FromExchange.ExchangeName))
            .ForMember(dest => dest.ToExchangeName, opt => opt.MapFrom(src => src.ToExchange.ExchangeName))
            .ForMember(dest => dest.CryptoIcon, opt => opt.MapFrom(src => src.Crypto.CryptoIcon))
            ;
    }

}