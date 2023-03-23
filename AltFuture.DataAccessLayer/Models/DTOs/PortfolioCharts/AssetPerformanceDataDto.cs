namespace AltFuture.DataAccessLayer.Models.DTOs.PortfolioCharts
{
    public class AssetPerformanceDataDto
    {
        public int CryptoId { get; set; }
        public string TickerSymbol { get; set; }
        public decimal Investment { get; set; }
        public decimal UnrealizedProfit { get; set; }
    }
}
