using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AltFuture.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class CoinbaseProExchangeTransactionTypeData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ExchangeTransactionTypes",
                columns: new[] { "ExchangeTransactionTypeId", "DataImportTypeId", "ExchangeId", "ExchangeTransactionTypeName", "TransactionTypeId" },
                values: new object[] { 10, 2, 2, "Buy", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ExchangeTransactionTypes",
                keyColumn: "ExchangeTransactionTypeId",
                keyValue: 10);
        }
    }
}
