using System.ComponentModel.DataAnnotations;

namespace AltFutureWebApp.Models
{
    public class Exchange
    {
        [Key]
        public int ExchangeId { get; set; }
        public string ExchangeName { get; set; }
    }
}
