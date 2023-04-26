using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AltFuture.BusinessLogicLayer.ViewModels.Transactions
{
    public class TransactionCreate
    {


        private int? transactionReferenceNum;
        [DisplayName("Transaction Reference Number")]
        public int? TransactionReferenceNum { get => transactionReferenceNum; set => transactionReferenceNum = value is null ? 0 : value; } 

        public int AppUserId { get; set; }

        [DisplayName("Crypto Currency")]
        [Required(ErrorMessage ="Please select a crypto currency.")]
        public int CryptoId { get; set; }

        [DisplayName("Transaction Type")]
        [Required]
        public int ExchangeTransactionTypeId { get; set; }

        public decimal Price { get; set; } = decimal.Zero;

        public decimal Quantity { get; set; } = decimal.Zero;

        public decimal Fee { get; set; } = decimal.Zero;

        [DisplayName("Transaction Total")]
        public decimal TransactionTotal { get; set; } = decimal.Zero;

        [DisplayName("Transaction Date")]
        public DateTime TransactionDate { get; set; }

        [DisplayName("Exchange")]
        [Required]
        public int FromExchangeId { get; set; }

        private int? toExchangeId;
        [DisplayName("Transfered to Exchange")]
        public int? ToExchangeId { get => toExchangeId == 0 ? null : toExchangeId; set => toExchangeId = value; }

    }
}
