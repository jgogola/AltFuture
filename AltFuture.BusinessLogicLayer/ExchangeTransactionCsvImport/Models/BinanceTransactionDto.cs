using AltFuture.BusinessLogicLayer.ExchangeTransactionCsvImport.Interfaces;
using AltFuture.DataAccessLayer.Data.Enums;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltFuture.BusinessLogicLayer.ExchangeTransactionCsvImport.Models
{
    public class BinanceTransactionDto : IExchangeTransactionDto
    {
        public int ExchangeId { get => (int)ExchangeEnum.Binance; }

        [Name("Transaction_Id")]
        public int TransactionReferenceNum { get; set; }

        [Name("Base_Asset")]
        public string CryptoAsset { get; set; }

        [Name("Operation")]
        public string ExchangeTransactionTypeName { get; set; }

        [Ignore]
        public decimal Price { get => investmentTotal / Quantity; set => _ = value; }

        [Name("Realized_Amount_For_Base_Asset")]
        public decimal Quantity { get; set; }

        [Ignore]
        public decimal Fee { get; set; }

        [Name("Realized_Amount_For_Fee_Asset_In_USD_Value")]
        public float FeeAsFloat
        {
            get => (float)Fee;
            set => Fee = (decimal)value;
        }
   
        private decimal investmentTotal = decimal.Zero;
        [Name("Realized_Amount_For_Quote_Asset_In_USD_Value")]
        public decimal TransactionTotal { get => investmentTotal + Fee; set => investmentTotal = value; }

        [Name("Time")]
        public DateTime TransactionDate { get; set; }
    }
}
