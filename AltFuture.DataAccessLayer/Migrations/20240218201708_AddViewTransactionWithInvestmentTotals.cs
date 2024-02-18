using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AltFuture.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddViewTransactionWithInvestmentTotals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE VIEW v_TransactionWithInvestmentTotals AS
                    SELECT 
                        t.TransactionId, 
	                    t.TransactionReferenceNum,
	                    t.AppUserId,
	                    t.CryptoId,
	                    t.ExchangeTransactionTypeId,
                        t.Price, 
                        t.Quantity,
	                    t.Fee,
	                    t.TransactionTotal,
	                    t.TransactionDate,
	                    t.FromExchangeId,
	                    t.ToExchangeId,
	                    t.CreatedDate,
                        tt.TransactionTypeId,
                        CASE 
                            WHEN tt.TransactionTypeId = 1 THEN t.Price * t.Quantity 
                            ELSE 0 
                        END AS InvestmentTotal
                    FROM 
                        Transactions t
                        INNER JOIN ExchangeTransactionTypes ett ON t.ExchangeTransactionTypeId = ett.ExchangeTransactionTypeId
                        INNER JOIN TransactionTypes tt ON ett.TransactionTypeId = tt.TransactionTypeId
                "
            );

            migrationBuilder.DropColumn(
                name: "InvestmentTotal",
                table: "Transactions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW IF EXISTS v_TransactionWithInvestmentTotals");

            migrationBuilder.AddColumn<decimal>(
                name: "InvestmentTotal",
                table: "Transactions",
                type: "decimal(18,10)",
                nullable: false,
                computedColumnSql: "Price * Quantity");
        }
    }
}
