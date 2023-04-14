using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AltFuture.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class AlterCryptoPriceAddIsMonthStartPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMonthStartPrice",
                table: "CryptoPrices",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMonthStartPrice",
                table: "CryptoPrices");
        }
    }
}
