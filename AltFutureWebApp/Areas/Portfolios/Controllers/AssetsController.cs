using AltFuture.CoinMarketCapAPI.Interfaces;
using AltFuture.CoinMarketCapAPI.Models;
using AltFuture.CoinMarketCapAPI.Services;
using AltFuture.DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AltFutureWebApp.Areas.Portfolios.Controllers
{
    [Area("Portfolios")]
    public class AssetsController : Controller
    {

        private readonly IPortfolioSummaryRepository _portfolioSummaryRepository;
        private readonly ICoinMarketCapAPI _cmcAPI;

        public AssetsController(IPortfolioSummaryRepository portfolioSummaryRepository, ICoinMarketCapAPI cmcAPI)
        {
            _portfolioSummaryRepository = portfolioSummaryRepository;
            _cmcAPI = cmcAPI;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _portfolioSummaryRepository.GetAllAsync());

     }
    }
}
