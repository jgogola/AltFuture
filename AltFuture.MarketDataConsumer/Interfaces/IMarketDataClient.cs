using AltFuture.MarketDataConsumer.Models.MarketDataClient;

namespace AltFuture.MarketDataConsumer.Interfaces
{
    public interface IMarketDataClient
    {
        int RateLimitHours { get; }

        Task<DataPlanUsage> GetDataPlanUsageAsync();

        Task<IEnumerable<MarketPriceData>> GetLatestMarketPriceDataAsync(Dictionary<int, string> tickerDictionary);

    }
}
