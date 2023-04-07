using AltFuture.BusinessLogicLayer.ExchangeTransactionCsvImport.Interfaces;
using AltFuture.DataAccessLayer.Data.Enums;
using CsvHelper.Configuration.Attributes;


namespace AltFuture.BusinessLogicLayer.ExchangeTransactionCsvImport.Models
{
    public class CoinbaseProTransactionDto : IExchangeTransactionDto
    {

        public int ExchangeId { get => (int)ExchangeEnum.CoinbasePro; }

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


        private decimal transactionTotal = decimal.Zero;
        [Name("total")]
        public decimal TransactionTotal { get => transactionTotal * -1; set => transactionTotal = value; }

    }
}
