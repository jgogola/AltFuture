using AltFuture.BusinessLogicLayer.ExchangeTransactionCsvImport.Interfaces;
using CsvHelper.Configuration.Attributes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;


namespace AltFuture.BusinessLogicLayer.ExchangeTransactionCsvImport.Models
{
    public class CryptoDotComTransactionDto : IExchangeTransactionDto
    {
        [Name("Timestamp (UTC)")]
        public DateTime TransactionDate { get; set; }

        [Name("Transaction Kind")]
        public string ExchangeTransactionType { get; set; }

        [Name("Transaction Description")]
        public string TransactionDescription { get; set; }

        public string Currency { get; set; }

        [Column(TypeName = "decimal(18,10)")]
        [Default(defaultValue: 0)]
        public decimal Amount { get; set; } = decimal.Zero;

        [Name("To Currency")]
        public string ToCurrency { get; set; }

        [Name("To Amount")]
        [Column(TypeName = "decimal(18,10)")]
        [Default(defaultValue:0)]
        public decimal ToAmount { get; set; } = decimal.Zero;

        [Name("Native Currency")]
        public string NativeCurrency { get; set; }

        [Name("Native Amount")]
        [Column(TypeName = "decimal(18,10)")]
        [Default(defaultValue: 0)]
        public decimal NativeAmount { get; set; } = decimal.Zero;

        [Name("Native Amount (in USD)")]
        [Column(TypeName = "decimal(18,10)")]
        [Default(defaultValue: 0)]
        public decimal NativeAmountUsd { get; set; } = decimal.Zero;

        [Name("Transaction Hash")]
        public string TransactionHash { get; set; }

        [Ignore]
        public string? CryptoAsset { get; set; } = null;

    }
}
