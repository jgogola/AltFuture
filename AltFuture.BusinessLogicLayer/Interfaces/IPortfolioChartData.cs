using AltFuture.BusinessLogicLayer.Models.PortfolioCharts;


namespace AltFuture.BusinessLogicLayer.Interfaces
{
    public interface IPortfolioChartData
    {
        Task<List<AssetAllocationDataDto>> GetAssetAllocationDataAsync(int userId);

        Task<List<AssetPerformanceDataDto>> GetAssetPerformanceDataAsync(int userId);

        Task<List<ExchangeUsageDataDto>> GetExchangeUsageDataAsync(int userId);
    }
}
