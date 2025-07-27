using AltFuture.MarketDataConsumer.Interfaces;

namespace AltFuture.MarketDataConsumer.Models.CoinMarketCap
{
    public class CoinMarketCapEndPointOptions : IMarketDataEndPointOptions
    {
        public const string SettingsSection = "CoinMarketCapSettings:EndPointOptions";
        public int RateLimitHours { get; set; } = 0;
        public int RateLimitMinutes { get; set; } = 0;
        public string DataPlanUsage { get; set; } = String.Empty;
        public string MarketPriceData { get; set; } = String.Empty;
    }

}
