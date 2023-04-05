using AltFuture.BusinessLogicLayer.Interfaces.Models;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltFuture.BusinessLogicLayer.Models.ExchangeTransactions
{
    public class EtoroTransactionHistoryDto : IExchangeTransactionHistoryDto
    {
        [Name("Date")]
        public DateTime TransactionDate { get; set; }


        [Name("Type")]
        public string ExchangeTransactionTypeName { get; set; }


        private string cryptoAsset = string.Empty;
        [Name("Details")]
        public string CryptoAsset { get => cryptoAsset.Replace("/USD","") ; set => cryptoAsset = value; }


        [Name("Amount")]
        public decimal TransactionTotal { get; set; }

        [Name("Units")]
        public decimal Quantity { get; set; }


        [Name("Position ID")]
        public int TransactionReferenceNum { get; set; }



       // private decimal priceSetIgnore = Decimal.Zero;
        [Ignore]
        public decimal Price { get => TransactionTotal / Quantity ; set=> _ = value; }


        [Ignore]
        public decimal Fee { get; set; } = Decimal.Zero;


    }
}
