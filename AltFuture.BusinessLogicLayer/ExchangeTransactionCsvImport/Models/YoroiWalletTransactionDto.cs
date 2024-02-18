using AltFuture.BusinessLogicLayer.ExchangeTransactionCsvImport.Interfaces;
using AltFuture.DataAccessLayer.Data.Enums;
using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace AltFuture.BusinessLogicLayer.ExchangeTransactionCsvImport.Models
{
    //* Note: the annotations are for use with the CsvHelper. Not AutoMapper.
    //* Some fields are used for the purposes of data translation.

    public class YoroiWalletTransactionDto : IExchangeTransactionDto
    {

        public int ExchangeId { get => (int)ExchangeEnum.YoroiWallet; }

        [Name("Date")]
        public DateTime TransactionDate { get; set; }

        [Ignore]
        public string ExchangeTransactionTypeName { get => YoroiComment.Contains("Staking Reward") ? "Staking Reward" : YoroiTransactionType; set => _ = value; }

        [Name("Buy Cur.")]
        public string CryptoAsset { get; set; }

        [Ignore]
        public decimal Quantity { get => Convert.ToDecimal(_yoroiQuantity); set => _ = value; }

        [Ignore]
        public decimal Price { get => 0.00m; set => _ = value; }

        [Ignore]
        public decimal TransactionTotal { get => 0.00m; set => _ = value; }

        [Ignore]
        public decimal Fee { get => 0.00m; set => _ = value; }



        [Name("Type (Trade, IN or OUT)")]
        public string YoroiTransactionType { get; set; }

        private string _yoroiQuantity;
        [Name("Buy Amount")]
        public string YoroiQuantity
        {
            get { return _yoroiQuantity; }
            set
            {
                _yoroiQuantity = value == "" ? "0" : value;
            }
        }

        [Name("Comment (optional)")]
        public string YoroiComment { get; set; }
    }
}
