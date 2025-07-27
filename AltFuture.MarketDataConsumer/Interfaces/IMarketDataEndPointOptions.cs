
namespace AltFuture.MarketDataConsumer.Interfaces
{
    public interface IMarketDataEndPointOptions
    {
        int RateLimitHours { get; set; }

        int RateLimitMinutes { get; set; }

        string DataPlanUsage { get; set; }

        string MarketPriceData { get; set; }
    }

}
