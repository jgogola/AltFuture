using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AltFuture.Models
{
    public class Crypto
    {
        [Key]
        public int CryptoId { get; set; }

        public string CryptoName { get; set; }

        public string TickerSymbol { get; set; }

        //[DefaultValue(0.00)]
        //public int CmcId { get; set; } = 0;

        //[DefaultValue(0.00)]
        //public int CmcRank { get; set; } = 0;
    }
}
