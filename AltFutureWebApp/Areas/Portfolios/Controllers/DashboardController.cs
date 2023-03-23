using AltFuture.CoinMarketCapAPI.Interfaces;
using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.DataAccessLayer.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace AltFutureWebApp.Areas.Portfolios.Controllers
{
    [Area("Portfolios")]
    public class DashboardController : Controller
    {
        private readonly IPortfolioSummaryRepository _portfolioSummaryRepository;
        private readonly ICoinMarketCapAPI _cmcAPI;
        private readonly ICryptoRepository _cryptoRepository;
        private readonly ICryptoPriceRepository _cryptoPriceRepository;
        private readonly ICryptoAssetChartData _chartsData;

        public DashboardController(IPortfolioSummaryRepository portfolioSummaryRepository, ICoinMarketCapAPI cmcAPI,ICryptoRepository cryptoRepository, ICryptoPriceRepository cryptoPriceRepository, ICryptoAssetChartData chartsData)
        {
            _portfolioSummaryRepository = portfolioSummaryRepository;
            _cmcAPI = cmcAPI;
            _cryptoRepository = cryptoRepository;
            _cryptoPriceRepository = cryptoPriceRepository;
            _chartsData = chartsData;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _portfolioSummaryRepository.GetAllAsync());
        }


        [HttpGet]
        public async Task<JsonResult> GetAssetAllocationData()
        {

            var cryptoInvestmentPercentages = await _chartsData.GetCryptoAssetAllocationDataAsync(1);

            var data = new JArray { };

            data.Add(new JArray { "Crypto", "Allocation" });

            cryptoInvestmentPercentages.ForEach(c =>
            {
                data.Add(new JArray { c.TickerSymbol, c.InvestmentPercentage });
            });


            return Json(data);
        }


        [HttpGet]
        public async Task<JsonResult> GetAssetPerformanceData()
        {

            var cryptoInvestmentPerformances = await _chartsData.GetCryptoAssetPerformanceDataAsync(1);

            var data = new JArray { };

            data.Add(new JArray { "Crypto Assets", "Investment", "Current Worth" });  

            cryptoInvestmentPerformances.ForEach(c =>
            {
                data.Add(new JArray { c.TickerSymbol, c.Investment, c.Investment + c.UnrealizedProfit }); 
            });


            return Json(data);
        }

    }
}
