using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AltFuture.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class TransactionTypeDataUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeId", "TransactionTypeName" },
                values: new object[,]
                {
                                { 6, "Perk Reward" },
                                { 7, "Deposit" }
                });

            migrationBuilder.UpdateData(
                table: "ExchangeTransactionTypes",
                keyColumn: "ExchangeTransactionTypeId",
                keyValue: 5,
                column: "TransactionTypeId",
                value: 6);

            migrationBuilder.InsertData(
                table: "Exchanges",
                columns: new[] { "ExchangeId", "ExchangeName" },
                values: new object[] { 3, "Crypto.com" });



            migrationBuilder.InsertData(
                table: "ExchangeTransactionTypes",
                columns: new[] { "ExchangeTransactionTypeId", "DataImportTypeId", "ExchangeId", "ExchangeTransactionTypeName", "TransactionTypeId" },
                values: new object[,]
                {
                    { 6, 2, 3, "viban_purchase", 1 },
                    { 7, 2, 3, "reimbursement", 6 },
                    { 8, 2, 3, "referral_card_cashback", 6 },
                    { 9, 2, 3, "card_cashback_reverted", 6 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ExchangeTransactionTypes",
                keyColumn: "ExchangeTransactionTypeId",
                keyValue: 5,
                column: "TransactionTypeId",
                value: 4);

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
                table: "ExchangeTransactionTypes",
                keyColumn: "ExchangeTransactionTypeId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "ExchangeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: 6);


        }
    }
}
