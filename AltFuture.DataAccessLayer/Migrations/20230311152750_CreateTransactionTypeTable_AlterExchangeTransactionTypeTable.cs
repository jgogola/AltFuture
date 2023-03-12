using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AltFutureWebApp.Migrations
{
    /// <inheritdoc />
    public partial class CreateTransactionTypeTable_AlterExchangeTransactionTypeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CommonTransactionType",
                table: "ExchangeTransactionTypes",
                newName: "TransactionTypeId");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "CryptoPrices",
                newName: "DateRecorded");

            migrationBuilder.CreateTable(
                name: "TransactionTypes",
                columns: table => new
                {
                    TransactionTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionTypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionTypes", x => x.TransactionTypeId);
                });



            migrationBuilder.CreateIndex(
                name: "IX_ExchangeTransactionTypes_TransactionTypeId",
                table: "ExchangeTransactionTypes",
                column: "TransactionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeTransactionTypes_TransactionTypes_TransactionTypeId",
                table: "ExchangeTransactionTypes",
                column: "TransactionTypeId",
                principalTable: "TransactionTypes",
                principalColumn: "TransactionTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeTransactionTypes_TransactionTypes_TransactionTypeId",
                table: "ExchangeTransactionTypes");

            migrationBuilder.DropTable(
                name: "TransactionTypes");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeTransactionTypes_TransactionTypeId",
                table: "ExchangeTransactionTypes");

       

            migrationBuilder.RenameColumn(
                name: "TransactionTypeId",
                table: "ExchangeTransactionTypes",
                newName: "CommonTransactionType");

            migrationBuilder.RenameColumn(
                name: "DateRecorded",
                table: "CryptoPrices",
                newName: "DateCreated");
        }
    }
}
