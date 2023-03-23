using AltFuture.DataAccessLayer.Models.StoredProcs;

namespace AltFuture.DataAccessLayer.Interfaces
{
    public interface IPortfolioSummaryRepository
    {

        Task<IEnumerable<PortfolioSummaryGetAll>> GetAllAsync();


    }
}
