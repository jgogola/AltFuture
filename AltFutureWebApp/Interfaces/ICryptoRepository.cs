using AltFutureWebApp.Areas.Portfolios.Models;

namespace AltFutureWebApp.Interfaces
{
    public interface ICryptoRepository
    {
        Task<IEnumerable<Crypto>> GetAllAsync();

        Task<Crypto> GetAsync(int id);


        bool Add(Crypto crypto);

        bool Update(Crypto crypto);

        bool Delete(Crypto crypto);

        bool Save();
    }
}
