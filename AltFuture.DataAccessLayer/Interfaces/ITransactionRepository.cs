using AltFuture.DataAccessLayer.Models;

namespace AltFuture.DataAccessLayer.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllAsync();

        Task<IEnumerable<Transaction>> GetAllForUserAsync(int userId);

        Task<Transaction> GetByIdAsync(int id);

        Task<int> GetCountAsync();

        Task<int> GetCountForUserAsync(int userId);

        Task<DateTime> GetLatestTransactionDate(int userId);

        Task<DateTime> GetLatestTransactionDate(int userId, int exchangeId);

        Task<bool> AddRangeAsync(IEnumerable<Transaction> transactions);

        bool Add(Transaction transaction);

        bool Update(Transaction transaction);

        bool Delete(Transaction transaction);

        bool Save();

        Task<bool> SaveAsync();
    }
}
