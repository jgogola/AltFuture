using AltFutureWebApp.Areas.Portfolios.Models;

namespace AltFutureWebApp.Interfaces
{
    public interface IExchangeRepository
    {
        Task<IEnumerable<Exchange>> GetAllAsync();

        Task<Exchange> GetAsync(int id);


        bool Add(Exchange exchange);

        bool Update(Exchange exchange);

        bool Delete(Exchange exchange);

        bool Save();
    }
}
