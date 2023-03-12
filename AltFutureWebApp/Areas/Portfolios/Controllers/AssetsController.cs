using AltFuture.DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AltFutureWebApp.Areas.Portfolios.Controllers
{
    [Area("Portfolios")]
    public class AssetsController : Controller
    {

        private readonly IPortfolioSummaryRepository _portfolioSummaryRepository;

        public AssetsController(IPortfolioSummaryRepository portfolioSummaryRepository)
        {
            _portfolioSummaryRepository = portfolioSummaryRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _portfolioSummaryRepository.GetAllAsync());

     }
    }
}
