using AltFuture.MarketDataConsumer.Models.MarketDataClient;

namespace AltFuture.BusinessLogicLayer.Interfaces
{
    public interface IMarketDataService
    {
        Task<DataPlanUsage> GetDataPlanUsageAsync();

        Task<DateTime> SyncMarketPricesCacheAsync();

        Task<DateTime> SyncMarketPricesAsync();
    }
}
