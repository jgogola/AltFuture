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

            return _mapper.Map<List<Transaction>>(exchangeTransactionDtoList, opts =>
                            {
                                opts.Items["AppUserId"] = appUserId;
                            });

        }
    }
}
