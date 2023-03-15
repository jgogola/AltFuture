using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltFuture.CoinMarketCapAPI.Models.KeyInfo
{
    public class KeyInfoRoot
    {

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("data")]
        public KeyInfo KeyInfo { get; set; }


        
    }
}
