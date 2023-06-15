using AltFuture.DataAccessLayer.Models;

namespace AltFuture.DataAccessLayer.Interfaces
{
    public interface IExchangeApiProfileRepository
    {

        IEnumerable<ExchangeApiProfile> GetAllByUserId(int appUserId);
        IEnumerable<ExchangeApiProfile> GetAllByUserIdExchangeId(int appUserId, int exchangeId);
        Task<ExchangeApiProfile> GetByIdAsync(int exchangeApiId);
        bool Add(ExchangeApiProfile exchangeApiProfile);
        bool Update(ExchangeApiProfile exchangeApiProfile);
        bool Delete(ExchangeApiProfile exchangeApiProfile);
        bool Save();
    }
}