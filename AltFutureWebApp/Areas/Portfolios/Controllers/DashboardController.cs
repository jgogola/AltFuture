using AltFuture.CoinMarketCapAPI.Interfaces;
using AltFuture.CoinMarketCapAPI.Models.CryptoQuotes;
using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.Models;
using Microsoft.AspNetCore.Mvc;

namespace AltFutureWebApp.Areas.Portfolios.Controllers
{
    [Area("Portfolios")]
    public class DashboardController : Controller
    {
        private readonly ICryptoRepository _cryptoRepository;
        private readonly ICryptoPriceRepository _cryptoPriceRepository;
        private readonly ICoinMarketCapAPI _cmcApi;

        public DashboardController(ICryptoRepository cryptoRepository, ICryptoPriceRepository cryptoPriceRepository, ICoinMarketCapAPI cmcApi)
        {
            _cryptoRepository = cryptoRepository;
            _cryptoPriceRepository = cryptoPriceRepository;
            _cmcApi = cmcApi;
        }

        public async Task<IActionResult> Index()
        {            
            var cryptos =  await _cryptoRepository.GetAllAsync();
            var tickerDictionary = cryptos.ToDictionary(crypto => crypto.CryptoId, crypto => crypto.TickerSymbol);

            var cryptoQuotes = await _cmcApi.GetQuotesLatestAsync(tickerDictionary);

            if(cryptoQuotes != null)
            {
                var cryptoPrices = new List<CryptoPrice>();
                var dateRecorded = DateTime.Now;
                foreach (CryptoQuote cryptoQuote in cryptoQuotes)
                {
                    var cryptoPrice = new CryptoPrice()
                    {
                        DateRecorded = dateRecorded,
                        CryptoId = cryptoQuote.CryptoId,
                        Price = cryptoQuote.FiatPrice.Price
                    };

                    cryptoPrices.Add(cryptoPrice);
                }

                _ = _cryptoPriceRepository.AddRangeAsync(cryptoPrices);
            }


            //var i = await _cryptoRepository.GetCountAsync();
            var crypto = await _cryptoRepository.GetByIdAsync(1);

            return View(crypto);
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
