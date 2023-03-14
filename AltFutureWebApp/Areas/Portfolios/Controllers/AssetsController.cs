using AltFuture.CoinMarketCapAPI;
using AltFuture.CoinMarketCapAPI.Models;
using AltFuture.DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AltFutureWebApp.Areas.Portfolios.Controllers
{
    [Area("Portfolios")]
    public class AssetsController : Controller
    {

        private readonly IPortfolioSummaryRepository _portfolioSummaryRepository;
        private readonly CoinMarketCapAPI _cmcAPI;

        public AssetsController(IPortfolioSummaryRepository portfolioSummaryRepository, CoinMarketCapAPI cmcAPI)
        {
            _portfolioSummaryRepository = portfolioSummaryRepository;
            _cmcAPI = cmcAPI;
        }

        public async Task<IActionResult> Index()
        {
            string[] tickerSymbols = { "BTC" };
            string[] currencySymbols = { "USD" };

            List<CryptoQuote> cryptoQuotes = (List<CryptoQuote>)await _cmcAPI.GetQuotesLatestSandboxAsync(tickerSymbols, currencySymbols);

            ViewBag.Quotes = $"Name: {cryptoQuotes[0].Name} Max Supply: {cryptoQuotes[0].MaxSupply} Fiat: {cryptoQuotes[0].FiatPrice.FiatSymbol} Price: {cryptoQuotes[0].FiatPrice.Price} Volume24: {cryptoQuotes[0].FiatPrice.Volume24h}";

            return View(await _portfolioSummaryRepository.GetAllAsync());

     }
    }
}
