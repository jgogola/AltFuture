﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace AltFuture.DataAccessLayer.Models.StoredProcs
{
    public class PortfolioSummary
    {
        public int CryptoId { get; set; }

        [DisplayName("Crypto")]
        public string CryptoName { get; set; }

        [DisplayName("Ticker Symbol")]
        public string TickerSymbol { get; set; }

        private const string defaultIcon = "<svg height=\"32\" viewBox=\"0 0 32 32\" width=\"32\" xmlns=\"http://www.w3.org/2000/svg\"><g fill=\"none\" fill-rule=\"evenodd\"><circle cx=\"16\" cy=\"16\" fill=\"#efb914\" fill-rule=\"nonzero\" r=\"16\"/><path d=\"M21.002 9.855A7.947 7.947 0 0124 15.278l-2.847-.708a5.357 5.357 0 00-3.86-3.667c-2.866-.713-5.76.991-6.465 3.806s1.05 5.675 3.917 6.388a5.373 5.373 0 005.134-1.43l2.847.707a7.974 7.974 0 01-5.2 3.385L16.716 27l-2.596-.645.644-2.575a8.28 8.28 0 01-1.298-.323l-.643 2.575-2.596-.646.81-3.241c-2.378-1.875-3.575-4.996-2.804-8.081s3.297-5.281 6.28-5.823L15.323 5l2.596.645-.644 2.575a8.28 8.28 0 011.298.323l.643-2.575 2.596.646z\" fill=\"#fff\"/></g></svg>";
        public string CryptoIcon { get; set; } = defaultIcon;

        [DisplayName("Num of Orders")]
        public int NumberOfOrders { get; set; }

        [Column(TypeName = "decimal(10,3)")]
        public decimal Quantity { get; set; }

        [DisplayName("Avg Buy Price")]
        [Column(TypeName = "decimal(10,3)")]
        public decimal AverageBuyPrice { get; set; }

        [DisplayName("Current Price")]
        [Column(TypeName = "decimal(10,3)")]
        public decimal Price { get; set; }

        [DisplayName("24hr Change")]
        [Column(TypeName = "decimal(10,3)")]
        public decimal PercentChange24h { get; set; }

        public DateTime? DateRecorded { get; set; }


        [DisplayName("Total Invested")]
        [Column(TypeName = "decimal(10,3)")]
        public decimal InvestmentTotal { get; set; }

        [DisplayName("Total Fees")]
        [Column(TypeName = "decimal(10,3)")]
        public decimal FeeTotal { get; set; }


        [DisplayName("Unrealized P/L")]
        [Column(TypeName = "decimal(10,3)")]
        public decimal UnrealizedProfit { get; set; }

        [DisplayName("Current Worth")]
        [Column(TypeName = "decimal(10,3)")]
        public decimal CurrentWorth { get; set; }

        public int RowType { get; set; }
    }
}
