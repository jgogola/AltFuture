using AltFuture.BusinessLogicLayer.Interfaces;
using AltFuture.DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Newtonsoft.Json.Linq;

namespace AltFutureWebApp.Areas.Portfolios.Controllers
{
    [Area("Portfolios")]
    public class DashboardController : Controller
    {
        private readonly ICryptoRepository _cryptoRepository;
        private readonly ICryptoPriceRepository _cryptoPriceRepository;
        private readonly IDashboardChartsData _chartsData;

        public DashboardController(ICryptoRepository cryptoRepository, ICryptoPriceRepository cryptoPriceRepository, IDashboardChartsData chartsData)
        {
            _cryptoRepository = cryptoRepository;
            _cryptoPriceRepository = cryptoPriceRepository;
            _chartsData = chartsData;
        }

        public async Task<IActionResult> Index()
        {            
            var crypto = await _cryptoRepository.GetByIdAsync(1);

            return View(crypto);
        }

        [HttpGet]
        public async Task<JsonResult> GetData()
        {

            var cryptoInvestmentPercentages = await _chartsData.GetCryptoInvestmentPercentageAsync(1);

            var data = new JArray { };

            data.Add(new JArray { "Crypto", "Percentage" });

            cryptoInvestmentPercentages.ForEach(c =>
            {
                data.Add(new JArray { c.TickerSymbol, c.InvestmentPercentage });
            });


            return Json(data);
        }
   
    }
}
