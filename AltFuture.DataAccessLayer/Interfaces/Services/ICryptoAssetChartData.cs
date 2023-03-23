using AltFuture.DataAccessLayer.Models.DTOs.CryptoAssetCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace AltFuture.DataAccessLayer.Interfaces.Services
{
    public interface ICryptoAssetChartData
    {
        Task<List<CryptoQuantityPercentageDto>> GetCryptoQuantityPercentageAsync(int userId);

        Task<List<CryptoInvestmentPercentageDto>> GetCryptoAssetAllocationDataAsync(int userId);

        Task<List<CryptoInvestmentPerformanceDto>> GetCryptoAssetPerformanceDataAsync(int userId);
    }
}
