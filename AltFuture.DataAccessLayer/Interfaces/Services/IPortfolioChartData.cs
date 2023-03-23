using AltFuture.DataAccessLayer.Models.DTOs.PortfolioCharts;


namespace AltFuture.DataAccessLayer.Interfaces.Services
{
    public interface IPortfolioChartData
    {
        Task<List<AssetAllocationDataDto>> GetAssetAllocationDataAsync(int userId);

        Task<List<AssetPerformanceDataDto>> GetAssetPerformanceDataAsync(int userId);

        Task<List<ExchangeUsageDataDto>> GetExchangeUsageDataAsync(int userId);
    }
}
