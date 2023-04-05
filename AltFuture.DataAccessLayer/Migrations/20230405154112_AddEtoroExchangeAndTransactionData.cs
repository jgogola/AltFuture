using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AltFuture.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddEtoroExchangeAndTransactionData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Exchanges",
                columns: new[] { "ExchangeId", "ExchangeName" },
                values: new object[] { 4, "Etoro" });

            migrationBuilder.InsertData(
                table: "ExchangeTransactionTypes",
                columns: new[] { "ExchangeTransactionTypeId", "DataImportTypeId", "ExchangeId", "ExchangeTransactionTypeName", "TransactionTypeId" },
                values: new object[,]
                {
                    { 11, 2, 4, "Open Position", 1 },
                    { 12, 2, 4, "Staking", 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ExchangeTransactionTypes",
                keyColumn: "ExchangeTransactionTypeId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ExchangeTransactionTypes",
                keyColumn: "ExchangeTransactionTypeId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "ExchangeId",
                keyValue: 4);
        }
    }
}
