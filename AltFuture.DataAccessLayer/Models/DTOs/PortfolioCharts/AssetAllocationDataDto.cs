namespace AltFuture.DataAccessLayer.Models.DTOs.PortfolioCharts
{
    public class AssetAllocationDataDto
    {
        public int CryptoId { get; set; }
        public string TickerSymbol { get; set; }
        public decimal InvestmentPercentage { get; set; }
    }
}
