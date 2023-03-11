using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AltFutureWebApp.Models.StoredProcs
{
    public class PortfolioSummaryGetAll
    {
        public int CryptoId { get; set; }

        public string CryptoName { get; set; }

        public string TickerSymbol { get; set; }
         public int NumberOfOrders { get; set; }

        [Column(TypeName = "decimal(10,3)")]
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(10,3)")]
        public decimal AverageBuyPrice { get; set; }

        [Column(TypeName = "decimal(10,3)")]
        public decimal Price { get; set; }

        public DateTime? DateRecorded { get; set; }

        [Column(TypeName = "decimal(10,3)")]
        public decimal TotalInvested { get; set; }

        [Column(TypeName = "decimal(10,3)")]
        public decimal UnrealizedProfit { get; set; }

        [Column(TypeName = "decimal(10,3)")]
        public decimal CurrentWorth { get; set; }

        public int RowType { get; set; }
    }
}
