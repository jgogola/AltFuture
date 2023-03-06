using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AltFutureWebApp.Areas.Portfolios.Data.Enums
{
    public enum CommonTransactionType
    {
        None = 0,
        Buy = 1,
        Sell = 2,
        [Display(Name = "Stakeing Reward")]
        StakeingReward = 3,
        [Display(Name = "Loan Interest")]
        LoanInterest = 4,
        [Display(Name = "Card Cashback")]
        CardCashback = 5,
        [Display(Name = "Card Cashback Reversal")]
        CardCashbackReversal = 6,
        Reimbursement = 7,
        Withdrawl = 8
    }
}
