using AltFuture.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AltFuture.DataAccessLayer.Data.Configurations
{
    public class ExchangeTransactionTypeConfiguration : IEntityTypeConfiguration<ExchangeTransactionType>
    {
        public void Configure(EntityTypeBuilder<ExchangeTransactionType> builder)
        {
            builder.HasData
            (
                //BUY
                new ExchangeTransactionType
                {
                    ExchangeTransactionTypeId = 1,
                    ExchangeTransactionTypeName = "Buy",
                    ExchangeId = 1, //Coinbase
                    TransactionTypeId = 1
                },
                new ExchangeTransactionType
                {
                    ExchangeTransactionTypeId = 2,
                    ExchangeTransactionTypeName = "viban_purchase",
                    ExchangeId = 2, //Crypto.com
                    TransactionTypeId = 1
                },
                new ExchangeTransactionType
                {
                    ExchangeTransactionTypeId = 3,
                    ExchangeTransactionTypeName = "Buy",
                    ExchangeId = 3, //Kucoin
                    TransactionTypeId = 1
                },
                new ExchangeTransactionType
                {
                    ExchangeTransactionTypeId = 4,
                    ExchangeTransactionTypeName = "Open Position",
                    ExchangeId = 4, //Etoro
                    TransactionTypeId = 1
                },
                //SELL
                new ExchangeTransactionType
                {
                    ExchangeTransactionTypeId = 5,
                    ExchangeTransactionTypeName = "Sell",
                    ExchangeId = 1, //Coinbase
                    TransactionTypeId = 2
                },
                new ExchangeTransactionType
                {
                    ExchangeTransactionTypeId = 6,
                    ExchangeTransactionTypeName = "Sell",
                    ExchangeId = 2, //Crypto.com
                    TransactionTypeId = 2
                },
                new ExchangeTransactionType
                {
                    ExchangeTransactionTypeId = 7,
                    ExchangeTransactionTypeName = "Sell",
                    ExchangeId = 3, //Kucoin
                    TransactionTypeId = 2
                },
                new ExchangeTransactionType
                {
                    ExchangeTransactionTypeId = 8,
                    ExchangeTransactionTypeName = "Sell",
                    ExchangeId = 4, //Etoro
                    TransactionTypeId = 2
                }
            );
        }
    }
}
