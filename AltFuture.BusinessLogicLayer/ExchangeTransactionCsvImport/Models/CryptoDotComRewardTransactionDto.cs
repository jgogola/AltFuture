using AltFuture.BusinessLogicLayer.ExchangeTransactionCsvImport.Interfaces;
using AltFuture.DataAccessLayer.Data.Enums;
using CsvHelper.Configuration.Attributes;


namespace AltFuture.BusinessLogicLayer.ExchangeTransactionCsvImport.Models
{
    public class CryptoDotComRewardTransactionDto : IExchangeTransactionDto
    {

        public int ExchangeId { get => (int)ExchangeEnum.CryptoDotCom; }

        [Name("Timestamp (UTC)")]
        public DateTime TransactionDate { get; set; }

        [Name("Transaction Kind")]
        public string ExchangeTransactionTypeName { get; set; }

        [Name("Currency")]
        public string? CryptoAsset { get; set; } = null;


        [Ignore]
        [Default(defaultValue: 0)]
        public decimal Price { get; set; } = decimal.Zero; //Value should always be zero for rewards


        [Name("Amount")]
        [Default(defaultValue: 0)]
        public decimal Quantity { get; set; } = decimal.Zero;

        [Ignore]
        [Default(defaultValue: 0)]
        public decimal Fee { get; set; } = decimal.Zero; //Value should always be zero for rewards


        private decimal transactionTotal = decimal.Zero;
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
