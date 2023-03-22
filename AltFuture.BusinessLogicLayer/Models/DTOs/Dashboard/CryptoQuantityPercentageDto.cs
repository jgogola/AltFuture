using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltFuture.BusinessLogicLayer.Models.DTOs.Dashboard
{
    public class CryptoQuantityPercentageDto
    {
        public int CryptoId { get; set; }
        public string TickerSymbol { get; set; }
        public decimal QuantityPercentage { get; set; }
    }
}
