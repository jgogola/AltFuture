
using Newtonsoft.Json;

namespace AltFuture.MarketDataConsumer.Models.MarketDataClient
{
    public class MarketPriceData
    {
        public int CryptoId { get; set; }
        public string CryptoName { get; set; }
        public string TickerSymbol { get; set; }
        public long? MaxSupply { get; set; } = 0;
        public long? CirculatingSupply { get; set; } = 0;
        public long? TotalSupply { get; set; } = 0;
        public int MarketRank { get; set; } = 0;
        public DateTime DateRecorded { get; set; } 
        public string FiatSymbol { get; set; } = string.Empty;
        public decimal MarketPrice { get; set; } = decimal.Zero;
        public decimal? Volume24h { get; set; } = decimal.Zero;
        public decimal? VolumeChange24h { get; set; } = decimal.Zero;
        public decimal? PercentChange1h { get; set; } = decimal.Zero;
        public decimal? PercentChange24h { get; set; } = decimal.Zero;
        public decimal? PercentChange7d { get; set; } = decimal.Zero;
        public decimal? PercentChange30d { get; set; } = decimal.Zero;
        public decimal? PercentChange60d { get; set; } = decimal.Zero;
        public decimal? PercentChange90d { get; set; } = decimal.Zero;
        public decimal? MarketCap { get; set; } = decimal.Zero;
        public decimal? MarketCapDominance { get; set; } = decimal.Zero;
    }
}
