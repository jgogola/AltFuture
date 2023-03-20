using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AltFutureWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFieldsToCryptoPriceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MarketCap",
                table: "CryptoPrices",
                type: "decimal(25,10)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MarketCapDominance",
                table: "CryptoPrices",
                type: "decimal(25,10)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PercentChange1h",
                table: "CryptoPrices",
                type: "decimal(20,10)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PercentChange24h",
                table: "CryptoPrices",
                type: "decimal(20,10)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PercentChange30d",
                table: "CryptoPrices",
                type: "decimal(20,10)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PercentChange60d",
                table: "CryptoPrices",
                type: "decimal(20,10)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PercentChange7d",
                table: "CryptoPrices",
                type: "decimal(20,10)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PercentChange90d",
                table: "CryptoPrices",
                type: "decimal(20,10)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Volume24h",
                table: "CryptoPrices",
                type: "decimal(25,10)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "VolumeChange24h",
                table: "CryptoPrices",
                type: "decimal(20,10)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarketCap",
                table: "CryptoPrices");

            migrationBuilder.DropColumn(
                name: "MarketCapDominance",
                table: "CryptoPrices");

            migrationBuilder.DropColumn(
                name: "PercentChange1h",
                table: "CryptoPrices");

            migrationBuilder.DropColumn(
                name: "PercentChange24h",
                table: "CryptoPrices");

            migrationBuilder.DropColumn(
                name: "PercentChange30d",
                table: "CryptoPrices");

            migrationBuilder.DropColumn(
                name: "PercentChange60d",
                table: "CryptoPrices");

            migrationBuilder.DropColumn(
                name: "PercentChange7d",
                table: "CryptoPrices");

            migrationBuilder.DropColumn(
                name: "PercentChange90d",
                table: "CryptoPrices");

            migrationBuilder.DropColumn(
                name: "Volume24h",
                table: "CryptoPrices");

            migrationBuilder.DropColumn(
                name: "VolumeChange24h",
                table: "CryptoPrices");
        }
    }
}
