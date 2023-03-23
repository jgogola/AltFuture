using AltFuture.DataAccessLayer.Models;

namespace AltFuture.DataAccessLayer.Interfaces
{
    public interface ITransactionTypeRepository
    {
        Task<IEnumerable<TransactionType>> GetAllAsync();

        Task<TransactionType> GetByIdAsync(int id);


    }
}
