using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AltFutureWebApp.Areas.Admin.ViewModels
{
    public class ExchangeViewModel
    {
        public int ExchangeId { get; set; }

        [Display(Name = "Exhange/Wallet Name", Description = "Name of the Exchange or Wallet your assets exist on.")]
        public string ExchangeName { get; set; }
    }
}
