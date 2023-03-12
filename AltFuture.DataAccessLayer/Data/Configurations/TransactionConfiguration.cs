using AltFuture.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AltFuture.DataAccessLayer.Data.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasData
            (
                new Transaction
                {
                    TransactionId = 1,
                    TransactionReferenceNum = 1000,
                    AppUserId = 1,
                    CryptoId = 1,
                    ExchangeTransactionTypeId = 1,
                    Price = 23500.00m,
                    Quantity = 0.004m,
                    TransactionTotal = 97.00m,
                    Fee = 0.80m,
                    TransactionDate = new DateTime(2023, 3, 9, 11, 52, 29, 147, DateTimeKind.Local).AddTicks(809),
                    FromExchangeId = 1,
                    ToExchangeId = null,
                    CreatedDate = new DateTime(2023, 3, 11, 11, 52, 29, 147, DateTimeKind.Local).AddTicks(854)
                },
                new Transaction
                {
                    TransactionId = 2,
                    TransactionReferenceNum = 1001,
                    AppUserId = 1,
                    CryptoId = 1,
                    ExchangeTransactionTypeId = 1,
                    Price = 23400.00m,
                    Quantity = 0.005m,
                    TransactionTotal = 117.00m,
                    Fee = 0.85m,
                    TransactionDate = new DateTime(2023, 3, 10, 11, 52, 29, 147, DateTimeKind.Local).AddTicks(860),
                    FromExchangeId = 1,
                    ToExchangeId = null,
                    CreatedDate = new DateTime(2023, 3, 11, 11, 52, 29, 147, DateTimeKind.Local).AddTicks(863)
                },
                new Transaction
                {
                    TransactionId = 3,
                    TransactionReferenceNum = 1003,
                    AppUserId = 1,
                    CryptoId = 1,
                    ExchangeTransactionTypeId = 1,
                    Price = 23450.00m,
                    Quantity = 0.005m,
                    TransactionTotal = 117.25m,
                    Fee = 0.84m,
                    TransactionDate = new DateTime(2023, 3, 11, 11, 52, 29, 147, DateTimeKind.Local).AddTicks(867),
                    FromExchangeId = 1,
                    ToExchangeId = null,
                    CreatedDate = new DateTime(2023, 3, 11, 11, 52, 29, 147, DateTimeKind.Local).AddTicks(870)
                },
                new Transaction
                {
                    TransactionId = 4,
                    TransactionReferenceNum = 2001,
                    AppUserId = 1,
                    CryptoId = 2,
                    ExchangeTransactionTypeId = 1,
                    Price = 0.35m,
                    Quantity = 100m,
                    TransactionTotal = 35m,
                    Fee = 0.50m,
                    TransactionDate = new DateTime(2023, 3, 6, 11, 52, 29, 147, DateTimeKind.Local).AddTicks(876),
                    FromExchangeId = 1,
                    ToExchangeId = null,
                    CreatedDate = new DateTime(2023, 3, 11, 11, 52, 29, 147, DateTimeKind.Local).AddTicks(878)
                },
                new Transaction
                {
                    TransactionId = 5,
                    TransactionReferenceNum = 2002,
                    AppUserId = 1,
                    CryptoId = 2,
                    ExchangeTransactionTypeId = 1,
                    Price = 0.40m,
                    Quantity = 150m,
                    TransactionTotal = 60m,
                    Fee = 0.75m,
                    TransactionDate = new DateTime(2023, 3, 9, 11, 52, 29, 147, DateTimeKind.Local).AddTicks(882),
                    FromExchangeId = 1,
                    ToExchangeId = null,
                    CreatedDate = new DateTime(2023, 3, 11, 11, 52, 29, 147, DateTimeKind.Local).AddTicks(885)
                }
            );
        }
    }
}
