using AltFuture.CoinMarketCapAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltFuture.CoinMarketCapAPI.Interfaces
{
    internal interface ICoinMarketCapAPI
    {
        Task<IEnumerable<CryptoQuote>> GetQuotesLatestAsync(string[] tickerSymbols, string[] fiatSymbols);

        Task<IEnumerable<CryptoQuote>> GetQuotesLatestSandboxAsync(string[] tickerSymbols, string[] fiatSymbols);

    }
}
