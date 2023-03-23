using AltFuture.DataAccessLayer.Models.DTOs.CryptoAssetCharts;


namespace AltFuture.DataAccessLayer.Interfaces.Services
{
    public interface ICryptoAssetChartData
    {
        Task<List<CryptoQuantityPercentageDto>> GetCryptoQuantityPercentageAsync(int userId);

        Task<List<CryptoInvestmentPercentageDto>> GetCryptoAssetAllocationDataAsync(int userId);

        Task<List<CryptoInvestmentPerformanceDto>> GetCryptoAssetPerformanceDataAsync(int userId);
    }
}
