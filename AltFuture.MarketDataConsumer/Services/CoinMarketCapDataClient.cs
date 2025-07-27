using AltFuture.MarketDataConsumer.Interfaces;
using AltFuture.MarketDataConsumer.Models.CoinMarketCap;
using AltFuture.MarketDataConsumer.Models.MarketDataClient;
using AutoMapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace AltFuture.MarketDataConsumer.Services
{
    public class CoinMarketCapDataClient : IMarketDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly CoinMarketCapEndPointOptions _endPointOptions;
        private readonly IMapper _mapper;

        public CoinMarketCapDataClient(HttpClient httpClient, 
                                       IOptions<CoinMarketCapEndPointOptions> endPointOptions, 
                                       IMapper mapper)
        {
            _httpClient = httpClient;
            _endPointOptions = endPointOptions.Value;
            _mapper = mapper;
        }

        public int RateLimitHours => _endPointOptions.RateLimitHours;
        public int RateLimitMinutes => _endPointOptions.RateLimitMinutes;

        public async Task<DataPlanUsage> GetDataPlanUsageAsync()
        {
            var endpointUrl = _endPointOptions.DataPlanUsage;

            HttpResponseMessage response = await _httpClient.GetAsync(endpointUrl.ToString());
            response.EnsureSuccessStatusCode();

            JsonObject jsonResponce = await response.Content.ReadFromJsonAsync<JsonObject>();
            var incommingPlanUsage = JsonConvert.DeserializeObject<CoinMarketCapPlanUsage>(jsonResponce["data"].ToString());
            
            var mappedDataPlanUsage = _mapper.Map<DataPlanUsage>(incommingPlanUsage); 

            return mappedDataPlanUsage;
        }

        public async Task<IEnumerable<MarketPriceData>> GetLatestMarketPriceDataAsync(Dictionary<int, string> tickerDictionary) 
        {
            const string fiatSymbol = "USD";

            var baseAddress = _httpClient.BaseAddress;

            var endpointUrl = $"{_endPointOptions.MarketPriceData}?symbol={string.Join(",", tickerDictionary.Values.ToArray())}&convert={fiatSymbol}";

            HttpResponseMessage response = await _httpClient.GetAsync(endpointUrl.ToString());
            response.EnsureSuccessStatusCode();
            JsonObject jsonResponce = await response.Content.ReadFromJsonAsync<JsonObject>();

            var incommingMarketPriceData = new List<CoinMarketCapPriceData>();
            
            for (int i = 0; i < tickerDictionary.Count; i++)
            {
                //data:BTC[0]
                var marketPrice = JsonConvert.DeserializeObject<CoinMarketCapPriceData>(jsonResponce["data"][tickerDictionary.ElementAt(i).Value][0].ToString());
                marketPrice.CryptoId = tickerDictionary.ElementAt(i).Key;

                incommingMarketPriceData.Add(marketPrice);
            }

            var mappedMarketPriceData = _mapper.Map<IEnumerable<MarketPriceData>>(incommingMarketPriceData, opts => { opts.Items["Timestamp"] = DateTime.Now; });

            return mappedMarketPriceData;
        }

    }
}
