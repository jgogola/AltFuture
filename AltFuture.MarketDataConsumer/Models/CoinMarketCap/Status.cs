using Newtonsoft.Json;

namespace AltFuture.MarketDataConsumer.Models.CoinMarketCap
{
    public class Status
    {
        public DateTime Timestamp { get; set; }

        [JsonProperty("error_code")]
        public int ErrorCode { get; set; }

        [JsonProperty("error_message")]
        public object ErrorMessage { get; set; }
        public int Elapsed { get; set; }

        [JsonProperty("credit_count")]
        public int CreditCount { get; set; }
        public object Notice { get; set; }
    }

}
