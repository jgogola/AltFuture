using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AltFuture.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddExchangeYoroi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Exchanges",
                columns: new[] { "ExchangeId", "ExchangeName" },
                values: new object[] { 6, "Yoroi Wallet" });

            migrationBuilder.InsertData(
                table: "ExchangeTransactionTypes",
                columns: new[] { "ExchangeTransactionTypeId", "ExchangeTransactionTypeName", "ExchangeId", "TransactionTypeId", "DataImportTypeId" },
                values: new object[] { 15, "Staking Reward", 6, 4, 2});
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "ExchangeId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ExchangeTransactionTypes",
                keyColumn: "ExchangeTransactionTypeId",
                keyValue: 15);
        }
    }
}
