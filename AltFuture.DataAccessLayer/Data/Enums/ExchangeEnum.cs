using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltFuture.DataAccessLayer.Data.Enums
{
    public enum ExchangeEnum
    {
        None = 0,
        Coinbase = 1,
        [Display(Name = "Coinbase Pro")]
        CoinbasePro = 2,
    }
}
