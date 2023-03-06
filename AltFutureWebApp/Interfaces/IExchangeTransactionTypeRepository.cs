using AltFutureWebApp.Areas.Portfolios.Models;

namespace AltFutureWebApp.Interfaces
{
    public interface IExchangeTransactionTypeRepository
    {
        Task<IEnumerable<ExchangeTransactionType>> GetAllAsync();

        Task<ExchangeTransactionType> GetAsync(int id);


        bool Add(ExchangeTransactionType exchangeTransactionType);

        bool Update(ExchangeTransactionType exchangeTransactionType);

        bool Delete(ExchangeTransactionType exchangeTransactionType);

        bool Save();
    }
}
