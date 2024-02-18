using AltFuture.BusinessLogicLayer.ExchangeTransactionCsvImport.Interfaces;
using AltFuture.DataAccessLayer.Models;
using AutoMapper;

namespace AltFuture.BusinessLogicLayer.Services.ExchangeTransactions
{
    public class ExchangeTransactionMapper : IExchangeTransactionMapper
    {

        private readonly IMapper _mapper;

        public ExchangeTransactionMapper(IMapper mapper)
        {
            _mapper = mapper;
        }


        public async Task<List<Transaction>> MapExchangeTransactionDtoToTransaction<T>(IEnumerable<T> exchangeTransactionDtoList, int appUserId) where T : IExchangeTransactionDto
        {
            //* The Data Type of exchangeTransactionDtoList will be looked at by AutoMapper to then determine by Naming Convention the correct Profile to use.
            return _mapper.Map<List<Transaction>>(exchangeTransactionDtoList, opts =>
                            {
                                opts.Items["AppUserId"] = appUserId;
                            });

        }
    }
}
