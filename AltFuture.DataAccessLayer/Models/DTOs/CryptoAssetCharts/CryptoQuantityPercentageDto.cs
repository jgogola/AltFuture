namespace AltFuture.DataAccessLayer.Models.DTOs.CryptoAssetCharts
{
    public class CryptoQuantityPercentageDto
    {
        public int CryptoId { get; set; }
        public string TickerSymbol { get; set; }
        public decimal QuantityPercentage { get; set; }
    }
}
