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
                    TransactionTypeId = (int)TransactionTypeEnum.PerkReward,
                    DataImportTypeId = (int)DataImportTypeEnum.CSV
                },

                //Crypto.com CSV Transaction Types:
                new ExchangeTransactionType
                {
                    ExchangeTransactionTypeId = 6,
                    ExchangeTransactionTypeName = "viban_purchase",
                    ExchangeId = (int)ExchangeEnum.CryptoDotCom,
                    TransactionTypeId = (int)TransactionTypeEnum.Buy,
                    DataImportTypeId = (int)DataImportTypeEnum.CSV
                },
                new ExchangeTransactionType
                {
                    ExchangeTransactionTypeId = 7,
                    ExchangeTransactionTypeName = "reimbursement",
                    ExchangeId = (int)ExchangeEnum.CryptoDotCom,
                    TransactionTypeId = (int)TransactionTypeEnum.PerkReward,
                    DataImportTypeId = (int)DataImportTypeEnum.CSV
                },
                new ExchangeTransactionType
                {
                    ExchangeTransactionTypeId = 8,
                    ExchangeTransactionTypeName = "referral_card_cashback",
                    ExchangeId = (int)ExchangeEnum.CryptoDotCom,
                    TransactionTypeId = (int)TransactionTypeEnum.PerkReward,
                    DataImportTypeId = (int)DataImportTypeEnum.CSV
                },
                new ExchangeTransactionType
                {
                    ExchangeTransactionTypeId = 9,
                    ExchangeTransactionTypeName = "card_cashback_reverted",
                    ExchangeId = (int)ExchangeEnum.CryptoDotCom,
                    TransactionTypeId = (int)TransactionTypeEnum.PerkReward,
                    DataImportTypeId = (int)DataImportTypeEnum.CSV
                },

                //Coinbase Pro CSV Transaction Types:
                new ExchangeTransactionType
                {
                    ExchangeTransactionTypeId = 10,
                    ExchangeTransactionTypeName = "Buy",
                    ExchangeId = (int)ExchangeEnum.CoinbasePro,
                    TransactionTypeId = (int)TransactionTypeEnum.Buy,
                    DataImportTypeId = (int)DataImportTypeEnum.CSV
                },

                //Etoro CSV Transaction Types:
                new ExchangeTransactionType
                {
                    ExchangeTransactionTypeId = 11,
                    ExchangeTransactionTypeName = "Open Position",
                    ExchangeId = (int)ExchangeEnum.Etoro,
                    TransactionTypeId = (int)TransactionTypeEnum.Buy,
                    DataImportTypeId = (int)DataImportTypeEnum.CSV
                },
                new ExchangeTransactionType
                {
                    ExchangeTransactionTypeId = 12,
                    ExchangeTransactionTypeName = "Staking",
                    ExchangeId = (int)ExchangeEnum.Etoro,
                    TransactionTypeId = (int)TransactionTypeEnum.StakingReward,
                    DataImportTypeId = (int)DataImportTypeEnum.CSV
                },

                //Binance CSV Transaction Types:
                new ExchangeTransactionType
                {
                    ExchangeTransactionTypeId = 13,
                    ExchangeTransactionTypeName = "Buy",
                    ExchangeId = (int)ExchangeEnum.Binance,
                    TransactionTypeId = (int)TransactionTypeEnum.Buy,
                    DataImportTypeId = (int)DataImportTypeEnum.CSV
                }


            );
        }
    }
}
