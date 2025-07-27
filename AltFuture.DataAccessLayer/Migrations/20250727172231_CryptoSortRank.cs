using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AltFuture.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class CryptoSortRank : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "SortRank",
                table: "Cryptos",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Cryptos",
                keyColumn: "CryptoId",
                keyValue: 1,
                column: "SortRank",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Cryptos",
                keyColumn: "CryptoId",
                keyValue: 2,
                column: "SortRank",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Cryptos",
                keyColumn: "CryptoId",
                keyValue: 3,
                column: "SortRank",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Cryptos",
                keyColumn: "CryptoId",
                keyValue: 4,
                column: "SortRank",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Cryptos",
                keyColumn: "CryptoId",
                keyValue: 5,
                column: "SortRank",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Cryptos",
                keyColumn: "CryptoId",
                keyValue: 6,
                column: "SortRank",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Cryptos",
                keyColumn: "CryptoId",
                keyValue: 7,
                columns: new[] { "SortRank", "TickerSymbol" },
                values: new object[] { 0.0, "POL" });

            migrationBuilder.UpdateData(
                table: "Cryptos",
                keyColumn: "CryptoId",
                keyValue: 8,
                column: "SortRank",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Cryptos",
                keyColumn: "CryptoId",
                keyValue: 9,
                column: "SortRank",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Cryptos",
                keyColumn: "CryptoId",
                keyValue: 10,
                column: "SortRank",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Cryptos",
                keyColumn: "CryptoId",
                keyValue: 11,
                column: "SortRank",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Cryptos",
                keyColumn: "CryptoId",
                keyValue: 12,
                column: "SortRank",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Cryptos",
                keyColumn: "CryptoId",
                keyValue: 13,
                column: "SortRank",
                value: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortRank",
                table: "Cryptos");

            migrationBuilder.UpdateData(
                table: "Cryptos",
                keyColumn: "CryptoId",
                keyValue: 7,
                column: "TickerSymbol",
                value: "MATIC");
        }
    }
}
