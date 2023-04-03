using AltFuture.BusinessLogicLayer.Interfaces.Models;
using CsvHelper.Configuration.Attributes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;


namespace AltFuture.BusinessLogicLayer.Models.ExchangeTransactions
{
    public class CryptoDotComBuyTransactionHistoryDto : IExchangeTransactionHistoryDto
    {
        [Name("Timestamp (UTC)")]
        public DateTime TransactionDate { get; set; }

        [Name("Transaction Kind")]
        public string ExchangeTransactionTypeName { get; set; }

        [Name("To Currency")]
        public string? CryptoAsset { get; set; } = null;


        private decimal ignoreSetPrice = Decimal.Zero;
        [Ignore]
        public decimal Price { get => TransactionTotal / Quantity; set => ignoreSetPrice = value; }


       // private decimal quantity = Decimal.Zero;
        [Name("To Amount")]
        [Default(defaultValue: 0)]
        public decimal Quantity { get; set; } = Decimal.Zero;

        [Ignore]
        [Default(defaultValue: 0)]
        public decimal Fee { get; set; } = Decimal.Zero;


       // private decimal transactionTotal = Decimal.Zero;
        [Name("Native Amount (in USD)")]
        [Default(defaultValue: 0)]
        public decimal TransactionTotal { get; set; }


        [Name("Transaction Description")]
        public string TransactionDescription { get; set; }



        [Name("Transaction Hash")]
        public string TransactionHash { get; set; }


    }
}
