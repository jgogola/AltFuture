using AltFuture.CoinMarketCapAPI.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Options;
using AltFuture.CoinMarketCapAPI.Models.KeyInfo;
using AltFuture.CoinMarketCapAPI.Models.EndPoints;
using AltFuture.CoinMarketCapAPI.Models.CryptoQuotes;

namespace AltFuture.CoinMarketCapAPI.Services
{
    public class CoinMarketCapAPIOLD : ICoinMarketCapAPI
    {

        private readonly IHttpClientFactory _clientFactory;
        private readonly CoinMarketCapEndPoints _endPoints;

        public CoinMarketCapAPIOLD(IHttpClientFactory clientFactory, IOptions<CoinMarketCapEndPoints> endPoints)
        {
            _clientFactory = clientFactory;
            _endPoints = endPoints.Value;
        }

        public async Task<KeyInfo> GetKeyInfoAsync()
        {
            var client = _clientFactory.CreateClient("CoinMarketCapPro");
            var endpointUrl = $"{_endPoints.V1.KeyInfo}";

            HttpResponseMessage response = await client.GetAsync(endpointUrl.ToString());
            response.EnsureSuccessStatusCode();

            JsonObject jsonResponce = await response.Content.ReadFromJsonAsync<JsonObject>();
            var keyInfo = JsonConvert.DeserializeObject<KeyInfo>(jsonResponce["data"].ToString());

            return keyInfo;

            //var keyInfoRoot = await response.Content.ReadFromJsonAsync<KeyInfoRoot>();
            // return keyInfoRoot.KeyInfo;
        }

        public async Task<IEnumerable<CryptoQuote>> GetQuotesLatestAsync(Dictionary<int, string> tickerDictionary, string[]? fiatSymbols = null)
        {
            if (fiatSymbols == null) { fiatSymbols = new string[] { "USD" }; };

            var client = _clientFactory.CreateClient("CoinMarketCapPro");
            var endpointUrl = $"{_endPoints.V2.QuotesLatest}?symbol={string.Join(",", tickerDictionary.Values.ToArray())}&convert={string.Join(",", fiatSymbols)}";

            HttpResponseMessage response = await client.GetAsync(endpointUrl.ToString());
            response.EnsureSuccessStatusCode();
            JsonObject jsonResponce = await response.Content.ReadFromJsonAsync<JsonObject>();

            var cryptoQuotes = new List<CryptoQuote>();
            for (int i = 0; i < tickerDictionary.Count; i++)
            {
                //data:BTC[0]
                var cryptoQuote = JsonConvert.DeserializeObject<CryptoQuote>(jsonResponce["data"][tickerDictionary.ElementAt(i).Value][0].ToString());
                cryptoQuote.CryptoId = tickerDictionary.ElementAt(i).Key;


                //data:BTC[0]:quote:USD[0]
                var fiatPrice = JsonConvert.DeserializeObject<FiatPrice>(jsonResponce["data"][tickerDictionary.ElementAt(i).Value][0]["quote"][fiatSymbols[0]].ToString());

                fiatPrice.FiatSymbol = fiatSymbols[0];
                cryptoQuote.FiatPrice = fiatPrice;

                cryptoQuotes.Add(cryptoQuote);
            }

            return cryptoQuotes;
        }

        public async Task<IEnumerable<CryptoQuote>> GetQuotesLatestSandboxAsync(Dictionary<int, string> tickerDictionary, string[]? fiatSymbols = null)
        {
            if (fiatSymbols == null) { fiatSymbols = new string[] { "USD" }; };

            var client = _clientFactory.CreateClient("CoinMarketCapSandbox");
            var endpointUrl = $"{_endPoints.V2.QuotesLatest}?symbol={string.Join(",", tickerDictionary.Values.ToArray())}&convert={string.Join(",", fiatSymbols)}";

            HttpResponseMessage response = await client.GetAsync(endpointUrl.ToString());
            response.EnsureSuccessStatusCode();
            JsonObject jsonResponce = await response.Content.ReadFromJsonAsync<JsonObject>();

            var cryptoQuotes = new List<CryptoQuote>();
            for (int i = 0; i < tickerDictionary.Count; i++)
            {
                //data:BTC[0]
                var cryptoQuote = JsonConvert.DeserializeObject<CryptoQuote>(jsonResponce["data"][tickerDictionary.ElementAt(i).Value][0].ToString());
                cryptoQuote.CryptoId = tickerDictionary.ElementAt(i).Key;

                //data:BTC[0]:quote:USD[0]
                var fiatPrice = JsonConvert.DeserializeObject<FiatPrice>(jsonResponce["data"][tickerDictionary.ElementAt(i).Value][0]["quote"][fiatSymbols[0]].ToString());

                fiatPrice.FiatSymbol = fiatSymbols[0];
                cryptoQuote.FiatPrice = fiatPrice;

                cryptoQuotes.Add(cryptoQuote);
            }

            return cryptoQuotes;
        }
    }
}
