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
        [Display(Name = "Crypto.com")]
        CryptoDotCom = 3,
        Etoro = 4,
        Binance = 5
    }
}
