using AltFuture.BusinessLogicLayer.Models.PortfolioCharts;
using AltFuture.DataAccessLayer.Models.StoredProcs;

namespace AltFuture.BusinessLogicLayer.Interfaces
{
    public interface IPortfolioChartData
    {
        Task<List<AssetAllocationDataDto>> GetAssetAllocationDataAsync(int userId);

        Task<List<AssetPerformanceDataDto>> GetAssetPerformanceDataAsync(int userId);

        Task<List<ExchangeUsageDataDto>> GetExchangeUsageDataAsync(int userId);

        Task<List<PortfolioRunningTotalByMonth>> GetPortfolioRunningTotalByMonthDataAsync(int userId);
    }
}
