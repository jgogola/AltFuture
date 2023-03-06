using AltFutureWebApp.Areas.Portfolios.Models;

namespace AltFutureWebApp.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllAsync();

        Task<Transaction> GetAsync(int id);


        bool Add(Transaction transaction);

        bool Update(Transaction transaction);

        bool Delete(Transaction transaction);

        bool Save();
    }
}
