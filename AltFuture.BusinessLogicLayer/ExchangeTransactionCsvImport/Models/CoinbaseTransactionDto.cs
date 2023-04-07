using AltFuture.BusinessLogicLayer.ExchangeTransactionCsvImport.Interfaces;
using AltFuture.DataAccessLayer.Data.Enums;
using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace AltFuture.BusinessLogicLayer.ExchangeTransactionCsvImport.Models
{
    public class CoinbaseTransactionDto : IExchangeTransactionDto
    {

        public int ExchangeId { get => (int)ExchangeEnum.Coinbase; }

        [Name("Timestamp")]
        public DateTime TransactionDate { get; set; }

        [Name("Transaction Type")]
        public string ExchangeTransactionTypeName { get; set; }

        [Name("Asset")]
        public string CryptoAsset { get; set; }

        [Name("Quantity Transacted")]
        public float QuantityCsv { get; set; }


        [Ignore]
        public decimal Quantity { get => (decimal)QuantityCsv; set => _ = value; }

        [Name("Spot Price Currency")]
        public string Currency { get; set; }

        [Name("Spot Price at Transaction")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,10)")]
        public decimal Subtotal { get; set; }

        [Name("Total (inclusive of fees and/or spread)")]
        [Column(TypeName = "decimal(18,10)")]
        public decimal TransactionTotal { get; set; }

        [Name("Fees and/or Spread")]
        [Column(TypeName = "decimal(18,10)")]
        public decimal Fee { get; set; }
    }
}
