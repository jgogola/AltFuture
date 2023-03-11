using AltFutureWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AltFutureWebApp.Data.Configurations
{
    public class TransactionTypeConfiguration : IEntityTypeConfiguration<TransactionType>
    {
        public void Configure(EntityTypeBuilder<TransactionType> builder)
        {
            builder.HasData
            (
                new TransactionType
                {
                    TransactionTypeId = 1,
                    TransactionTypeName = "Buy"
                },
                new TransactionType
                {
                    TransactionTypeId = 2,
                    TransactionTypeName = "Sell"
                },
                new TransactionType
                {
                    TransactionTypeId = 3,
                    TransactionTypeName = "Stakeing Reward"
                },
                new TransactionType
                {
                    TransactionTypeId = 4,
                    TransactionTypeName = "Loan Interest"
                },
                new TransactionType
                {
                    TransactionTypeId = 5,
                    TransactionTypeName = "Card Cashback"
                },
                new TransactionType
                {
                    TransactionTypeId = 6,
                    TransactionTypeName = "Card Cashback Reversalv"
                },
                new TransactionType
                {
                    TransactionTypeId = 7,
                    TransactionTypeName = "Reimbursement"
                },
                new TransactionType
                {
                    TransactionTypeId = 8,
                    TransactionTypeName = "Withdrawl"
                }
            );
        }
    }
}
