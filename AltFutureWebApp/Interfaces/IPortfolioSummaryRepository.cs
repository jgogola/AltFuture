using AltFutureWebApp.Data;
using AltFutureWebApp.Models;
using AltFutureWebApp.Models.StoredProcs;

namespace AltFutureWebApp.Interfaces
{
    public interface IPortfolioSummaryRepository
    {

        Task<IEnumerable<PortfolioSummaryGetAll>> GetAllAsync();


    }
}
