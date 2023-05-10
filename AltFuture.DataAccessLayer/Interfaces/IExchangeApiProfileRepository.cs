using AltFuture.DataAccessLayer.Models;

namespace AltFuture.DataAccessLayer.Interfaces
{
    public interface IExchangeApiProfileRepository
    {
        Task<IEnumerable<ExchangeApiProfile>> GetAllByUserIdExchangeIdAsync(int appUserId, int exchangeId);
        Task<ExchangeApiProfile> GetByIdAsync(int exchangeApiId);
        bool Add(ExchangeApiProfile exchangeApiProfile);
        bool Update(ExchangeApiProfile exchangeApiProfile);
        bool Delete(ExchangeApiProfile exchangeApiProfile);
        bool Save();
    }
}