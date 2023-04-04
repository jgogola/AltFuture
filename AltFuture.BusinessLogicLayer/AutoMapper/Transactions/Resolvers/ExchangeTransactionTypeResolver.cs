using AltFuture.BusinessLogicLayer.Interfaces.Models;
using AltFuture.BusinessLogicLayer.Models.ExchangeTransactions;
using AltFuture.DataAccessLayer.Data.Enums;
using AltFuture.DataAccessLayer.Interfaces.Services;
using AltFuture.DataAccessLayer.Models;
using AutoMapper;

namespace AltFuture.BusinessLogicLayer.AutoMapper.Transactions.Resolvers
{
    internal class ExchangeTransactionTypeResolver : IValueResolver<IExchangeTransactionHistoryDto, Transaction, int>
    {
        private readonly List<ExchangeTransactionType> _exchangeTransactionTypeLookup;

        public ExchangeTransactionTypeResolver(IExchangeTransactionTypeDataService exchangeTransactionTypeDataService)
        {
            _exchangeTransactionTypeLookup = exchangeTransactionTypeDataService.ExchangeTransactionTypeList;
        }

        public int Resolve(IExchangeTransactionHistoryDto source, Transaction destination, int destMember, ResolutionContext context)
        {
            var exchangeId = (int)context.Items["ExchangeId"];
            return _exchangeTransactionTypeLookup.FirstOrDefault(t => t.ExchangeId == exchangeId && t.ExchangeTransactionTypeName.ToLower() == source.ExchangeTransactionTypeName.ToLower()).ExchangeTransactionTypeId;
        }
    }
}
