using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AltFuture.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class NewSeedData_ModelUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                table: "Exchanges",
                keyColumn: "ExchangeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "ExchangeId",
                keyValue: 4);

            migrationBuilder.AddColumn<int>(
                name: "DataImportTypeId",
                table: "ExchangeTransactionTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DataImportTypes",
                columns: table => new
                {
                    DataImportTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataImportTypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataImportTypes", x => x.DataImportTypeId);
                });

            migrationBuilder.InsertData(
                table: "DataImportTypes",
                columns: new[] { "DataImportTypeId", "DataImportTypeName" },
                values: new object[,]
                {
                    { 1, "API" },
                    { 2, "CSV" }
                });

            migrationBuilder.UpdateData(
                table: "ExchangeTransactionTypes",
                keyColumn: "ExchangeTransactionTypeId",
                keyValue: 1,
                column: "DataImportTypeId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ExchangeTransactionTypes",
                keyColumn: "ExchangeTransactionTypeId",
                keyValue: 2,
                columns: new[] { "DataImportTypeId", "ExchangeId", "ExchangeTransactionTypeName" },
                values: new object[] { 2, 1, "Advanced Trade Buy" });

            migrationBuilder.UpdateData(
                table: "ExchangeTransactionTypes",
                keyColumn: "ExchangeTransactionTypeId",
                keyValue: 3,
                columns: new[] { "DataImportTypeId", "ExchangeId", "ExchangeTransactionTypeName", "TransactionTypeId" },
                values: new object[] { 2, 1, "Sell", 2 });

            migrationBuilder.UpdateData(
                table: "ExchangeTransactionTypes",
                keyColumn: "ExchangeTransactionTypeId",
                keyValue: 4,
                columns: new[] { "DataImportTypeId", "ExchangeId", "ExchangeTransactionTypeName", "TransactionTypeId" },
                values: new object[] { 2, 1, "Rewards Income", 4 });

            migrationBuilder.UpdateData(
                table: "ExchangeTransactionTypes",
                keyColumn: "ExchangeTransactionTypeId",
                keyValue: 5,
                columns: new[] { "DataImportTypeId", "ExchangeTransactionTypeName", "TransactionTypeId" },
                values: new object[] { 2, "Learning Reward", 4 });

            migrationBuilder.UpdateData(
                table: "Exchanges",
                keyColumn: "ExchangeId",
                keyValue: 2,
                column: "ExchangeName",
                value: "Coinbase Pro");

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: 3,
                column: "TransactionTypeName",
                value: "Withdrawl");

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: 4,
                column: "TransactionTypeName",
                value: "Staking Reward");

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: 5,
                column: "TransactionTypeName",
                value: "Loan Interest");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeTransactionTypes_DataImportTypeId",
                table: "ExchangeTransactionTypes",
                column: "DataImportTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeTransactionTypes_DataImportTypes_DataImportTypeId",
                table: "ExchangeTransactionTypes",
                column: "DataImportTypeId",
                principalTable: "DataImportTypes",
                principalColumn: "DataImportTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeTransactionTypes_DataImportTypes_DataImportTypeId",
                table: "ExchangeTransactionTypes");

            migrationBuilder.DropTable(
                name: "DataImportTypes");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeTransactionTypes_DataImportTypeId",
                table: "ExchangeTransactionTypes");

            migrationBuilder.DropColumn(
                name: "DataImportTypeId",
                table: "ExchangeTransactionTypes");

            migrationBuilder.UpdateData(
                table: "ExchangeTransactionTypes",
                keyColumn: "ExchangeTransactionTypeId",
                keyValue: 2,
                columns: new[] { "ExchangeId", "ExchangeTransactionTypeName" },
                values: new object[] { 2, "viban_purchase" });

            migrationBuilder.UpdateData(
                table: "ExchangeTransactionTypes",
                keyColumn: "ExchangeTransactionTypeId",
                keyValue: 3,
                columns: new[] { "ExchangeId", "ExchangeTransactionTypeName", "TransactionTypeId" },
                values: new object[] { 3, "Buy", 1 });

            migrationBuilder.UpdateData(
                table: "ExchangeTransactionTypes",
                keyColumn: "ExchangeTransactionTypeId",
                keyValue: 4,
                columns: new[] { "ExchangeId", "ExchangeTransactionTypeName", "TransactionTypeId" },
                values: new object[] { 4, "Open Position", 1 });

            migrationBuilder.UpdateData(
                table: "ExchangeTransactionTypes",
                keyColumn: "ExchangeTransactionTypeId",
                keyValue: 5,
                columns: new[] { "ExchangeTransactionTypeName", "TransactionTypeId" },
                values: new object[] { "Sell", 2 });

            migrationBuilder.InsertData(
                table: "ExchangeTransactionTypes",
                columns: new[] { "ExchangeTransactionTypeId", "ExchangeId", "ExchangeTransactionTypeName", "TransactionTypeId" },
                values: new object[] { 6, 2, "Sell", 2 });

            migrationBuilder.UpdateData(
                table: "Exchanges",
                keyColumn: "ExchangeId",
                keyValue: 2,
                column: "ExchangeName",
                value: "Crypto.com");

            migrationBuilder.InsertData(
                table: "Exchanges",
                columns: new[] { "ExchangeId", "ExchangeName" },
                values: new object[,]
                {
                    { 3, "Kucoin" },
                    { 4, "Etoro" },
                    { 5, "CDC Defi Wallet" },
                    { 6, "Yoroi Wallet" }
                });

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: 3,
                column: "TransactionTypeName",
                value: "Stakeing Reward");

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: 4,
                column: "TransactionTypeName",
                value: "Loan Interest");

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: 5,
                column: "TransactionTypeName",
                value: "Card Cashback");

            migrationBuilder.InsertData(
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeId", "TransactionTypeName" },
                values: new object[,]
                {
                    { 6, "Card Cashback Reversalv" },
                    { 7, "Reimbursement" },
                    { 8, "Withdrawl" }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionId", "AppUserId", "CreatedDate", "CryptoId", "ExchangeTransactionTypeId", "Fee", "FromExchangeId", "Price", "Quantity", "ToExchangeId", "TransactionDate", "TransactionReferenceNum", "TransactionTotal" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 3, 11, 11, 52, 29, 147, DateTimeKind.Local).AddTicks(854), 1, 1, 0.80m, 1, 23500.00m, 0.004m, null, new DateTime(2023, 3, 9, 11, 52, 29, 147, DateTimeKind.Local).AddTicks(809), 1000, 97.00m },
                    { 2, 1, new DateTime(2023, 3, 11, 11, 52, 29, 147, DateTimeKind.Local).AddTicks(863), 1, 1, 0.85m, 1, 23400.00m, 0.005m, null, new DateTime(2023, 3, 10, 11, 52, 29, 147, DateTimeKind.Local).AddTicks(860), 1001, 117.00m },
                    { 3, 1, new DateTime(2023, 3, 11, 11, 52, 29, 147, DateTimeKind.Local).AddTicks(870), 1, 1, 0.84m, 1, 23450.00m, 0.005m, null, new DateTime(2023, 3, 11, 11, 52, 29, 147, DateTimeKind.Local).AddTicks(867), 1003, 117.25m },
                    { 4, 1, new DateTime(2023, 3, 11, 11, 52, 29, 147, DateTimeKind.Local).AddTicks(878), 2, 1, 0.50m, 1, 0.35m, 100m, null, new DateTime(2023, 3, 6, 11, 52, 29, 147, DateTimeKind.Local).AddTicks(876), 2001, 35m },
                    { 5, 1, new DateTime(2023, 3, 11, 11, 52, 29, 147, DateTimeKind.Local).AddTicks(885), 2, 1, 0.75m, 1, 0.40m, 150m, null, new DateTime(2023, 3, 9, 11, 52, 29, 147, DateTimeKind.Local).AddTicks(882), 2002, 60m }
                });

            migrationBuilder.InsertData(
                table: "ExchangeTransactionTypes",
                columns: new[] { "ExchangeTransactionTypeId", "ExchangeId", "ExchangeTransactionTypeName", "TransactionTypeId" },
                values: new object[,]
                {
                    { 7, 3, "Sell", 2 },
                    { 8, 4, "Sell", 2 }
                });
        }
    }
}
