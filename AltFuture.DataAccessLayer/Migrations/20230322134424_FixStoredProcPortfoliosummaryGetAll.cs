using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AltFuture.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class FixStoredProcPortfoliosummaryGetAll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			var updateProcSql = @"CREATE OR ALTER PROCEDURE dbo.PortfolioSummaryGetAll
AS
BEGIN

	SET NOCOUNT ON;
	;WITH cteTransactionGroupTotal AS (
		SELECT
			 C.CryptoId
			,C.CryptoName
			,C.TickerSymbol
			,COUNT(C.TickerSymbol) NumberOfOrders
			,SUM(T.Quantity) Quantity
			,SUM(T.InvestmentTotal) InvestmentTotal
			,SUM(T.Fee) FeeTotal
			,SUM(T.InvestmentTotal) / SUM(T.Quantity) AverageBuyPrice
		FROM [AltFuture].[dbo].[Transactions] T
		INNER JOIN dbo.Cryptos C ON C.CryptoId = T.CryptoId
		INNER JOIN dbo.ExchangeTransactionTypes ETT ON ETT.ExchangeTransactionTypeId = T.ExchangeTransactionTypeId
		WHERE ETT.TransactionTypeId IN (1,5,6,7)
		GROUP BY C.CryptoId, C.CryptoName,  C.TickerSymbol
	) 
	, cteCryptoPrices AS
	(
	   SELECT 
			 CP.CryptoPriceId
			,CP.CryptoId
			,CP.DateRecorded
			,CP.Price
			,ROW_NUMBER() OVER (PARTITION BY CP.CryptoId ORDER BY CP.DateRecorded DESC) AS RowNum
	   FROM CryptoPrices CP
	)
	, cteUnrealizedProfit AS (

		SELECT
			T.CryptoId
		   ,CP.CryptoPriceId
		   ,T.CryptoName
		   ,T.TickerSymbol
		   ,T.NumberOfOrders
		   ,ISNULL(CP.Price,0.00) AS Price
		   ,CP.DateRecorded
		   ,T.Quantity
		   ,T.InvestmentTotal
		   ,T.FeeTotal
		   ,T.AverageBuyPrice
		   ,(T.Quantity * ISNULL(CP.Price,0.00)) - (T.Quantity * T.AverageBuyPrice) - T.FeeTotal AS UnrealizedProfit
		FROM cteTransactionGroupTotal T
		INNER JOIN dbo.Cryptos C ON C.TickerSymbol = T.TickerSymbol
		LEFT JOIN cteCryptoPrices CP ON CP.CryptoId = C.CryptoId AND CP.RowNum = 1
	)
	SELECT
		P.CryptoId
	   ,P.CryptoName
	   ,P.TickerSymbol
	   ,P.NumberOfOrders
	   ,CAST(P.Quantity AS decimal(10,3)) AS Quantity
	   ,CAST(P.AverageBuyPrice AS decimal(10,3)) AS AverageBuyPrice
	   ,CAST(P.Price AS decimal(10,3)) AS Price
	   ,P.DateRecorded
	   ,CAST(P.InvestmentTotal AS decimal(10,3)) AS InvestmentTotal
	   ,CAST(P.UnrealizedProfit AS decimal(10,3)) AS UnrealizedProfit
	   ,CAST(P.FeeTotal as decimal(10,3)) AS FeeTotal
	   ,CAST((P.InvestmentTotal + UnrealizedProfit) AS decimal(10,3)) AS CurrentWorth
	   ,1 AS RowType
	FROM cteUnrealizedProfit P

	UNION

	SELECT
	     -1
		,''
		,'TOTAL'
		,SUM(P.NumberOfOrders)
		,CAST(SUM(P.Quantity) AS decimal(10,3))
		,0.00
		,0.00
		,NULL
		,SUM(CAST(P.InvestmentTotal AS decimal(10,3)))
		,SUM(CAST(P.UnrealizedProfit AS decimal(10,3))) 
		,SUM(CAST(P.FeeTotal as decimal(10,3)))
		,CAST(SUM(P.InvestmentTotal) + SUM(UnrealizedProfit) AS decimal(10,3)) as currentWorth
		,99 as RowType
    FROM cteUnrealizedProfit P
	ORDER BY RowType, InvestmentTotal DESC;
