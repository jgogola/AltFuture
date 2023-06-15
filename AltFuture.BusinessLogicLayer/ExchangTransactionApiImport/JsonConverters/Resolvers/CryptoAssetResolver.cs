using AltFuture.DataAccessLayer.Data.Enums;
using AltFuture.DataAccessLayer.Models;

namespace AltFuture.BusinessLogicLayer.ExchangTransactionApiImport.JsonConverters.Resolvers;

internal class CryptoAssetResolver
{

    private readonly List<Crypto> _cryptoAssets;

    public CryptoAssetResolver(List<Crypto> cryptoAssets)
    {
        _cryptoAssets = cryptoAssets;
    }

    public int Resolve(ExchangeEnum exchangeId, string cryptoAsset)
    {
        switch(exchangeId)
        {
            case ExchangeEnum.Coinbase:
                cryptoAsset = cryptoAsset.Replace("-USD","");
                break;
        }

        return _cryptoAssets.FirstOrDefault(c => c.TickerSymbol == cryptoAsset).CryptoId;
    }
}
