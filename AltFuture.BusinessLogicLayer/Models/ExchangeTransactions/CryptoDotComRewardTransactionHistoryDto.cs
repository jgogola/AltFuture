using AltFuture.BusinessLogicLayer.Interfaces.Models;
using CsvHelper.Configuration.Attributes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;


namespace AltFuture.BusinessLogicLayer.Models.ExchangeTransactions
{
    public class CryptoDotComRewardTransactionHistoryDto : IExchangeTransactionHistoryDto
    {
        [Name("Timestamp (UTC)")]
        public DateTime TransactionDate { get; set; }

        [Name("Transaction Kind")]
        public string ExchangeTransactionTypeName { get; set; }

        [Name("Currency")]
        public string? CryptoAsset { get; set; } = null;


        [Ignore]
        [Default(defaultValue: 0)]
        public decimal Price { get; set; } = Decimal.Zero; //Value should always be zero for rewards


        [Name("Amount")]
        [Default(defaultValue: 0)]
        public decimal Quantity { get; set; } = Decimal.Zero;

        [Ignore]
        [Default(defaultValue: 0)]
        public decimal Fee { get; set; } = Decimal.Zero; //Value should always be zero for rewards


        private decimal transactionTotal = Decimal.Zero;
        [Name("Native Amount (in USD)")]
        [Default(defaultValue: 0)]
        public decimal TransactionTotal
        { 
            get => Quantity < 0 && transactionTotal > 0 ? transactionTotal * -1 : transactionTotal; 
            set => transactionTotal = value; 
        }


        [Name("Transaction Description")]
        public string TransactionDescription { get; set; }



        [Name("Transaction Hash")]
        public string TransactionHash { get; set; }


    }
}
