using Newtonsoft.Json;

namespace AltFuture.CoinMarketCapAPI.Models.KeyInfo
{
    public class Plan
    {
        [JsonProperty("credit_limit_daily")]
        public int CreditLimitDaily { get; set; }

        [JsonProperty("credit_limit_daily_reset")]
        public string CreditLimitDailyReset { get; set; }

        [JsonProperty("credit_limit_daily_reset_timestamp")]
        public DateTime CreditLimitDailyResetTimestamp { get; set; }

        [JsonProperty("credit_limit_monthly")]
        public int CreditLimitmonthly { get; set; }

        [JsonProperty("credit_limit_monthly_reset")]
        public string CreditLimitMonthlyReset { get; set; }

        [JsonProperty("credit_limit_monthly_reset_timestamp")]
        public DateTime CreditLimitMonthlyResetTimestamp { get; set; }

        [JsonProperty("rate_limit_minute")]
        public int RateLimitMinute { get; set; }
    }




}
