using AltFuture.BusinessLogicLayer.AutoMapper.Transactions.Resolvers;
using AltFuture.BusinessLogicLayer.Models.ExchangeTransactions;
using AltFuture.DataAccessLayer.Data.Enums;
using AltFuture.DataAccessLayer.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AltFuture.BusinessLogicLayer.AutoMapper.Transactions
{
    public class EtoroTransactionHistoryProfile : Profile
    {
        public EtoroTransactionHistoryProfile()
        {
            DateTime createdDate = DateTime.Now;

            CreateMap<EtoroTransactionHistoryDto, Transaction>()
               // .ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => appUserId))
                .ForMember(dest => dest.CryptoId, opt => opt.MapFrom<CryptoAssetResolver>())
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => (decimal)src.Quantity))
                .ForMember(dest => dest.ExchangeTransactionTypeId, opt => opt.MapFrom<ExchangeTransactionTypeResolver>())
               // .ForMember(dest => dest.FromExchangeId, opt => opt.MapFrom(src => exchangeId))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => createdDate))
                .BeforeMap((src, dest, context) =>
                {
                    if (context.Items.ContainsKey("AppUserId"))
                    {
                        int appUserId = (int)context.Items["AppUserId"];
                        dest.AppUserId = appUserId;
                    }

                    if (context.Items.ContainsKey("ExchangeId"))
                    {
                        int exchangeId = (int)context.Items["ExchangeId"];
                        dest.FromExchangeId = exchangeId;
                    }
                })
                ;
        }

    }
}
