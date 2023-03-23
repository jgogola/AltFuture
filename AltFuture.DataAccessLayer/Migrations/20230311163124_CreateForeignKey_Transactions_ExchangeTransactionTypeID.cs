using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AltFuture.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class CreateForeignKey_Transactions_ExchangeTransactionTypeID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeTransactionTypes_Exchanges_ExchageId",
                table: "ExchangeTransactionTypes");

            migrationBuilder.RenameColumn(
                name: "ExchageId",
                table: "ExchangeTransactionTypes",
                newName: "ExchangeId");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeTransactionTypes_ExchageId",
                table: "ExchangeTransactionTypes",
                newName: "IX_ExchangeTransactionTypes_ExchangeId");

            migrationBuilder.AddColumn<int>(
                name: "ExchangeTransactionTypeId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

         
            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ExchangeTransactionTypeId",
                table: "Transactions",
                column: "ExchangeTransactionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeTransactionTypes_Exchanges_ExchangeId",
                table: "ExchangeTransactionTypes",
                column: "ExchangeId",
                principalTable: "Exchanges",
                principalColumn: "ExchangeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_ExchangeTransactionTypes_ExchangeTransactionTypeId",
                table: "Transactions",
                column: "ExchangeTransactionTypeId",
                principalTable: "ExchangeTransactionTypes",
                principalColumn: "ExchangeTransactionTypeId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeTransactionTypes_Exchanges_ExchangeId",
                table: "ExchangeTransactionTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_ExchangeTransactionTypes_ExchangeTransactionTypeId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_ExchangeTransactionTypeId",
                table: "Transactions");

           
            migrationBuilder.DropColumn(
                name: "ExchangeTransactionTypeId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "ExchangeId",
                table: "ExchangeTransactionTypes",
                newName: "ExchageId");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeTransactionTypes_ExchangeId",
                table: "ExchangeTransactionTypes",
                newName: "IX_ExchangeTransactionTypes_ExchageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeTransactionTypes_Exchanges_ExchageId",
                table: "ExchangeTransactionTypes",
                column: "ExchageId",
                principalTable: "Exchanges",
                principalColumn: "ExchangeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
