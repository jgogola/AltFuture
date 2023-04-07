using AltFuture.DataAccessLayer.Models;
using AutoMapper;
using AltFuture.DataAccessLayer.Interfaces.Services;
using AltFuture.BusinessLogicLayer.ExchangeTransactionCsvImport.Interfaces;

namespace AltFuture.BusinessLogicLayer.ExchangeTransactionCsvImport.AutoMapper.Resolvers
{
    internal class CryptoAssetResolver : IValueResolver<IExchangeTransactionDto, Transaction, int>
    {
        private readonly List<Crypto> _cryptoLookup;

        public CryptoAssetResolver(ICryptoDataService cryptoDataService)
        {
            _cryptoLookup = cryptoDataService.CryptoList;
        }

        public int Resolve(IExchangeTransactionDto source, Transaction destination, int destMember, ResolutionContext context)
        {
            return _cryptoLookup.FirstOrDefault(c => c.TickerSymbol == source.CryptoAsset).CryptoId;
        }
    }
}
