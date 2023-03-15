using Newtonsoft.Json;

namespace AltFuture.CoinMarketCapAPI.Models.KeyInfo
{
    public class CurrentDay
    {
        [JsonProperty("credits_used")]
        public int CreditsUsed { get; set; }

        [JsonProperty("credits_left")]
        public int CreditsLeft { get; set; }
    }




}
