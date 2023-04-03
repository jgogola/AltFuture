using AltFuture.DataAccessLayer.Models;
using AutoMapper;
using AltFuture.DataAccessLayer.Interfaces.Services;
using AltFuture.BusinessLogicLayer.Models.ExchangeTransactions;
using AltFuture.BusinessLogicLayer.Interfaces.Models;

namespace AltFuture.BusinessLogicLayer.AutoMapper.Transactions.Resolvers
{
    internal class CryptoAssetResolver : IValueResolver<IExchangeTransactionHistoryDto, Transaction, int>
    {
        private readonly List<Crypto> _cryptoLookup;

        public CryptoAssetResolver(ICryptoDataService cryptoDataService)
        {
            _cryptoLookup = cryptoDataService.CryptoList;
        }

        public int Resolve(IExchangeTransactionHistoryDto source, Transaction destination, int destMember, ResolutionContext context)
        {
            return _cryptoLookup.FirstOrDefault(c => c.TickerSymbol == source.CryptoAsset).CryptoId;
        }
    }
}
