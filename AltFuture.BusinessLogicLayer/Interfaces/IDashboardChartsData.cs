using AltFuture.BusinessLogicLayer.Models.DTOs.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace AltFuture.BusinessLogicLayer.Interfaces
{
    public interface IDashboardChartsData
    {
        Task<List<CryptoQuantityPercentageDto>> GetCryptoQuantityPercentageAsync(int userId);

        Task<List<CryptoInvestmentPercentageDto>> GetCryptoInvestmentPercentageAsync(int userId);

        Task<List<CryptoInvestmentPerformanceDto>> GetCryptoInvestmentPerformanceAsync(int userId);
    }
}
