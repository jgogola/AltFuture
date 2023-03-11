using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AltFutureWebApp.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public int TransactionReferenceNum { get; set; } = 0;


        [ForeignKey(nameof(AppUser))]
        public int AppUserId { get; set; }

        public AppUser AppUser { get; set; }


        [ForeignKey(nameof(Crypto))]
        public int CryptoId { get; set; }

        public Crypto Crypto { get; set; }


        [ForeignKey(nameof(ExchangeTransactionType))]
        public int ExchangeTransactionTypeId { get; set; }

        public ExchangeTransactionType ExchangeTransactionType { get; set; }


        [Column(TypeName = "decimal(18,10)")]
        [DefaultValue(0.00)]
        public decimal Price { get; set; } = decimal.Zero;


        [Column(TypeName = "decimal(18,10)")]
        public decimal Quantity { get; set; } = decimal.Zero;


        [Column(TypeName = "decimal(18,10)")]
        public decimal TransactionTotal { get; set; } = decimal.Zero;


        [Column(TypeName = "decimal(18,10)")]
        public decimal Fee { get; set; } = decimal.Zero;

        public DateTime TransactionDate { get; set; }


        [ForeignKey(nameof(Exchange))]
        public int FromExchangeId { get; set; }

        public Exchange FromExchange { get; set; }


        [ForeignKey(nameof(Exchange))]
        public int? ToExchangeId { get; set; }

        public Exchange ToExchange { get; set; }

        public DateTime CreatedDate { get; set; }



    }
}
