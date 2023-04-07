using AltFuture.BusinessLogicLayer.ExchangeTransactionCsvImport.Interfaces;
using AltFuture.DataAccessLayer.Interfaces.Services;
using AltFuture.DataAccessLayer.Models;
using AutoMapper;

namespace AltFuture.BusinessLogicLayer.ExchangeTransactionCsvImport.AutoMapper.Resolvers
{
    internal class ExchangeTransactionTypeResolver : IValueResolver<IExchangeTransactionDto, Transaction, int>
    {
        private readonly List<ExchangeTransactionType> _exchangeTransactionTypeLookup;

        public ExchangeTransactionTypeResolver(IExchangeTransactionTypeDataService exchangeTransactionTypeDataService)
        {
            _exchangeTransactionTypeLookup = exchangeTransactionTypeDataService.ExchangeTransactionTypeList;
        }

        public int Resolve(IExchangeTransactionDto source, Transaction destination, int destMember, ResolutionContext context)
        {
            return _exchangeTransactionTypeLookup.FirstOrDefault(t => t.ExchangeId == source.ExchangeId && t.ExchangeTransactionTypeName.ToLower() == source.ExchangeTransactionTypeName.ToLower()).ExchangeTransactionTypeId;
        }
    }
}
