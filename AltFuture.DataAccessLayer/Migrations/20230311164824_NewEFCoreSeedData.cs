using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AltFuture.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class NewEFCoreSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cryptos",
                columns: new[] { "CryptoId", "CryptoName", "TickerSymbol" },
                values: new object[,]
                {
                    { 1, "Bitcoin", "BTC" },
                    { 2, "Cardano", "ADA" },
                    { 3, "Ethereum", "ETH" },
                    { 4, "Bianance", "BNB" },
                    { 5, "Crypto.com", "CRO" },
                    { 6, "Shiba Inu", "SHIB" },
                    { 7, "Polygon", "MATIC" },
                    { 8, "Axie-Infinity", "AXS" },
                    { 9, "Decentraland", "MANA" },
                    { 10, "Arweave", "AR" },
                    { 11, "Avalanche", "AVAX" },
                    { 12, "Polkadot", "DOT" }
                });

            migrationBuilder.InsertData(
                table: "Exchanges",
                columns: new[] { "ExchangeId", "ExchangeName" },
                values: new object[,]
                {
                    { 1, "Coinbase" },
                    { 2, "Crypto.com" },
                    { 3, "Kucoin" },
                    { 4, "Etoro" },
                    { 5, "CDC Defi Wallet" },
                    { 6, "Yoroi Wallet" }
                });

            migrationBuilder.InsertData(
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeId", "TransactionTypeName" },
                values: new object[,]
                {
                    { 1, "Buy" },
                    { 2, "Sell" },
                    { 3, "Stakeing Reward" },
                    { 4, "Loan Interest" },
                    { 5, "Card Cashback" },
                    { 6, "Card Cashback Reversalv" },
                    { 7, "Reimbursement" },
                    { 8, "Withdrawl" }
                });

            migrationBuilder.InsertData(
                table: "ExchangeTransactionTypes",
                columns: new[] { "ExchangeTransactionTypeId", "ExchangeId", "ExchangeTransactionTypeName", "TransactionTypeId" },
                values: new object[,]
                {
                    { 1, 1, "Buy", 1 },
                    { 2, 2, "viban_purchase", 1 },
                    { 3, 3, "Buy", 1 },
                    { 4, 4, "Open Position", 1 },
                    { 5, 1, "Sell", 2 },
                    { 6, 2, "Sell", 2 },
                    { 7, 3, "Sell", 2 },
                    { 8, 4, "Sell", 2 }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionId", "AppUserId", "CreatedDate", "CryptoId", "ExchangeTransactionTypeId", "Fee", "FromExchangeId", "Price", "Quantity", "ToExchangeId", "TransactionDate", "TransactionReferenceNum", "TransactionTotal" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 3, 11, 11, 48, 23, 958, DateTimeKind.Local).AddTicks(3966), 1, 1, 0.80m, 1, 23500.00m, 0.004m, null, new DateTime(2023, 3, 9, 11, 48, 23, 958, DateTimeKind.Local).AddTicks(3922), 1000, 97.00m },
                    { 2, 1, new DateTime(2023, 3, 11, 11, 48, 23, 958, DateTimeKind.Local).AddTicks(3976), 1, 1, 0.85m, 1, 23400.00m, 0.005m, null, new DateTime(2023, 3, 10, 11, 48, 23, 958, DateTimeKind.Local).AddTicks(3972), 1001, 117.00m },
                    { 3, 1, new DateTime(2023, 3, 11, 11, 48, 23, 958, DateTimeKind.Local).AddTicks(3985), 1, 1, 0.84m, 1, 23450.00m, 0.005m, null, new DateTime(2023, 3, 11, 11, 48, 23, 958, DateTimeKind.Local).AddTicks(3981), 1003, 117.25m },
                    { 4, 1, new DateTime(2023, 3, 11, 11, 48, 23, 958, DateTimeKind.Local).AddTicks(4005), 2, 1, 0.50m, 1, 0.35m, 100m, null, new DateTime(2023, 3, 6, 11, 48, 23, 958, DateTimeKind.Local).AddTicks(4001), 2001, 35m },
                    { 5, 1, new DateTime(2023, 3, 11, 11, 48, 23, 958, DateTimeKind.Local).AddTicks(4014), 2, 1, 0.75m, 1, 0.40m, 150m, null, new DateTime(2023, 3, 9, 11, 48, 23, 958, DateTimeKind.Local).AddTicks(4010), 2002, 60m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cryptos",
                keyColumn: "CryptoId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cryptos",
                keyColumn: "CryptoId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cryptos",
                keyColumn: "CryptoId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cryptos",
                keyColumn: "CryptoId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cryptos",
                keyColumn: "CryptoId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Cryptos",
                keyColumn: "CryptoId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Cryptos",
                keyColumn: "CryptoId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Cryptos",
                keyColumn: "CryptoId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Cryptos",
                keyColumn: "CryptoId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Cryptos",
                keyColumn: "CryptoId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ExchangeTransactionTypes",
                keyColumn: "ExchangeTransactionTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ExchangeTransactionTypes",
                keyColumn: "ExchangeTransactionTypeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ExchangeTransactionTypes",
                keyColumn: "ExchangeTransactionTypeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ExchangeTransactionTypes",
                keyColumn: "ExchangeTransactionTypeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ExchangeTransactionTypes",
                keyColumn: "ExchangeTransactionTypeId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ExchangeTransactionTypes",
                keyColumn: "ExchangeTransactionTypeId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ExchangeTransactionTypes",
                keyColumn: "ExchangeTransactionTypeId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "ExchangeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "ExchangeId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "TransactionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "TransactionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "TransactionId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "TransactionId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "TransactionId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cryptos",
                keyColumn: "CryptoId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cryptos",
                keyColumn: "CryptoId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ExchangeTransactionTypes",
                keyColumn: "ExchangeTransactionTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "ExchangeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "ExchangeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "ExchangeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "ExchangeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: 1);
        }
    }
}
