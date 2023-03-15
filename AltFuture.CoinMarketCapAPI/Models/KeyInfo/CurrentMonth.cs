using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltFuture.CoinMarketCapAPI.Models.KeyInfo
{

    public class CurrentMonth
    {
        [JsonProperty("credits_used")]
        public int CreditsUsed { get; set; }

        [JsonProperty("credits_left")]
        public int CreditsLeft { get; set; }
    }




}
