using AltFuture.CoinMarketCapAPI.Models.CryptoQuotes;
using AltFuture.CoinMarketCapAPI.Models.KeyInfo;

namespace AltFuture.CoinMarketCapAPI.Interfaces
{
    public interface ICoinMarketCapAPI
    {
        Task<IEnumerable<CryptoQuote>> GetQuotesLatestAsync(Dictionary<int, string> tickerDictionary, string[]? fiatSymbols = null);

        Task<IEnumerable<CryptoQuote>> GetQuotesLatestSandboxAsync(Dictionary<int, string> tickerDictionary, string[]? fiatSymbols = null);

        Task<KeyInfo> GetKeyInfoAsync();

    }
}
