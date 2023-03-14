using Newtonsoft.Json;

namespace AltFuture.CoinMarketCapAPI.Models
{
    public class CryptoQuote
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }

        [JsonProperty("max_supply")]
        public long? MaxSupply { get; set; } = 0;

        [JsonProperty("circulating_supply")]
        public long? CirculatingSupply { get; set; } = 0;

        [JsonProperty("total_supply")]
        public long? TotalSupply { get; set; } = 0;

        [JsonProperty("cmc_rank")]
        public int? CmcRank { get; set; }

        [JsonProperty("tvl_ratio")]
        public object TvlRatio { get; set; }

        public FiatPrice FiatPrice { get; set; }
    }
}
