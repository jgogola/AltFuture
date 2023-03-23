using AltFuture.DataAccessLayer.Data.Enums;
using AltFuture.DataAccessLayer.Models;
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
                //Coinbase CSV Transaction Types:
                new ExchangeTransactionType
                {
                    ExchangeTransactionTypeId = 1,
                    ExchangeTransactionTypeName = "Buy",
                    ExchangeId = (int)ExchangeEnum.Coinbase,
                    TransactionTypeId = (int)TransactionTypeEnum.Buy,
                    DataImportTypeId = (int)DataImportTypeEnum.CSV
                },
                new ExchangeTransactionType
                {
                    ExchangeTransactionTypeId = 2,
                    ExchangeTransactionTypeName = "Advanced Trade Buy",
                    ExchangeId = (int)ExchangeEnum.Coinbase,
                    TransactionTypeId = (int)TransactionTypeEnum.Buy,
                    DataImportTypeId = (int)DataImportTypeEnum.CSV
                },
                new ExchangeTransactionType
                {
                    ExchangeTransactionTypeId = 3,
                    ExchangeTransactionTypeName = "Sell",
                    ExchangeId = (int)ExchangeEnum.Coinbase,
                    TransactionTypeId = (int)TransactionTypeEnum.Sell,
                    DataImportTypeId = (int)DataImportTypeEnum.CSV
                },
                new ExchangeTransactionType
                {
                    ExchangeTransactionTypeId = 4,
                    ExchangeTransactionTypeName = "Rewards Income",
                    ExchangeId = (int)ExchangeEnum.Coinbase,
                    TransactionTypeId = (int)TransactionTypeEnum.StakingReward,
                    DataImportTypeId = (int)DataImportTypeEnum.CSV
                },
                new ExchangeTransactionType
                {
                    ExchangeTransactionTypeId = 5,
                    ExchangeTransactionTypeName = "Learning Reward",
                    ExchangeId = (int)ExchangeEnum.Coinbase,
                    TransactionTypeId = (int)TransactionTypeEnum.StakingReward,
                    DataImportTypeId = (int)DataImportTypeEnum.CSV
                }

             
            );
        }
    }
}
