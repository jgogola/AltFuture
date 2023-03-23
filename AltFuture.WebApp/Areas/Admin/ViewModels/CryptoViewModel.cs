using System.ComponentModel;

namespace AltFuture.WebApp.Areas.Admin.ViewModels
{
    public class CryptoViewModel
    {
        public int CryptoId { get; set; }

        [DisplayName("Crypto")]
        public string CryptoName { get; set; }

        [DisplayName("Ticker")]
        public string TickerSymbol { get; set; }
    }
}
