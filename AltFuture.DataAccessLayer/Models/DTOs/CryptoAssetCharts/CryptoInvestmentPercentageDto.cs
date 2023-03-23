namespace AltFuture.DataAccessLayer.Models.DTOs.CryptoAssetCharts
{
    public class CryptoInvestmentPercentageDto
    {
        public int CryptoId { get; set; }
        public string TickerSymbol { get; set; }
        public decimal InvestmentPercentage { get; set; }
    }
}
