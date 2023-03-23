using AltFuture.DataAccessLayer.Models;

namespace AltFuture.DataAccessLayer.Interfaces
{
    public interface ICryptoRepository
    {

        Task<IEnumerable<Crypto>> GetAllAsync();

        Task<Crypto> GetByIdAsync(int id);

        Task<int> GetCountAsync();

        bool Add(Crypto crypto);

        bool Update(Crypto crypto);

        bool Delete(Crypto crypto);

        bool Save();
    }
}