END";

            migrationBuilder.Sql(updateProcSql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var revertProcSql = @"CREATE OR ALTER PROCEDURE dbo.PortfolioSummaryGetAll
AS
BEGIN

	SET NOCOUNT ON;

	;WITH cteTransactionGroupTotal AS (
		SELECT
			 C.CryptoId
			,C.CryptoName
			,C.TickerSymbol
			,COUNT(C.TickerSymbol) NumberOfOrders
			,SUM(T.Quantity) Quantity
			,SUM(T.TransactionTotal) TotalInvested
			,SUM(T.TransactionTotal) / SUM(T.Quantity) AverageBuyPrice
		FROM [AltFuture].[dbo].[Transactions] T
		INNER JOIN dbo.Cryptos C ON C.CryptoId = T.CryptoId
		INNER JOIN dbo.ExchangeTransactionTypes ETT ON ETT.ExchangeTransactionTypeId = T.ExchangeTransactionTypeId
		WHERE ETT.TransactionTypeId IN (1,5,6,7)
		GROUP BY C.CryptoId, C.CryptoName,  C.TickerSymbol
	) 
	, cteCryptoPrices AS
	(
	   SELECT 
			 CP.CryptoPriceId
			,CP.CryptoId
			,CP.DateRecorded
			,CP.Price
			,ROW_NUMBER() OVER (PARTITION BY CP.CryptoId ORDER BY CP.DateRecorded DESC) AS RowNum
	   FROM CryptoPrices CP
	)
	, cteUnrealizedProfit AS (

		SELECT
			T.CryptoId
		   ,CP.CryptoPriceId
		   ,T.CryptoName
		   ,T.TickerSymbol
		   ,T.NumberOfOrders
		   ,ISNULL(CP.Price,0.00) AS Price
		   ,CP.DateRecorded
		   ,T.Quantity
		   ,T.TotalInvested
		   ,T.AverageBuyPrice
		   ,(T.Quantity * ISNULL(CP.Price,0.00)) - (T.Quantity * T.AverageBuyPrice) AS UnrealizedProfit
		FROM cteTransactionGroupTotal T
		INNER JOIN dbo.Cryptos C ON C.TickerSymbol = T.TickerSymbol
		LEFT JOIN cteCryptoPrices CP ON CP.CryptoId = C.CryptoId AND CP.RowNum = 1
	)
	SELECT
		P.CryptoId
	   ,P.CryptoName
	   ,P.TickerSymbol
	   ,P.NumberOfOrders
	   ,CAST(P.Quantity AS decimal(10,3)) AS Quantity
	   ,CAST(P.AverageBuyPrice AS decimal(10,3)) AS AverageBuyPrice
	   ,CAST(P.Price AS decimal(10,3)) AS Price
	   ,P.DateRecorded
	   ,CAST(P.TotalInvested AS decimal(10,3)) AS TotalInvested
	   ,CAST(P.UnrealizedProfit AS decimal(10,3)) AS UnrealizedProfit
	   ,CAST((P.TotalInvested + UnrealizedProfit) AS decimal(10,3)) AS currentWorth
	   ,1 AS RowType
	FROM cteUnrealizedProfit P

	UNION

	SELECT
	     -1
		,''
		,'TOTAL'
		,SUM(P.NumberOfOrders)
		,CAST(SUM(P.Quantity) AS decimal(10,3))
		,0.00
		,0.00
		,NULL
		,SUM(CAST(P.TotalInvested AS decimal(10,3)))
		,SUM(CAST(P.UnrealizedProfit AS decimal(10,3))) 
		,CAST(SUM(P.TotalInvested) + SUM(UnrealizedProfit) AS decimal(10,3)) as currentWorth
		,99 as RowType
    FROM cteUnrealizedProfit P
	ORDER BY RowType, TotalInvested DESC;


END";
            migrationBuilder.Sql(revertProcSql);
        }
    }
}
