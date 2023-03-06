using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AltFutureWebApp.Areas.Portfolios.Models
{
    public class CryptoPrice
    {
        [Key]
        public int CryptoPriceId { get; set; }

        public DateTime DateCreated { get; set; }

        [ForeignKey(nameof(Crypto))]
        public int CryptoId { get; set; }

        [Column(TypeName = "decimal(30,20)")]
        [DefaultValue(0.00)]
        public decimal Price { get; set; } = decimal.Zero;
    }
}
