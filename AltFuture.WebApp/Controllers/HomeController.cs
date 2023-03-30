
using AltFuture.BusinessLogicLayer.Interfaces;
using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AltFuture.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICryptoPriceRepository _cryptoPricesRepository;
        private readonly IMarketDataService _marketDataService;

        public HomeController(ILogger<HomeController> logger, 
                              ICryptoPriceRepository cryptoPricesRepository, 
                              IMarketDataService marketDataService)
        {
            _logger = logger;
            _cryptoPricesRepository = cryptoPricesRepository;
            _marketDataService = marketDataService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.DateLastSynced = await _marketDataService.SyncMarketPricesCacheAsync();

            return View(await _cryptoPricesRepository.GetLatestPricesAsync());
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