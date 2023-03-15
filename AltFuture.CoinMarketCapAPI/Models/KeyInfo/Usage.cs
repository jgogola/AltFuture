using Newtonsoft.Json;

namespace AltFuture.CoinMarketCapAPI.Models.KeyInfo
{
    public class Usage
    {
        [JsonProperty("current_minute")]
        public CurrentMinute CurrentMinute { get; set; }

        [JsonProperty("current_day")]
        public CurrentDay CurrentDay { get; set; }

        [JsonProperty("current_month")]
        public CurrentMonth CurrentMonth { get; set; }
    }




}
