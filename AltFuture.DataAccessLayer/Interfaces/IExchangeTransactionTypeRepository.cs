using AltFuture.Models;

namespace AltFuture.DataAccessLayer.Interfaces
{
    public interface IExchangeTransactionTypeRepository
    {
        Task<IEnumerable<ExchangeTransactionType>> GetAllAsync();

        Task<ExchangeTransactionType> GetByIdAsync(int id);

        Task<int> GetCountAsync();
        bool Add(ExchangeTransactionType exchangeTransactionType);

        bool Update(ExchangeTransactionType exchangeTransactionType);

        bool Delete(ExchangeTransactionType exchangeTransactionType);

        bool Save();
    }
}
