using AltFuture.Models;

namespace AltFuture.DataAccessLayer.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllAsync();

        Task<Transaction> GetByIdAsync(int id);

        Task<int> GetCountAsync();
        bool Add(Transaction transaction);

        bool Update(Transaction transaction);

        bool Delete(Transaction transaction);

        bool Save();
    }
}
