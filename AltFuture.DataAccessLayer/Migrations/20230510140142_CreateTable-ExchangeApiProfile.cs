using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AltFuture.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableExchangeApiProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExchangeApiProfiles",
                columns: table => new
                {
                    ExchangeApiProfileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    ExchangeId = table.Column<int>(type: "int", nullable: false),
                    ProfileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApiKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApiSecret = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    LastSyncDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeApiProfiles", x => x.ExchangeApiProfileId);
                    table.ForeignKey(
                        name: "FK_ExchangeApiProfiles_Exchanges_ExchangeId",
                        column: x => x.ExchangeId,
                        principalTable: "Exchanges",
                        principalColumn: "ExchangeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ExchangeTransactionTypes",
                columns: new[] { "ExchangeTransactionTypeId", "DataImportTypeId", "ExchangeId", "ExchangeTransactionTypeName", "TransactionTypeId" },
                values: new object[] { 14, 1, 1, "BUY", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeApiProfiles_ExchangeId",
                table: "ExchangeApiProfiles",
                column: "ExchangeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExchangeApiProfiles");

            migrationBuilder.DeleteData(
                table: "ExchangeTransactionTypes",
                keyColumn: "ExchangeTransactionTypeId",
                keyValue: 14);
        }
    }
}
