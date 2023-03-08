using AltFutureWebApp.Models;
using AltFutureWebApp.Repository;
using AltFutureWebApp.Data;
using AltFutureWebApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AltFutureWebApp.Areas.Portfolios.Controllers
{
    [Area("Portfolios")]
    public class DashboardController : Controller
    {
        private readonly ICryptoRepository _cryptoRepository;

        public DashboardController(ICryptoRepository cryptoRepository)
        {
            _cryptoRepository = cryptoRepository;
        }

        public async Task<IActionResult> Index()
        {

            //var i = await _cryptoRepository.GetCountAsync();
            var crypto = await _cryptoRepository.GetByIdAsync(1);

            return View(crypto);
        }

        public IActionResult Test()
        {
            return View();
        }

        //private async Task<AppDbContext> GetDbContext()
        //{
        //    var options = new DbContextOptionsBuilder<AppDbContext>()
        //       .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        //       .Options;

        //    var databaseContext = new AppDbContext(options);
        //    databaseContext.Database.EnsureCreated();


        //    var count = await databaseContext.Cryptos.CountAsync();
        //    if (count == 0)
        //    {
        //        for (int i = 0; i < 10; i++)
        //        {
        //            databaseContext.Cryptos.Add(
        //              new Crypto()
        //              {
        //                  CryptoName = "Bitcoin",
        //                  TickerSymbol = "BTC"
        //              });
        //            await databaseContext.SaveChangesAsync();
        //        }
        //    }
        //    return databaseContext;
        //}
    }
}
