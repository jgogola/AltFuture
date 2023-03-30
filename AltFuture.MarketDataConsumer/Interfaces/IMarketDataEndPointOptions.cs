
namespace AltFuture.MarketDataConsumer.Interfaces
{
    public interface IMarketDataEndPointOptions
    {
        int RateLimitHours { get; set; }

        string DataPlanUsage { get; set; }

        string MarketPriceData { get; set; }
    }

}
