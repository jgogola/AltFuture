using AltFuture.DataAccessLayer.Interfaces.Services;
using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.DataAccessLayer.Models;
using AltFuture.MarketDataConsumer.Interfaces;
using AltFuture.MarketDataConsumer.Models.MarketDataClient;
using AutoMapper;
using AltFuture.BusinessLogicLayer.Interfaces;

namespace AltFuture.BusinessLogicLayer.Services.MarketData
{
    public class MarketDataService : IMarketDataService
    {
        private readonly IMarketDataClient _marketDataClient;
        private readonly List<Crypto> _cryptos;
        private readonly ICryptoPriceRepository _cryptoPriceRepository;
        private readonly IMapper _mapper;

        public MarketDataService(IMarketDataClient marketDataClient, 
                                 ICryptoDataService cryptoDataService, 
                                 ICryptoPriceRepository cryptoPriceRepository, 
                                 IMapper mapper)
        {
            _marketDataClient = marketDataClient;
            _cryptos = cryptoDataService.CryptoList;
            _cryptoPriceRepository = cryptoPriceRepository;
            _mapper = mapper;
        }

        public async Task<DataPlanUsage> GetDataPlanUsageAsync()
        {
            return await _marketDataClient.GetDataPlanUsageAsync();
        }

        public async Task<DateTime> SyncMarketPricesCacheAsync()
        {
            var dateLastSynced = await _cryptoPriceRepository.GetLastSyncedDate();

            var isApiSyncAllowed = false;
            if (dateLastSynced.AddHours(_marketDataClient.RateLimitHours) <= DateTime.Now)
            {
                isApiSyncAllowed = true;
            }

            if (isApiSyncAllowed)
            {
                dateLastSynced = DateTime.Now;
                var tickerDictionary = _cryptos.ToDictionary(crypto => crypto.CryptoId, crypto => crypto.TickerSymbol);
                var incommingMarketDataPrices = await _marketDataClient.GetLatestMarketPriceDataAsync(tickerDictionary);

                if (incommingMarketDataPrices != null)
                {
                    var mappedCryptoPrices = _mapper.Map<List<CryptoPrice>>(incommingMarketDataPrices);


                    _ = await _cryptoPriceRepository.AddRangeAsync(mappedCryptoPrices);
                }
            }

            return dateLastSynced;
        }


        public async Task<DateTime> SyncMarketPricesAsync()
        {
            var dateLastSynced = await _cryptoPriceRepository.GetLastSyncedDate();

            var tickerDictionary = _cryptos.ToDictionary(crypto => crypto.CryptoId, crypto => crypto.TickerSymbol);
            var incommingMarketDataPrices = await _marketDataClient.GetLatestMarketPriceDataAsync(tickerDictionary);

            if (incommingMarketDataPrices != null)
            {
                var mappedCryptoPrices = _mapper.Map<List<CryptoPrice>>(incommingMarketDataPrices);

                _ = await _cryptoPriceRepository.AddRangeAsync(mappedCryptoPrices);
            }

            return dateLastSynced;

        }

    }
}
