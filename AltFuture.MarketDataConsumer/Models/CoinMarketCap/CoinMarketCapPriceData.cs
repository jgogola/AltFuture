using Newtonsoft.Json;

namespace AltFuture.MarketDataConsumer.Models.CoinMarketCap
{
    public class CoinMarketCapPriceData
    {
        public int CryptoId { get; set; }

        [JsonProperty("id")]
        public int CmcId { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }

        [JsonProperty("max_supply")]
        public long? MaxSupply { get; set; } = 0;

        [JsonProperty("circulating_supply")]
        public long? CirculatingSupply { get; set; } = 0;

        [JsonProperty("total_supply")]
        public long? TotalSupply { get; set; } = 0;

        [JsonProperty("cmc_rank")]
        public int CmcRank { get; set; } = 0;

        //[JsonProperty("tvl_ratio")]
        //public object TvlRatio { get; set; }

        public FiatPrice FiatPrice { get; set; }
    }

    public class FiatPrice
    {
        public string FiatSymbol { get; set; } = string.Empty;
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
