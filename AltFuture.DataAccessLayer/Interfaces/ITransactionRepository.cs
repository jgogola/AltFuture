using AltFuture.DataAccessLayer.Models;

namespace AltFuture.DataAccessLayer.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<TransactionWithInvestmentTotals>> GetAllAsync();

        Task<IEnumerable<TransactionWithInvestmentTotals>> GetAllForUserAsync(int userId);

        Task<TransactionWithInvestmentTotals> GetByIdAsync(int id);

        Task<int> GetCountAsync();

        Task<int> GetCountForUserAsync(int userId);

        Task<DateTime> GetLatestTransactionDateAsync(int userId);

        Task<DateTime> GetLatestTransactionDateAsync(int userId, int exchangeId);

        Task<bool> AddRangeAsync(IEnumerable<Transaction> transactions);

        bool Add(Transaction transaction);

        bool Update(Transaction transaction);

        bool Delete(Transaction transaction);

        bool Save();

        Task<bool> SaveAsync();
    }
}
