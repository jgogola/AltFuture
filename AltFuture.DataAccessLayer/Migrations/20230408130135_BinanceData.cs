using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AltFuture.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class BinanceData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Exchanges",
                keyColumn: "ExchangeId",
                keyValue: 4,
                column: "ExchangeName",
                value: "Etoro");

            migrationBuilder.InsertData(
                table: "Exchanges",
                columns: new[] { "ExchangeId", "ExchangeName" },
                values: new object[] { 5, "Binance" });

            migrationBuilder.InsertData(
                table: "ExchangeTransactionTypes",
                columns: new[] { "ExchangeTransactionTypeId", "DataImportTypeId", "ExchangeId", "ExchangeTransactionTypeName", "TransactionTypeId" },
                values: new object[] { 13, 2, 5, "Buy", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ExchangeTransactionTypes",
                keyColumn: "ExchangeTransactionTypeId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "ExchangeId",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Exchanges",
                keyColumn: "ExchangeId",
                keyValue: 4,
                column: "ExchangeName",
                value: "Binance");
        }
    }
}
