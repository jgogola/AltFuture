
using Newtonsoft.Json;

namespace AltFuture.MarketDataConsumer.Models.MarketDataClient
{
    public class DataPlanUsage
    {
        public string DataPlanName { get; set; }

        public Plan Plan { get; set; }

        public Usage Usage { get; set; }
    }

    public class Plan
    {
        public int CreditLimitDaily { get; set; }

        public string CreditLimitDailyReset { get; set; }

        public DateTime CreditLimitDailyResetTimestamp { get; set; }

        public int CreditLimitmonthly { get; set; }

        public string CreditLimitMonthlyReset { get; set; }

        public DateTime CreditLimitMonthlyResetTimestamp { get; set; }

        public int RateLimitMinute { get; set; }
    }

    public class Usage
    {
        public CurrentMinute CurrentMinute { get; set; }

        public CurrentDay CurrentDay { get; set; }

        public CurrentMonth CurrentMonth { get; set; }
    }

    public class CurrentMinute
    {
        public int RequestsMade { get; set; }

        public int RequestsLeft { get; set; }
    }

    public class CurrentDay
    {
        public int CreditsUsed { get; set; }

        public int CreditsLeft { get; set; }
    }

    public class CurrentMonth
    {
        public int CreditsUsed { get; set; }

        public int CreditsLeft { get; set; }
    }

}
