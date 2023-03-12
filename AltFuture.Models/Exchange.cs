using System.ComponentModel.DataAnnotations;

namespace AltFuture.Models
{
    public class Exchange
    {
        [Key]
        public int ExchangeId { get; set; }
        public string ExchangeName { get; set; }
    }
}
