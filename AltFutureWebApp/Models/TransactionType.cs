using System.ComponentModel.DataAnnotations;

namespace AltFutureWebApp.Models
{
    public class TransactionType
    {
        [Key]
        public int TransactionTypeId { get; set; }

        [StringLength(100)]
        public string TransactionTypeName { get; set;}
    }
}
