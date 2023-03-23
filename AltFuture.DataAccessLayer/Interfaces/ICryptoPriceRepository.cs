using AltFuture.DataAccessLayer.Models;

namespace AltFuture.DataAccessLayer.Interfaces
{
    public interface ICryptoPriceRepository
    {
        Task<IEnumerable<CryptoPrice>> GetAllAsync();

        Task<IEnumerable<CryptoPrice>> GetLatestAsync();

        Task<DateTime> GetLastSyncedDate();

        Task<CryptoPrice> GetByIdAsync(int id);

        Task<int> GetCountAsync();

        bool Add(CryptoPrice cryptoPrice);

        Task<bool> AddRangeAsync(IEnumerable<CryptoPrice> cryptoPrice);


        bool Update(CryptoPrice cryptoPrice);

        bool Delete(CryptoPrice cryptoPrice);

        bool Save();
    }
}
