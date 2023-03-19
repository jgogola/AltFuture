using AltFuture.BusinessLogicLayer.Models.DTOs;
using AltFuture.DataAccessLayer.Data.Enums;
using AltFuture.DataAccessLayer.Interfaces.Services;
using AltFuture.Models;
using AutoMapper;

namespace AltFuture.BusinessLogicLayer.AutoMapper.CoinbaseTransactionHistoryToTransaction
{
    internal class CoinbaseExchangeTransactionTypeResolver : IValueResolver<CoinbaseTransactionHistoryDto, Transaction, int>
    {
        private readonly List<ExchangeTransactionType> _exchangeTransactionTypeLookup;

        public CoinbaseExchangeTransactionTypeResolver(IExchangeTransactionTypeDataService exchangeTransactionTypeDataService)
        {
            _exchangeTransactionTypeLookup = exchangeTransactionTypeDataService.ExchangeTransactionTypeList;
        }

        public int Resolve(CoinbaseTransactionHistoryDto source, Transaction destination, int destMember, ResolutionContext context)
        {
            return _exchangeTransactionTypeLookup.FirstOrDefault(t => t.ExchangeId == (int)ExchangeEnum.Coinbase && t.ExchangeTransactionTypeName == source.TransactionType).ExchangeTransactionTypeId;
        }
    }
}
