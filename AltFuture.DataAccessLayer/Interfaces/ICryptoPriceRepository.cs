using AltFuture.Models;

namespace AltFuture.DataAccessLayer.Interfaces
{
    public interface ICryptoPriceRepository
    {
        Task<IEnumerable<CryptoPrice>> GetAllAsync();

        Task<CryptoPrice> GetByIdAsync(int id);

        Task<int> GetCountAsync();

        bool Add(CryptoPrice cryptoPrice);

        bool Update(CryptoPrice cryptoPrice);

        bool Delete(CryptoPrice cryptoPrice);

        bool Save();
    }
}
