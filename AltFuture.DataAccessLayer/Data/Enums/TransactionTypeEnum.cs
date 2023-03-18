using System.ComponentModel.DataAnnotations;

namespace AltFuture.DataAccessLayer.Data.Enums
{
    public enum TransactionTypeEnum
    {
        None = 0,
        Buy = 1,
        Sell = 2,
        Withdrawl = 3,
        [Display(Name = "Staking Reward")]
        StakingReward = 4,
        [Display(Name = "Loan Interest")]
        LoanInterest = 5,
    }
}
