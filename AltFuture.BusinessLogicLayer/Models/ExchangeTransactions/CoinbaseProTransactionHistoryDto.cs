using AltFuture.BusinessLogicLayer.Interfaces.Models;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltFuture.BusinessLogicLayer.Models.ExchangeTransactions
{
    public class CoinbaseProTransactionHistoryDto : IExchangeTransactionHistoryDto
    {
        [Name("created at")]
        public DateTime TransactionDate { get; set; }

        [Name("size unit")]
        public string CryptoAsset { get; set; }

        [Name("side")]
        public string ExchangeTransactionTypeName { get; set; }

        [Name("price")]
        public decimal Price { get; set; }

        [Name("size")]
        public decimal Quantity { get; set; }

        [Name("fee")]
        public decimal Fee { get; set; }


        private decimal transactionTotal = Decimal.Zero;
        [Name("total")]
        public decimal TransactionTotal { get => transactionTotal * -1; set => transactionTotal = value; }

    }
}
