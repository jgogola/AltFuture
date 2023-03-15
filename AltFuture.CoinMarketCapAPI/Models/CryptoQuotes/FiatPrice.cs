using Newtonsoft.Json;

namespace AltFuture.CoinMarketCapAPI.Models.CryptoQuotes
{
    public class FiatPrice
    {
        public string FiatSymbol { get; set; }
        public decimal Price { get; set; } = 0.00M;

        [JsonProperty("volume_24h")]
        public decimal? Volume24h { get; set; } = 0.00M;

        [JsonProperty("volume_change_24h")]
        public decimal? VolumeChange24h { get; set; } = 0.00M;

        [JsonProperty("percent_change_1h")]
        public decimal? PercentChange1h { get; set; } = 0.00M;

        [JsonProperty("percent_change_24h")]
        public decimal? PercentChange24h { get; set; } = 0.00M;

        [JsonProperty("percent_change_7d")]
        public decimal? PercentChange7d { get; set; } = 0.00M;

        [JsonProperty("percent_change_30d")]
        public decimal? PercentChange30d { get; set; } = 0.00M;

        [JsonProperty("percent_change_60d")]
        public decimal? PercentChange60d { get; set; } = 0.00M;

        [JsonProperty("percent_change_90d")]
        public decimal? PercentChange90d { get; set; } = 0.00M;

    }

}
