// <auto-generated />
using AltFuture.DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AltFutureWebApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230307131545_InitalCreate")]
    partial class InitalCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AltFutureWebApp.Areas.Portfolios.Models.AppUser", b =>
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

            modelBuilder.Entity("AltFutureWebApp.Areas.Portfolios.Models.Crypto", b =>
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
                });

            modelBuilder.Entity("AltFutureWebApp.Areas.Portfolios.Models.CryptoPrice", b =>
                {
                    b.Property<int>("CryptoPriceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CryptoPriceId"));

                    b.Property<int>("CryptoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(30,20)");

                    b.HasKey("CryptoPriceId");

                    b.ToTable("CryptoPrices");
                });

            modelBuilder.Entity("AltFutureWebApp.Areas.Portfolios.Models.Exchange", b =>
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
                });

            modelBuilder.Entity("AltFutureWebApp.Areas.Portfolios.Models.ExchangeTransactionType", b =>
                {
                    b.Property<int>("ExchangeTransactionTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExchangeTransactionTypeId"));

                    b.Property<int>("CommonTransactionType")
                        .HasColumnType("int");

                    b.Property<int>("ExchageId")
                        .HasColumnType("int");

                    b.Property<string>("ExchangeTransactionTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExchangeTransactionTypeId");

                    b.HasIndex("ExchageId");

                    b.ToTable("ExchangeTransactionTypes");
                });

            modelBuilder.Entity("AltFutureWebApp.Areas.Portfolios.Models.Transaction", b =>
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

                    b.HasIndex("FromExchangeId");

                    b.HasIndex("ToExchangeId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("AltFutureWebApp.Areas.Portfolios.Models.ExchangeTransactionType", b =>
                {
                    b.HasOne("AltFutureWebApp.Areas.Portfolios.Models.Exchange", "Exchage")
                        .WithMany()
                        .HasForeignKey("ExchageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exchage");
                });

            modelBuilder.Entity("AltFutureWebApp.Areas.Portfolios.Models.Transaction", b =>
                {
                    b.HasOne("AltFutureWebApp.Areas.Portfolios.Models.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AltFutureWebApp.Areas.Portfolios.Models.Crypto", "Crypto")
                        .WithMany()
                        .HasForeignKey("CryptoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AltFutureWebApp.Areas.Portfolios.Models.Exchange", "FromExchange")
                        .WithMany()
                        .HasForeignKey("FromExchangeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AltFutureWebApp.Areas.Portfolios.Models.Exchange", "ToExchange")
                        .WithMany()
                        .HasForeignKey("ToExchangeId");

                    b.Navigation("AppUser");

                    b.Navigation("Crypto");

                    b.Navigation("FromExchange");

                    b.Navigation("ToExchange");
                });
#pragma warning restore 612, 618
        }
    }
}
