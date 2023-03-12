using System.ComponentModel.DataAnnotations.Schema;

namespace AltFuture.Models
{
    public class ExchangeTransactionType
    {
        public int ExchangeTransactionTypeId { get; set; }
        public string ExchangeTransactionTypeName { get; set; }

        [ForeignKey(nameof(Exchange))]
        public int ExchangeId { get; set; }

        public Exchange Exchange { get; set; }

        [ForeignKey(nameof(TransactionType))]
        public int TransactionTypeId { get; set; }

        public TransactionType TransactionType { get; set; }
    }
}
