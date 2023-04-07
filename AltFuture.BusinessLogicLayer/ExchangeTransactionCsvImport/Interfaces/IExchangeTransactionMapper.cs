using AltFuture.DataAccessLayer.Models;

namespace AltFuture.BusinessLogicLayer.ExchangeTransactionCsvImport.Interfaces
{
    public interface IExchangeTransactionMapper
    {
        Task<List<Transaction>> MapExchangeTransactionDtoToTransaction<T>(IEnumerable<T> exchangeTransactionDtoList, int appUserId) where T : IExchangeTransactionDto;
    }
}
