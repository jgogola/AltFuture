using AltFuture.DataAccessLayer.Models;

namespace AltFuture.DataAccessLayer.Interfaces
{
    public interface IExchangeApiRepository
    {
        Task<IEnumerable<ExchangeApi>> GetAllByUserIdExchangeIdAsync(int appUserId, int exchangeId);
        Task<ExchangeApi> GetByIdAsync(int exchangeApiId);
        bool Add(ExchangeApi exchangeApi);
        bool Update(ExchangeApi exchangeApi);
        bool Delete(ExchangeApi exchangeApi);
        bool Save();
    }
}