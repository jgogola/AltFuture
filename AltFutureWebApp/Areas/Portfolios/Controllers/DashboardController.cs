using AltFuture.DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AltFutureWebApp.Areas.Portfolios.Controllers
{
    [Area("Portfolios")]
    public class DashboardController : Controller
    {
        private readonly ICryptoRepository _cryptoRepository;
        private readonly ICryptoPriceRepository _cryptoPriceRepository;


        public DashboardController(ICryptoRepository cryptoRepository, ICryptoPriceRepository cryptoPriceRepository)
        {
            _cryptoRepository = cryptoRepository;
            _cryptoPriceRepository = cryptoPriceRepository;

        }

        public async Task<IActionResult> Index()
        {            
            var crypto = await _cryptoRepository.GetByIdAsync(1);

            return View(crypto);
        }

   
    }
}
