using System.ComponentModel.DataAnnotations;

namespace AltFuture.DataAccessLayer.Data.Enums
{
    //* Note: Adding a value requires a Database Migration.
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
        [Display(Name = "Perk Reward")]
        PerkReward = 6,
        Deposit = 7
    }
}
