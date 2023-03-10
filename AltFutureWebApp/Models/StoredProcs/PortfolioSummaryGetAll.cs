namespace AltFutureWebApp.Models.StoredProcs
{
    public class PortfolioSummaryGetAll
    {
        public int CryptoId { get; set; }

        public string CryptoName { get; set; }

        public string Ticker { get; set; }
         public int NumberOfOrders { get; set; }

        public int Quantity { get; set; }

        public decimal AverageBuyPrice { get; set; }

        public decimal CryptoPrice { get; set; }

        public DateTime DateRecorded { get; set; }

        public decimal TotalInvested { get; set; }

        public decimal UnrealizedProfit { get; set; }

        public decimal CurrentWorth { get; set; }

        public int RowType { get; set; }
    }
}
