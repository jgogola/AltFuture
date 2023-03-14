using AltFuture.CoinMarketCapAPI.Interfaces;
using AltFuture.CoinMarketCapAPI.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Http;
using System.Net.Http.Json;
using System;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Options;

namespace AltFuture.CoinMarketCapAPI
{
    public class CoinMarketCapAPI : ICoinMarketCapAPI
    {

        private readonly IHttpClientFactory _clientFactory;
        private readonly CoinMarketCapEndPointsV2 _endPointsV2;

        public CoinMarketCapAPI(IHttpClientFactory clientFactory, IOptions<CoinMarketCapEndPointsV2> endPointsV2)
        {
            _clientFactory = clientFactory;
            _endPointsV2 = endPointsV2.Value;
        }

        public async Task<IEnumerable<CryptoQuote>> GetQuotesLatestAsync(string[] tickerSymbols, string[] fiatSymbols)
        {
            var client = _clientFactory.CreateClient("CoinMarketCapPro");

            var endpointUrl = $"{_endPointsV2.QuotesLatest}?symbol={string.Join(",", tickerSymbols)}&convert={string.Join(",", fiatSymbols)}";

            HttpResponseMessage response = await client.GetAsync(endpointUrl.ToString());
            response.EnsureSuccessStatusCode();
            JsonObject jsonResponce = await response.Content.ReadFromJsonAsync<JsonObject>();

            var cryptoQuotes = new List<CryptoQuote>();
            for (int i = 0; i < tickerSymbols.Length; i++)
            {
                //data:BTC[0]
                var cryptoQuote = JsonConvert.DeserializeObject<CryptoQuote>(jsonResponce["data"][tickerSymbols[i]][0].ToString());

                //data:BTC[0]:quote:USD[0]
                var fiatPrice = JsonConvert.DeserializeObject<FiatPrice>(jsonResponce["data"][tickerSymbols[i]][0]["quote"][fiatSymbols[0]].ToString());

                fiatPrice.FiatSymbol = fiatSymbols[0];
                cryptoQuote.FiatPrice = fiatPrice;

                cryptoQuotes.Add(cryptoQuote);
            }

            return cryptoQuotes;
        }

        public async Task<IEnumerable<CryptoQuote>> GetQuotesLatestSandboxAsync(string[] tickerSymbols, string[] fiatSymbols)
        {
            var client = _clientFactory.CreateClient("CoinMarketCapSandbox");

            var endpointUrl = $"{_endPointsV2.QuotesLatest}?symbol={string.Join(",", tickerSymbols)}&convert={string.Join(",", fiatSymbols)}";

            HttpResponseMessage response = await client.GetAsync(endpointUrl.ToString());
            response.EnsureSuccessStatusCode();
            JsonObject jsonResponce = await response.Content.ReadFromJsonAsync<JsonObject>();

            var cryptoQuotes = new List<CryptoQuote>();
            for (int i = 0; i < tickerSymbols.Length; i++)
            {
                //data:BTC[0]
                var cryptoQuote = JsonConvert.DeserializeObject<CryptoQuote>(jsonResponce["data"][tickerSymbols[i]][0].ToString());
             
                //data:BTC[0]:quote:USD[0]
                var fiatPrice = JsonConvert.DeserializeObject<FiatPrice>(jsonResponce["data"][tickerSymbols[i]][0]["quote"][fiatSymbols[0]].ToString());
                
                fiatPrice.FiatSymbol = fiatSymbols[0];
                cryptoQuote.FiatPrice = fiatPrice;

                cryptoQuotes.Add(cryptoQuote);
            }

            return cryptoQuotes;
        }
    }
}
