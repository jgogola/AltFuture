namespace AltFuture.DataAccessLayer.Models.DTOs.PortfolioCharts
{
    public class ExchangeUsageDataDto
    {
        public int ExchangeId { get; set; }
        public string ExchangeName { get; set; }
        public decimal UsagePercentage { get; set; }
    }
}
