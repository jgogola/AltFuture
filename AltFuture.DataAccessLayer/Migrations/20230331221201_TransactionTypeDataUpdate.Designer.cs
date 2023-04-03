﻿// <auto-generated />
using System;
using AltFuture.DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AltFuture.WebApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230331221201_TransactionTypeDataUpdate")]
    partial class TransactionTypeDataUpdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AltFuture.DataAccessLayer.Models.AppUser", b =>
                {
                    b.Property<int>("AppUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppUserId"));

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AppUserId");

                    b.ToTable("AppUsers");
                });

            modelBuilder.Entity("AltFuture.DataAccessLayer.Models.Crypto", b =>
                {
                    b.Property<int>("CryptoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CryptoId"));

                    b.Property<string>("CryptoName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TickerSymbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CryptoId");

                    b.ToTable("Cryptos");

                    b.HasData(
                        new
                        {
                            CryptoId = 1,
                            CryptoName = "Bitcoin",
                            TickerSymbol = "BTC"
                        },
                        new
                        {
                            CryptoId = 2,
                            CryptoName = "Cardano",
                            TickerSymbol = "ADA"
                        },
                        new
                        {
                            CryptoId = 3,
                            CryptoName = "Ethereum",
                            TickerSymbol = "ETH"
                        },
                        new
                        {
                            CryptoId = 4,
                            CryptoName = "Bianance",
                            TickerSymbol = "BNB"
                        },
                        new
                        {
                            CryptoId = 5,
                            CryptoName = "Crypto.com",
                            TickerSymbol = "CRO"
                        },
                        new
                        {
                            CryptoId = 6,
                            CryptoName = "Shiba Inu",
                            TickerSymbol = "SHIB"
                        },
                        new
                        {
                            CryptoId = 7,
                            CryptoName = "Polygon",
                            TickerSymbol = "MATIC"
                        },
                        new
                        {
                            CryptoId = 8,
                            CryptoName = "Axie-Infinity",
                            TickerSymbol = "AXS"
                        },
                        new
                        {
                            CryptoId = 9,
                            CryptoName = "Decentraland",
                            TickerSymbol = "MANA"
                        },
                        new
                        {
                            CryptoId = 10,
                            CryptoName = "Arweave",
                            TickerSymbol = "AR"
                        },
                        new
                        {
                            CryptoId = 11,
                            CryptoName = "Avalanche",
                            TickerSymbol = "AVAX"
                        },
                        new
                        {
                            CryptoId = 12,
                            CryptoName = "Polkadot",
                            TickerSymbol = "DOT"
                        },
                        new
                        {
                            CryptoId = 13,
                            CryptoName = "USD Coin",
                            TickerSymbol = "USDC"
                        });
                });

            modelBuilder.Entity("AltFuture.DataAccessLayer.Models.CryptoPrice", b =>
                {
                    b.Property<int>("CryptoPriceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CryptoPriceId"));

                    b.Property<int>("CryptoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateRecorded")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("MarketCap")
                        .HasColumnType("decimal(25,10)");

                    b.Property<decimal>("MarketCapDominance")
                        .HasColumnType("decimal(25,10)");

                    b.Property<decimal>("PercentChange1h")
                        .HasColumnType("decimal(20,10)");

                    b.Property<decimal>("PercentChange24h")
                        .HasColumnType("decimal(20,10)");

                    b.Property<decimal>("PercentChange30d")
                        .HasColumnType("decimal(20,10)");

                    b.Property<decimal>("PercentChange60d")
                        .HasColumnType("decimal(20,10)");

                    b.Property<decimal>("PercentChange7d")
                        .HasColumnType("decimal(20,10)");

                    b.Property<decimal>("PercentChange90d")
                        .HasColumnType("decimal(20,10)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(30,20)");

                    b.Property<decimal>("Volume24h")
                        .HasColumnType("decimal(25,10)");

                    b.Property<decimal>("VolumeChange24h")
                        .HasColumnType("decimal(20,10)");

                    b.HasKey("CryptoPriceId");

                    b.HasIndex("CryptoId");

                    b.ToTable("CryptoPrices");
                });

            modelBuilder.Entity("AltFuture.DataAccessLayer.Models.DataImportType", b =>
                {
                    b.Property<int>("DataImportTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DataImportTypeId"));

                    b.Property<string>("DataImportTypeName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("DataImportTypeId");

                    b.ToTable("DataImportTypes");

                    b.HasData(
                        new
                        {
                            DataImportTypeId = 1,
                            DataImportTypeName = "API"
                        },
                        new
                        {
                            DataImportTypeId = 2,
                            DataImportTypeName = "CSV"
                        });
                });

            modelBuilder.Entity("AltFuture.DataAccessLayer.Models.Exchange", b =>
                {
                    b.Property<int>("ExchangeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExchangeId"));

                    b.Property<string>("ExchangeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExchangeId");

                    b.ToTable("Exchanges");

                    b.HasData(
                        new
                        {
                            ExchangeId = 1,
                            ExchangeName = "Coinbase"
                        },
                        new
                        {
                            ExchangeId = 2,
                            ExchangeName = "Coinbase Pro"
                        },
                        new
                        {
                            ExchangeId = 3,
                            ExchangeName = "Crypto.com"
                        });
                });

            modelBuilder.Entity("AltFuture.DataAccessLayer.Models.ExchangeTransactionType", b =>
                {
                    b.Property<int>("ExchangeTransactionTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExchangeTransactionTypeId"));

                    b.Property<int>("DataImportTypeId")
                        .HasColumnType("int");

                    b.Property<int>("ExchangeId")
                        .HasColumnType("int");

                    b.Property<string>("ExchangeTransactionTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TransactionTypeId")
                        .HasColumnType("int");

                    b.HasKey("ExchangeTransactionTypeId");

                    b.HasIndex("DataImportTypeId");

                    b.HasIndex("ExchangeId");

                    b.HasIndex("TransactionTypeId");

                    b.ToTable("ExchangeTransactionTypes");

                    b.HasData(
                        new
                        {
                            ExchangeTransactionTypeId = 1,
                            DataImportTypeId = 2,
                            ExchangeId = 1,
                            ExchangeTransactionTypeName = "Buy",
                            TransactionTypeId = 1
                        },
                        new
                        {
                            ExchangeTransactionTypeId = 2,
                            DataImportTypeId = 2,
                            ExchangeId = 1,
                            ExchangeTransactionTypeName = "Advanced Trade Buy",
                            TransactionTypeId = 1
                        },
                        new
                        {
                            ExchangeTransactionTypeId = 3,
                            DataImportTypeId = 2,
                            ExchangeId = 1,
                            ExchangeTransactionTypeName = "Sell",
                            TransactionTypeId = 2
                        },
                        new
                        {
                            ExchangeTransactionTypeId = 4,
                            DataImportTypeId = 2,
                            ExchangeId = 1,
                            ExchangeTransactionTypeName = "Rewards Income",
                            TransactionTypeId = 4
                        },
                        new
                        {
                            ExchangeTransactionTypeId = 5,
                            DataImportTypeId = 2,
                            ExchangeId = 1,
                            ExchangeTransactionTypeName = "Learning Reward",
                            TransactionTypeId = 6
                        },
                        new
                        {
                            ExchangeTransactionTypeId = 6,
                            DataImportTypeId = 2,
                            ExchangeId = 3,
                            ExchangeTransactionTypeName = "viban_purchase",
                            TransactionTypeId = 1
                        },
                        new
                        {
                            ExchangeTransactionTypeId = 7,
                            DataImportTypeId = 2,
                            ExchangeId = 3,
                            ExchangeTransactionTypeName = "reimbursement",
                            TransactionTypeId = 6
                        },
                        new
                        {
                            ExchangeTransactionTypeId = 8,
                            DataImportTypeId = 2,
                            ExchangeId = 3,
                            ExchangeTransactionTypeName = "referral_card_cashback",
                            TransactionTypeId = 6
                        },
                        new
                        {
                            ExchangeTransactionTypeId = 9,
                            DataImportTypeId = 2,
                            ExchangeId = 3,
                            ExchangeTransactionTypeName = "card_cashback_reverted",
                            TransactionTypeId = 6
                        });
                });

            modelBuilder.Entity("AltFuture.DataAccessLayer.Models.StoredProcs.PortfolioSummaryGetAll", b =>
                {
                    b.Property<decimal>("AverageBuyPrice")
                        .HasColumnType("decimal(10,3)");

                    b.Property<int>("CryptoId")
                        .HasColumnType("int");

                    b.Property<string>("CryptoName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("CurrentWorth")
                        .HasColumnType("decimal(10,3)");

                    b.Property<DateTime?>("DateRecorded")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("FeeTotal")
                        .HasColumnType("decimal(10,3)");

                    b.Property<decimal>("InvestmentTotal")
                        .HasColumnType("decimal(10,3)");

                    b.Property<int>("NumberOfOrders")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10,3)");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(10,3)");

                    b.Property<int>("RowType")
                        .HasColumnType("int");

                    b.Property<string>("TickerSymbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("UnrealizedProfit")
                        .HasColumnType("decimal(10,3)");

                    b.ToTable("PortfolioSummaryGetAll", null, t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("AltFuture.DataAccessLayer.Models.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionId"));

                    b.Property<int>("AppUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CryptoId")
                        .HasColumnType("int");

                    b.Property<int>("ExchangeTransactionTypeId")
                        .HasColumnType("int");

                    b.Property<decimal>("Fee")
                        .HasColumnType("decimal(18,10)");

                    b.Property<int>("FromExchangeId")
                        .HasColumnType("int");

                    b.Property<decimal>("InvestmentTotal")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("decimal(18,10)")
                        .HasComputedColumnSql("Price * Quantity");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,10)");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,10)");

                    b.Property<int?>("ToExchangeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TransactionReferenceNum")
                        .HasColumnType("int");

                    b.Property<decimal>("TransactionTotal")
                        .HasColumnType("decimal(18,10)");

                    b.HasKey("TransactionId");

                    b.HasIndex("AppUserId");

                    b.HasIndex("CryptoId");

                    b.HasIndex("ExchangeTransactionTypeId");

                    b.HasIndex("FromExchangeId");

                    b.HasIndex("ToExchangeId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("AltFuture.DataAccessLayer.Models.TransactionType", b =>
                {
                    b.Property<int>("TransactionTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionTypeId"));

                    b.Property<string>("TransactionTypeName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("TransactionTypeId");

                    b.ToTable("TransactionTypes");

                    b.HasData(
                        new
                        {
                            TransactionTypeId = 1,
                            TransactionTypeName = "Buy"
                        },
                        new
                        {
                            TransactionTypeId = 2,
                            TransactionTypeName = "Sell"
                        },
                        new
                        {
                            TransactionTypeId = 3,
                            TransactionTypeName = "Withdrawl"
                        },
                        new
                        {
                            TransactionTypeId = 4,
                            TransactionTypeName = "Staking Reward"
                        },
                        new
                        {
                            TransactionTypeId = 5,
                            TransactionTypeName = "Loan Interest"
                        },
                        new
                        {
                            TransactionTypeId = 6,
                            TransactionTypeName = "Perk Reward"
                        },
                        new
                        {
                            TransactionTypeId = 7,
                            TransactionTypeName = "Deposit"
                        });
                });

            modelBuilder.Entity("AltFuture.DataAccessLayer.Models.CryptoPrice", b =>
                {
                    b.HasOne("AltFuture.DataAccessLayer.Models.Crypto", "Crypto")
                        .WithMany()
                        .HasForeignKey("CryptoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Crypto");
                });

            modelBuilder.Entity("AltFuture.DataAccessLayer.Models.ExchangeTransactionType", b =>
                {
                    b.HasOne("AltFuture.DataAccessLayer.Models.DataImportType", "DataImportType")
                        .WithMany()
                        .HasForeignKey("DataImportTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AltFuture.DataAccessLayer.Models.Exchange", "Exchange")
                        .WithMany()
                        .HasForeignKey("ExchangeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AltFuture.DataAccessLayer.Models.TransactionType", "TransactionType")
                        .WithMany()
                        .HasForeignKey("TransactionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DataImportType");

                    b.Navigation("Exchange");

                    b.Navigation("TransactionType");
                });

            modelBuilder.Entity("AltFuture.DataAccessLayer.Models.Transaction", b =>
                {
                    b.HasOne("AltFuture.DataAccessLayer.Models.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AltFuture.DataAccessLayer.Models.Crypto", "Crypto")
                        .WithMany()
                        .HasForeignKey("CryptoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AltFuture.DataAccessLayer.Models.ExchangeTransactionType", "ExchangeTransactionType")
                        .WithMany()
                        .HasForeignKey("ExchangeTransactionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AltFuture.DataAccessLayer.Models.Exchange", "FromExchange")
                        .WithMany()
                        .HasForeignKey("FromExchangeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AltFuture.DataAccessLayer.Models.Exchange", "ToExchange")
                        .WithMany()
                        .HasForeignKey("ToExchangeId");

                    b.Navigation("AppUser");

                    b.Navigation("Crypto");

                    b.Navigation("ExchangeTransactionType");

                    b.Navigation("FromExchange");

                    b.Navigation("ToExchange");
                });
#pragma warning restore 612, 618
        }
    }
}
