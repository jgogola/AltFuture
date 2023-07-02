using AltFuture.DataAccessLayer.Data.Enums;
using AltFuture.DataAccessLayer.Models;
using Newtonsoft.Json;

namespace AltFuture.BusinessLogicLayer.ExchangTransactionApiImport.JsonConverters
{
    internal class ExchangeTransactionJsonConverterFactory
    {

        public ExchangeTransactionJsonConverterFactory()
        {
            
        }

        public JsonConverter CreateJsonConverter(ExchangeEnum exchangeConverterType, int appUserId, DateTime createdDate, List<Crypto> cryptoAssets, List<ExchangeTransactionType> exchangeTransactionTypes)
        {
            switch (exchangeConverterType)
            {
                case ExchangeEnum.Coinbase:
                    return new CoinbaseTransactionJsonConverter(appUserId, createdDate, cryptoAssets, exchangeTransactionTypes);
                //case ExchangeEnum.Binance:
                //    return new BinanceTransactionJsonConverter();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
