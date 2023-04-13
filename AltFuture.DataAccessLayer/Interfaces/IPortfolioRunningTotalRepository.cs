using AltFuture.DataAccessLayer.Models.StoredProcs;

namespace AltFuture.DataAccessLayer.Interfaces
{
    public interface IPortfolioRunningTotalRepository
    {

        Task<IEnumerable<PortfolioRunningTotalByMonth>> GetByMonthAsync(int appUserId);

        Task<IEnumerable<PortfolioRunningTotalByDay>> GetByDayAsync(int appUserId);


    }
}
