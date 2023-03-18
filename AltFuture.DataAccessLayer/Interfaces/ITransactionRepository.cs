using AltFuture.Models;

namespace AltFuture.DataAccessLayer.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllAsync();

        Task<Transaction> GetByIdAsync(int id);

        Task<int> GetCountAsync();

        Task<bool> AddRangeAsync(IEnumerable<Transaction> transactions);

        bool Add(Transaction transaction);

        bool Update(Transaction transaction);

        bool Delete(Transaction transaction);

        bool Save();

        Task<bool> SaveAsync();
    }
}
