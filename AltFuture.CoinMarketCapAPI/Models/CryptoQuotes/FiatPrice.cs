using Newtonsoft.Json;

namespace AltFuture.CoinMarketCapAPI.Models.CryptoQuotes
{
    public class FiatPrice
    {
        public string FiatSymbol { get; set; }
        public decimal Price { get; set; } = decimal.Zero;

        [JsonProperty("volume_24h")]
        public decimal? Volume24h { get; set; } = decimal.Zero;

        [JsonProperty("volume_change_24h")]
        public decimal? VolumeChange24h { get; set; } = decimal.Zero;

        [JsonProperty("percent_change_1h")]
        public decimal? PercentChange1h { get; set; } = decimal.Zero;

        [JsonProperty("percent_change_24h")]
        public decimal? PercentChange24h { get; set; } = decimal.Zero;

        [JsonProperty("percent_change_7d")]
        public decimal? PercentChange7d { get; set; } = decimal.Zero;

        [JsonProperty("percent_change_30d")]
        public decimal? PercentChange30d { get; set; } = decimal.Zero;

        [JsonProperty("percent_change_60d")]
        public decimal? PercentChange60d { get; set; } = decimal.Zero;

        [JsonProperty("percent_change_90d")]
        public decimal? PercentChange90d { get; set; } = decimal.Zero;

        [JsonProperty("market_cap")]
        public decimal? MarketCap { get; set; } = decimal.Zero;

        [JsonProperty("market_cap_dominance")]
        public decimal? MarketCapDominance { get; set; } = decimal.Zero;

    }

}
