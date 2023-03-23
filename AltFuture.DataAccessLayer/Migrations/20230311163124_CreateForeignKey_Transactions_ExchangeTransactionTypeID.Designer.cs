﻿// <auto-generated />
using AltFuture.DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AltFuture.WebApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230311163124_CreateForeignKey_Transactions_ExchangeTransactionTypeID")]
    partial class CreateForeignKey_Transactions_ExchangeTransactionTypeID
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AltFuture.WebApp.Models.AppUser", b =>
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

            modelBuilder.Entity("AltFuture.WebApp.Models.Crypto", b =>
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
                        });
                });

            modelBuilder.Entity("AltFuture.WebApp.Models.CryptoPrice", b =>
                {
                    b.Property<int>("CryptoPriceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CryptoPriceId"));

                    b.Property<int>("CryptoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateRecorded")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(30,20)");

                    b.HasKey("CryptoPriceId");

                    b.ToTable("CryptoPrices");
                });

            modelBuilder.Entity("AltFuture.WebApp.Models.Exchange", b =>
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
                            ExchangeName = "Crypto.com"
                        },
                        new
                        {
                            ExchangeId = 3,
                            ExchangeName = "Kucoin"
                        },
                        new
                        {
                            ExchangeId = 4,
                            ExchangeName = "Etoro"
                        },
                        new
                        {
                            ExchangeId = 5,
                            ExchangeName = "CDC Defi Wallet"
                        },
                        new
                        {
                            ExchangeId = 6,
                            ExchangeName = "Yoroi Wallet"
                        });
                });

            modelBuilder.Entity("AltFuture.WebApp.Models.ExchangeTransactionType", b =>
                {
                    b.Property<int>("ExchangeTransactionTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExchangeTransactionTypeId"));

                    b.Property<int>("ExchangeId")
                        .HasColumnType("int");

                    b.Property<string>("ExchangeTransactionTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TransactionTypeId")
                        .HasColumnType("int");

                    b.HasKey("ExchangeTransactionTypeId");

                    b.HasIndex("ExchangeId");

                    b.HasIndex("TransactionTypeId");

                    b.ToTable("ExchangeTransactionTypes");

                    b.HasData(
                        new
                        {
                            ExchangeTransactionTypeId = 1,
                            ExchangeId = 1,
                            ExchangeTransactionTypeName = "Buy",
                            TransactionTypeId = 1
                        },
                        new
                        {
                            ExchangeTransactionTypeId = 2,
                            ExchangeId = 2,
                            ExchangeTransactionTypeName = "viban_purchase",
                            TransactionTypeId = 1
                        },
                        new
                        {
                            ExchangeTransactionTypeId = 3,
                            ExchangeId = 3,
                            ExchangeTransactionTypeName = "Buy",
                            TransactionTypeId = 1
                        },
                        new
                        {
                            ExchangeTransactionTypeId = 4,
                            ExchangeId = 4,
                            ExchangeTransactionTypeName = "Open Position",
                            TransactionTypeId = 1
                        },
                        new
                        {
                            ExchangeTransactionTypeId = 5,
                            ExchangeId = 1,
                            ExchangeTransactionTypeName = "Sell",
                            TransactionTypeId = 2
                        },
                        new
                        {
                            ExchangeTransactionTypeId = 6,
                            ExchangeId = 2,
                            ExchangeTransactionTypeName = "Sell",
                            TransactionTypeId = 2
                        },
                        new
                        {
                            ExchangeTransactionTypeId = 7,
                            ExchangeId = 3,
                            ExchangeTransactionTypeName = "Sell",
                            TransactionTypeId = 2
                        },
                        new
                        {
                            ExchangeTransactionTypeId = 8,
                            ExchangeId = 4,
                            ExchangeTransactionTypeName = "Sell",
                            TransactionTypeId = 2
                        });
                });

            modelBuilder.Entity("AltFuture.WebApp.Models.StoredProcs.PortfolioSummaryGetAll", b =>
                {
                    b.Property<decimal>("AverageBuyPrice")
                        .HasColumnType("decimal(10,3)");

                    b.Property<int>("CryptoId")
                        .HasColumnType("int");

                    b.Property<string>("CryptoName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("CryptoPrice")
                        .HasColumnType("decimal(10,3)");

                    b.Property<decimal>("CurrentWorth")
                        .HasColumnType("decimal(10,3)");

                    b.Property<DateTime>("DateRecorded")
                        .HasColumnType("datetime2");

                    b.Property<int>("NumberOfOrders")
                        .HasColumnType("int");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(10,3)");

                    b.Property<int>("RowType")
                        .HasColumnType("int");

                    b.Property<string>("Ticker")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalInvested")
                        .HasColumnType("decimal(10,3)");

                    b.Property<decimal>("UnrealizedProfit")
                        .HasColumnType("decimal(10,3)");

                    b.ToTable("PortfolioSummaryGetAll", null, t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("AltFuture.WebApp.Models.Transaction", b =>
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

                    b.HasData(
                        new
                        {
                            TransactionId = 1,
                            AppUserId = 1,
                            CreatedDate = new DateTime(2023, 3, 11, 11, 31, 23, 476, DateTimeKind.Local).AddTicks(5251),
                            CryptoId = 1,
                            ExchangeTransactionTypeId = 1,
                            Fee = 0.80m,
                            FromExchangeId = 1,
                            Price = 23500.00m,
                            Quantity = 0.004m,
                            TransactionDate = new DateTime(2023, 3, 9, 11, 31, 23, 476, DateTimeKind.Local).AddTicks(5207),
                            TransactionReferenceNum = 1000,
                            TransactionTotal = 97.00m
                        },
                        new
                        {
                            TransactionId = 2,
                            AppUserId = 1,
                            CreatedDate = new DateTime(2023, 3, 11, 11, 31, 23, 476, DateTimeKind.Local).AddTicks(5260),
                            CryptoId = 1,
                            ExchangeTransactionTypeId = 1,
                            Fee = 0.85m,
                            FromExchangeId = 1,
                            Price = 23400.00m,
                            Quantity = 0.005m,
                            TransactionDate = new DateTime(2023, 3, 10, 11, 31, 23, 476, DateTimeKind.Local).AddTicks(5257),
                            TransactionReferenceNum = 1001,
                            TransactionTotal = 117.00m
                        },
                        new
                        {
                            TransactionId = 3,
                            AppUserId = 1,
                            CreatedDate = new DateTime(2023, 3, 11, 11, 31, 23, 476, DateTimeKind.Local).AddTicks(5271),
                            CryptoId = 1,
                            ExchangeTransactionTypeId = 1,
                            Fee = 0.84m,
                            FromExchangeId = 1,
                            Price = 23450.00m,
                            Quantity = 0.005m,
                            TransactionDate = new DateTime(2023, 3, 11, 11, 31, 23, 476, DateTimeKind.Local).AddTicks(5268),
                            TransactionReferenceNum = 1003,
                            TransactionTotal = 117.25m
                        },
                        new
                        {
                            TransactionId = 4,
                            AppUserId = 1,
                            CreatedDate = new DateTime(2023, 3, 11, 11, 31, 23, 476, DateTimeKind.Local).AddTicks(5281),
                            CryptoId = 2,
                            ExchangeTransactionTypeId = 1,
                            Fee = 0.50m,
                            FromExchangeId = 1,
                            Price = 0.35m,
                            Quantity = 100m,
                            TransactionDate = new DateTime(2023, 3, 6, 11, 31, 23, 476, DateTimeKind.Local).AddTicks(5278),
                            TransactionReferenceNum = 2001,
                            TransactionTotal = 35m
                        },
                        new
                        {
                            TransactionId = 5,
                            AppUserId = 1,
                            CreatedDate = new DateTime(2023, 3, 11, 11, 31, 23, 476, DateTimeKind.Local).AddTicks(5289),
                            CryptoId = 2,
                            ExchangeTransactionTypeId = 1,
                            Fee = 0.75m,
                            FromExchangeId = 1,
                            Price = 0.40m,
                            Quantity = 150m,
                            TransactionDate = new DateTime(2023, 3, 9, 11, 31, 23, 476, DateTimeKind.Local).AddTicks(5286),
                            TransactionReferenceNum = 2002,
                            TransactionTotal = 60m
                        });
                });

            modelBuilder.Entity("AltFuture.WebApp.Models.TransactionType", b =>
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
                            TransactionTypeName = "Stakeing Reward"
                        },
                        new
                        {
                            TransactionTypeId = 4,
                            TransactionTypeName = "Loan Interest"
                        },
                        new
                        {
                            TransactionTypeId = 5,
                            TransactionTypeName = "Card Cashback"
                        },
                        new
                        {
                            TransactionTypeId = 6,
                            TransactionTypeName = "Card Cashback Reversalv"
                        },
                        new
                        {
                            TransactionTypeId = 7,
                            TransactionTypeName = "Reimbursement"
                        },
                        new
                        {
                            TransactionTypeId = 8,
                            TransactionTypeName = "Withdrawl"
                        });
                });

            modelBuilder.Entity("AltFuture.WebApp.Models.ExchangeTransactionType", b =>
                {
                    b.HasOne("AltFuture.WebApp.Models.Exchange", "Exchange")
                        .WithMany()
                        .HasForeignKey("ExchangeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AltFuture.WebApp.Models.TransactionType", "TransactionType")
                        .WithMany()
                        .HasForeignKey("TransactionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exchange");

                    b.Navigation("TransactionType");
                });

            modelBuilder.Entity("AltFuture.WebApp.Models.Transaction", b =>
                {
                    b.HasOne("AltFuture.WebApp.Models.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AltFuture.WebApp.Models.Crypto", "Crypto")
                        .WithMany()
                        .HasForeignKey("CryptoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AltFuture.WebApp.Models.ExchangeTransactionType", "ExchangeTransactionType")
                        .WithMany()
                        .HasForeignKey("ExchangeTransactionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AltFuture.WebApp.Models.Exchange", "FromExchange")
                        .WithMany()
                        .HasForeignKey("FromExchangeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AltFuture.WebApp.Models.Exchange", "ToExchange")
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
