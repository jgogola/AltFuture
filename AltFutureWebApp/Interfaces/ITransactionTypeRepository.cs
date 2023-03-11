using AltFutureWebApp.Models;

namespace AltFutureWebApp.Interfaces
{
    public interface ITransactionTypeRepository
    {
        Task<IEnumerable<TransactionType>> GetAllAsync();

        Task<TransactionType> GetByIdAsync(int id);


    }
}
