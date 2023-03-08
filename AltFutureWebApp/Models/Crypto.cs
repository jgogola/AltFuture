using System.ComponentModel.DataAnnotations;

namespace AltFutureWebApp.Models
{
    public class Crypto
    {
        [Key]
        public int CryptoId { get; set; }

        public string CryptoName { get; set; }

        public string TickerSymbol { get; set; }
    }
}
