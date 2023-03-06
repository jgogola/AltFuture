using AltFutureWebApp.Areas.Portfolios.Models;

namespace AltFutureWebApp.Interfaces
{
    public interface ICryptoPriceRepository
    {
        Task<IEnumerable<CryptoPrice>> GetAllAsync();

        Task<CryptoPrice> GetAsync(int id);


        bool Add(CryptoPrice cryptoPrice);

        bool Update(CryptoPrice cryptoPrice);

        bool Delete(CryptoPrice cryptoPrice);

        bool Save();
    }
}
