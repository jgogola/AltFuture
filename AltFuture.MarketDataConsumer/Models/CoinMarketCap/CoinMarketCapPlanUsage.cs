using Newtonsoft.Json;

namespace AltFuture.MarketDataConsumer.Models.CoinMarketCap
{
    public class CoinMarketCapPlanUsage
    {
        public string DataPlanName  { get;} = "CoinMarketCap";
        public Plan Plan { get; set; }
        public Usage Usage { get; set; }
    }

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

    public class Usage
    {
        [JsonProperty("current_minute")]
        public CurrentMinute CurrentMinute { get; set; }

        [JsonProperty("current_day")]
        public CurrentDay CurrentDay { get; set; }

        [JsonProperty("current_month")]
        public CurrentMonth CurrentMonth { get; set; }
    }

    public class CurrentMinute
    {
        [JsonProperty("requests_made")]
        public int RequestsMade { get; set; }

        [JsonProperty("requests_left")]
        public int RequestsLeft { get; set; }
    }
    public class CurrentDay
    {
        [JsonProperty("credits_used")]
        public int CreditsUsed { get; set; }

        [JsonProperty("credits_left")]
        public int CreditsLeft { get; set; }
    }

    public class CurrentMonth
    {
        [JsonProperty("credits_used")]
        public int CreditsUsed { get; set; }

        [JsonProperty("credits_left")]
        public int CreditsLeft { get; set; }
    }

}
