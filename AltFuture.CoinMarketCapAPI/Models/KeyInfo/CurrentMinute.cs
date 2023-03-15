using Newtonsoft.Json;

namespace AltFuture.CoinMarketCapAPI.Models.KeyInfo
{
    public class CurrentMinute
    {
        [JsonProperty("requests_made")]
        public int RequestsMade { get; set; }

        [JsonProperty("requests_left")]
        public int RequestsLeft { get; set; }
    }




}
