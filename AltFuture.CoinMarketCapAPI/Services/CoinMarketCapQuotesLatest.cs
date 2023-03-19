﻿using AltFuture.CoinMarketCapAPI.Interfaces;
using AltFuture.CoinMarketCapAPI.Models.CryptoQuotes;
using AltFuture.CoinMarketCapAPI.Models.EndPoints;
using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.DataAccessLayer.Interfaces.Services;
using AltFuture.DataAccessLayer.Services;
using AltFuture.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltFuture.CoinMarketCapAPI.Services
{
    public class CoinMarketCapQuotesLatest : ICoinMarketCapQuotesLatest
    {

        private readonly List<Crypto> _cryptos;
        private readonly CoinMarketCapEndPoints _coinMarketCapEndPoints;
        private readonly ICryptoPriceRepository _cryptoPriceRepository;
        private readonly ICoinMarketCapAPI _cmcApi;


        public CoinMarketCapQuotesLatest(IOptions<CoinMarketCapEndPoints> coinMarketCapEndPoints, ICryptoDataService cryptoDataService, ICryptoPriceRepository cryptoPriceRepository, ICoinMarketCapAPI cmcApi)
        {
            _cryptos = cryptoDataService.CryptoList;
            _coinMarketCapEndPoints = coinMarketCapEndPoints.Value;
            _cryptoPriceRepository = cryptoPriceRepository;
            _cmcApi = cmcApi;
        }

        public async Task<DateTime> SyncCacheAsync()
        {
            var dateLastSynced = await _cryptoPriceRepository.GetLastSynced();

            var isApiSyncAllowed = false;
            if(dateLastSynced.AddHours(_coinMarketCapEndPoints.RateLimitHours) <= DateTime.Now )
            {
                isApiSyncAllowed = true;
            }

            if (isApiSyncAllowed)
            {
                var tickerDictionary = _cryptos.ToDictionary(crypto => crypto.CryptoId, crypto => crypto.TickerSymbol);
                var cryptoQuotes = await _cmcApi.GetQuotesLatestAsync(tickerDictionary);

                if (cryptoQuotes != null)
                {
                    var cryptoPrices = new List<CryptoPrice>();
                    var dateRecorded = DateTime.Now;
                    dateLastSynced = dateRecorded;
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
                    _ = await _cryptoPriceRepository.AddRangeAsync(cryptoPrices);
                }
            }

            return dateLastSynced;
        }

        public async Task<DateTime> SyncAsync()
        {
            var dateLastSynced = await _cryptoPriceRepository.GetLastSynced();

            var tickerDictionary = _cryptos.ToDictionary(crypto => crypto.CryptoId, crypto => crypto.TickerSymbol);
            var cryptoQuotes = await _cmcApi.GetQuotesLatestAsync(tickerDictionary);

            if (cryptoQuotes != null)
            {
                var cryptoPrices = new List<CryptoPrice>();
                var dateRecorded = DateTime.Now;
                dateLastSynced = dateRecorded;
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
                _ = await _cryptoPriceRepository.AddRangeAsync(cryptoPrices);
            }

            return dateLastSynced;


        }
    }
}
