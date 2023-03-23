namespace AltFuture.DataAccessLayer.Models.DTOs.CryptoAssetCharts
{
    public class CryptoInvestmentPerformanceDto
    {
        public int CryptoId { get; set; }
        public string TickerSymbol { get; set; }
        public decimal Investment { get; set; }

        public decimal UnrealizedProfit { get; set; }
    }
}
