
using AltFuture.CoinMarketCapAPI.Interfaces;
using AltFuture.DataAccessLayer.Interfaces;
using AltFutureWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AltFutureWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICryptoPriceRepository _cryptoPricesRepository;
        private readonly ICoinMarketCapQuotesLatest _coinMarketCapQuotesLatest;

        public HomeController(ILogger<HomeController> logger, 
                              ICryptoPriceRepository cryptoPricesRepository, 
                              ICoinMarketCapQuotesLatest coinMarketCapQuotesLatest)
        {
            _logger = logger;
            _cryptoPricesRepository = cryptoPricesRepository;
            _coinMarketCapQuotesLatest = coinMarketCapQuotesLatest;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.DateLastSynced = await _coinMarketCapQuotesLatest.SyncCacheAsync();

            return View(await _cryptoPricesRepository.GetLatestAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}