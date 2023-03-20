using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AltFuture.Models
{
    public class CryptoPrice
    {
        [Key]
        public int CryptoPriceId { get; set; }

        public DateTime DateRecorded { get; set; }

        [ForeignKey(nameof(Crypto))]
        public int CryptoId { get; set; }

        public Crypto Crypto { get; set; }

        [Column(TypeName = "decimal(30,20)")]
        [DefaultValue(0.00)]
        public decimal Price { get; set; } = decimal.Zero;

        [Column(TypeName = "decimal(25,10)")]
        [DefaultValue(0.00)]
        public decimal Volume24h { get; set; } = decimal.Zero;

        [Column(TypeName = "decimal(20,10)")]
        [DefaultValue(0.00)]
        public decimal VolumeChange24h { get; set; } = decimal.Zero;

        [Column(TypeName = "decimal(20,10)")]
        [DefaultValue(0.00)]
        public decimal PercentChange1h { get; set; } = decimal.Zero;

        [Column(TypeName = "decimal(20,10)")]
        [DefaultValue(0.00)]
        public decimal PercentChange24h { get; set; } = decimal.Zero;

        [Column(TypeName = "decimal(20,10)")]
        [DefaultValue(0.00)]
        public decimal PercentChange7d { get; set; } = decimal.Zero;

        [Column(TypeName = "decimal(20,10)")]
        [DefaultValue(0.00)]
        public decimal PercentChange30d { get; set; } = decimal.Zero;

        [Column(TypeName = "decimal(20,10)")]
        [DefaultValue(0.00)]
        public decimal PercentChange60d { get; set; } = decimal.Zero;

        [Column(TypeName = "decimal(20,10)")]
        [DefaultValue(0.00)]
        public decimal PercentChange90d { get; set; } = decimal.Zero;

        [Column(TypeName = "decimal(25,10)")]
        [DefaultValue(0.00)]
        public decimal MarketCap { get; set; } = decimal.Zero;

        [Column(TypeName = "decimal(25,10)")]
        [DefaultValue(0.00)]
        public decimal MarketCapDominance { get; set; } = decimal.Zero;



    }
}
