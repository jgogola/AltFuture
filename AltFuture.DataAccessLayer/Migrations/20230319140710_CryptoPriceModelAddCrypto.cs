using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AltFuture.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class CryptoPriceModelAddCrypto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CryptoPrices_CryptoId",
                table: "CryptoPrices",
                column: "CryptoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CryptoPrices_Cryptos_CryptoId",
                table: "CryptoPrices",
                column: "CryptoId",
                principalTable: "Cryptos",
                principalColumn: "CryptoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CryptoPrices_Cryptos_CryptoId",
                table: "CryptoPrices");

            migrationBuilder.DropIndex(
                name: "IX_CryptoPrices_CryptoId",
                table: "CryptoPrices");
        }
    }
}
