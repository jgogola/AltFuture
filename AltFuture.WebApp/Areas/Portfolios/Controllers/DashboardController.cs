using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace AltFuture.WebApp.Areas.Portfolios.Controllers
{
    [Area("Portfolios")]
    public class DashboardController : Controller
    {
        private readonly IPortfolioSummaryRepository _portfolioSummaryRepository;
        private readonly ICryptoRepository _cryptoRepository;
        private readonly ICryptoPriceRepository _cryptoPriceRepository;
        private readonly IPortfolioChartData _portfolioChartData;

        public DashboardController(IPortfolioSummaryRepository portfolioSummaryRepository, 
                                   ICryptoRepository cryptoRepository, 
                                   ICryptoPriceRepository cryptoPriceRepository, 
                                   IPortfolioChartData portfolioChartData)
        {
            _portfolioSummaryRepository = portfolioSummaryRepository;
            _cryptoRepository = cryptoRepository;
            _cryptoPriceRepository = cryptoPriceRepository;
            _portfolioChartData = portfolioChartData;
        }

        public async Task<IActionResult> Index()
        {
            var userMessageJson = TempData["UserMessage"] as string;
            var appUserId = 1;
            return View(await _portfolioSummaryRepository.GetAllAsync(appUserId));
        }


        [HttpGet]
        public async Task<JsonResult> GetAssetAllocationData()
        {
            var appUserId = 1;
            var assetAllocationData = await _portfolioChartData.GetAssetAllocationDataAsync(appUserId);

            var data = new JArray { };

            data.Add(new JArray { "Crypto", "Allocation" });

            assetAllocationData.ForEach(c =>
            {
                data.Add(new JArray { c.TickerSymbol, c.InvestmentPercentage });
            });


            return Json(data);
        }


        [HttpGet]
        public async Task<JsonResult> GetAssetPerformanceData()
        {
            var appUserId = 1;
            var assetPerformanceData = await _portfolioChartData.GetAssetPerformanceDataAsync(appUserId);

            var data = new JArray { };

            data.Add(new JArray { "Crypto Assets", "Investment", "Current Worth" });

            assetPerformanceData.ForEach(c =>
            {
                data.Add(new JArray { c.TickerSymbol, c.Investment, c.Investment + c.UnrealizedProfit }); 
            });


            return Json(data);
        }


        [HttpGet]
        public async Task<JsonResult> GetExchangeUsageData()
        {
            var appUserId = 1;
            var exchangeUsageData = await _portfolioChartData.GetExchangeUsageDataAsync(appUserId);

            var data = new JArray { };

            data.Add(new JArray { "Exchange", "Usage" });

            exchangeUsageData.ForEach(c =>
            {
                data.Add(new JArray { c.ExchangeName, c.UsagePercentage });
            });


            return Json(data);
        }


        [HttpGet]
        public async Task<JsonResult> GetPortfolioRunningTotalByMonthData()
        {
            var appUserId = 1;
            var dailyRunningTotalData = await _portfolioChartData.GetPortfolioRunningTotalByMonthDataAsync(appUserId);

            var data = new JArray { };

            data.Add(new JArray { "Month", "Investment", "Current Worth" });

            dailyRunningTotalData.ForEach(c =>
            {
                data.Add(new JArray { c.RunningTotalInterval, c.InvestmentRunningTotal, c.CurrentWorthRunningTotal });
            });


            return Json(data);
        }

    }
}
