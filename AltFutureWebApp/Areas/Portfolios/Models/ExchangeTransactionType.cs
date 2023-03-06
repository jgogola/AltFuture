﻿using AltFutureWebApp.Areas.Portfolios.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace AltFutureWebApp.Areas.Portfolios.Models
{
    public class ExchangeTransactionType
    {
        public int ExchangeTransactionTypeId { get; set; }
        public string ExchangeTransactionTypeName { get; set;}

        [ForeignKey(nameof(Exchange))]
        public int ExchageId { get; set; }

        public Exchange Exchage { get; set; }

        public CommonTransactionType CommonTransactionType { get; set; }
    }
}
