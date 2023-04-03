using AltFuture.DataAccessLayer.Models;
using AutoMapper;
using AltFuture.DataAccessLayer.Interfaces.Services;
using AltFuture.BusinessLogicLayer.Models.ExchangeTransactions;

namespace AltFuture.BusinessLogicLayer.AutoMapper.CoinbaseTransactionHistoryToTransaction
{
    internal class CoinbaseCryptoResolver : IValueResolver<CoinbaseTransactionHistoryDto, Transaction, int>
    {
        private readonly List<Crypto> _cryptoLookup;

        public CoinbaseCryptoResolver(ICryptoDataService cryptoDataService)
        {
            _cryptoLookup = cryptoDataService.CryptoList;
        }

        public int Resolve(CoinbaseTransactionHistoryDto source, Transaction destination, int destMember, ResolutionContext context)
        {
            return _cryptoLookup.FirstOrDefault(c => c.TickerSymbol == source.Asset).CryptoId;
        }
    }
}
