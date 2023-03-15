using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace AltFuture.Models.StoredProcs
{
    public class PortfolioSummaryGetAll
    {
        public int CryptoId { get; set; }

        [DisplayName("Crypto")]
        public string CryptoName { get; set; }

        [DisplayName("Ticker Symbol")]
        public string TickerSymbol { get; set; }

        [DisplayName("Num of Orders")]
        public int NumberOfOrders { get; set; }

        [Column(TypeName = "decimal(10,3)")]
        public int Quantity { get; set; }

        [DisplayName("Avg Buy Price")]
        [Column(TypeName = "decimal(10,3)")]
        public decimal AverageBuyPrice { get; set; }

        [Column(TypeName = "decimal(10,3)")]
        public decimal Price { get; set; }

        public DateTime? DateRecorded { get; set; }


        [DisplayName("Total Invested")]
        [Column(TypeName = "decimal(10,3)")]
        public decimal TotalInvested { get; set; }


        [DisplayName("Unrealized Profits")]
        [Column(TypeName = "decimal(10,3)")]
        public decimal UnrealizedProfit { get; set; }

        [DisplayName("Current Worth")]
        [Column(TypeName = "decimal(10,3)")]
        public decimal CurrentWorth { get; set; }

        public int RowType { get; set; }
    }
}
