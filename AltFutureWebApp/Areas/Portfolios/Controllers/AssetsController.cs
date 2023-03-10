using AltFutureWebApp.Data;
using AltFutureWebApp.Models;
using AltFutureWebApp.Models.StoredProcs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using AltFutureWebApp.Interfaces;

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
