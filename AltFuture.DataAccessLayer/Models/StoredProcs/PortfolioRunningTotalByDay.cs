using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace AltFuture.DataAccessLayer.Models.StoredProcs
{
    public class PortfolioRunningTotalByDay
    {

        public string RunningTotalInterval { get; set; }


        [DisplayName("Investment")]
        [Column(TypeName = "decimal(10,3)")]
        public decimal InvestmentRunningTotal { get; set; }


        [DisplayName("Current Worth")]
        [Column(TypeName = "decimal(10,3)")]
        public decimal CurrentWorthRunningTotal { get; set; }

    }
}
