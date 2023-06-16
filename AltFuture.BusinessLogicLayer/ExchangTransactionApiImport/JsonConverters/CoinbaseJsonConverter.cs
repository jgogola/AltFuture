using AltFuture.BusinessLogicLayer.ExchangTransactionApiImport.JsonConverters.Resolvers;
using AltFuture.DataAccessLayer.Data.Enums;
using AltFuture.DataAccessLayer.Interfaces.Services;
using AltFuture.DataAccessLayer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltFuture.BusinessLogicLayer.ExchangTransactionApiImport.JsonConverters
{
    internal class CoinbaseJsonConverter : JsonConverter
    {
        private readonly int _appUserId;
        private readonly DateTime _createdDate;
        private readonly List<Crypto> _cryptoAssets;
        private readonly List<ExchangeTransactionType> _exchangeTransactionTypes;

        public CoinbaseJsonConverter(int appUserId, DateTime createdDate, List<Crypto> cryptoAssets, List<ExchangeTransactionType> exchangeTransactionTypes)
        {
            _appUserId = appUserId;
            _createdDate = createdDate;
            _cryptoAssets = cryptoAssets;
            _exchangeTransactionTypes = exchangeTransactionTypes;
        }
        public override bool CanConvert(Type objectType)
        {
            var x = objectType == typeof(Transaction);
            return x;
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var cryptoAssetResolver = new CryptoAssetResolver(_cryptoAssets);
            var exchangeTransactionTypeResolver = new ExchangeTransactionTypeResolver(_exchangeTransactionTypes);

            var json = JObject.Load(reader);
            var transaction = new Transaction();
            transaction.AppUserId = _appUserId;
            transaction.FromExchangeId = (int)ExchangeEnum.Coinbase;
            transaction.CryptoId = cryptoAssetResolver.Resolve(ExchangeEnum.Coinbase, json["product_id"].ToString());
            transaction.ExchangeTransactionTypeId = exchangeTransactionTypeResolver.Resolve(ExchangeEnum.Coinbase, json["side"].ToString());
            transaction.Price = (decimal)json["price"];
            transaction.Quantity = (decimal)json["size"];
            transaction.Fee = (decimal)json["commission"];
            transaction.TransactionTotal = transaction.Price * transaction.Quantity + transaction.Fee;
            transaction.TransactionDate = (DateTime)json["trade_time"];
            transaction.CreatedDate = _createdDate;
            return transaction;
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
