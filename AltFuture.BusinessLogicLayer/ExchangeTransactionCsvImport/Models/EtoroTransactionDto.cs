using AltFuture.BusinessLogicLayer.ExchangeTransactionCsvImport.Interfaces;
using AltFuture.DataAccessLayer.Data.Enums;
using CsvHelper.Configuration.Attributes;


namespace AltFuture.BusinessLogicLayer.ExchangeTransactionCsvImport.Models
{
    public class EtoroTransactionDto : IExchangeTransactionDto
    {

        public int ExchangeId { get => (int)ExchangeEnum.Etoro; }

        [Name("Date")]
        public DateTime TransactionDate { get; set; }


        [Name("Type")]
        public string ExchangeTransactionTypeName { get; set; }


        private string cryptoAsset = string.Empty;
        [Name("Details")]
        public string CryptoAsset { get => cryptoAsset.Replace("/USD", ""); set => cryptoAsset = value; }


        [Name("Amount")]
        public decimal TransactionTotal { get; set; }

        [Name("Units")]
        public decimal Quantity { get; set; }


        [Name("Position ID")]
        public int TransactionReferenceNum { get; set; }



        // private decimal priceSetIgnore = Decimal.Zero;
        [Ignore]
        public decimal Price { get => TransactionTotal / Quantity; set => _ = value; }


        [Ignore]
        public decimal Fee { get; set; } = decimal.Zero;


    }
}
