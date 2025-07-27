
using AltFuture.BusinessLogicLayer.Interfaces;
using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.DataAccessLayer.Models;
using AltFuture.WebApp.Enums;
using AltFuture.WebApp.Helpers;
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

            try
            {

                //* Import current market data:
                var timeCheck = DateTime.Now;
                var marketDataLastSynced = await _marketDataService.SyncMarketPricesCacheAsync();
                ViewBag.DateLastSynced = marketDataLastSynced;

                if (marketDataLastSynced >= timeCheck)
                {
                    var userMessagePartial = new UserMessagePartial(TempData);
                    userMessagePartial.SetUserMessage(
                        UserMessageTypes.System,
                        $"<li>Market data was synced at {marketDataLastSynced}.",
                        8
                    );
                }

                var latestPrices = await _cryptoPricesRepository.GetLatestPricesAsync();
                return View(latestPrices.OrderBy(p => p.Crypto.SortRank).ToList());
            }
            catch (Exception ex)
            {
                var userMessagePartial = new UserMessagePartial(TempData);
                userMessagePartial.SetUserMessage(
                    UserMessageTypes.Alert,
                    $"Error: {ex.Message}.",
                    8
                );

                return View(new List<CryptoPrice>());
            }
            
        }

        public IActionResult About()
        {
            return View();
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