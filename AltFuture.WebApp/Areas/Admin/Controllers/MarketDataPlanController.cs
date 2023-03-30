using AltFuture.BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AltFuture.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MarketDataPlanController : Controller
    {
        private readonly IMarketDataService _marketDataService;

        public MarketDataPlanController(IMarketDataService marketDataService)
        {
            _marketDataService = marketDataService;
        }

        // GET: MarketDataPlanController
        public async Task<ActionResult> Index()
        {
            var dataPlanUsage = await _marketDataService.GetDataPlanUsageAsync();
            return View(dataPlanUsage);
        }


       
    }
}
