using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AltFuture.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class IncreaseTransactionQuantitySize : Migration
    {
        /// <inheritdoc />
        /// 
       

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE Transactions DROP COLUMN InvestmentTotal");

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "Transactions",
                type: "decimal(20,10)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,10)");

            migrationBuilder.Sql("ALTER TABLE Transactions ADD InvestmentTotal AS (Price * Quantity)");

            var updatePortfolioSummary = @"CREATE OR ALTER PROCEDURE [dbo].[PortfolioSummary]
(	
	@AppUserId	INT
)
AS
BEGIN

	SET NOCOUNT ON;

	
	DECLARE @TransactionTypes AS Table (TransactionTypeId int);
	INSERT INTO @TransactionTypes 
	VALUES
	(1),(5),(6),(7); --* Buy, Loan Interest, Perk Reward, Deposit

	;WITH cteTransactionGroupTotal AS (
		SELECT
			 C.CryptoId
			,C.CryptoName
			,C.TickerSymbol
			,C.CryptoIcon
			,COUNT(C.TickerSymbol) NumberOfOrders
			,SUM(T.Quantity) Quantity
			,SUM(T.InvestmentTotal) InvestmentTotal
			,SUM(T.Fee) FeeTotal
			,SUM(T.InvestmentTotal) / SUM(T.Quantity) AverageBuyPrice
		FROM [AltFuture].[dbo].[Transactions] T
		INNER JOIN dbo.Cryptos C ON C.CryptoId = T.CryptoId
		INNER JOIN dbo.ExchangeTransactionTypes ETT ON ETT.ExchangeTransactionTypeId = T.ExchangeTransactionTypeId
		WHERE ETT.TransactionTypeId IN (SELECT TransactionTypeId FROM @TransactionTypes)
		AND T.AppUserId = @AppUserId
		GROUP BY C.CryptoId, C.CryptoName, C.TickerSymbol, C.CryptoIcon
	) 
	, cteCryptoPrices AS
	(
		SELECT
			 sub.CryptoPriceId
			,sub.CryptoId
			,sub.DateRecorded
			,sub.Price
			,sub.PercentChange24h
			,sub.RowNum
		FROM (
			SELECT 
			 	 CP.CryptoPriceId
				,CP.CryptoId
				,CP.DateRecorded
				,CP.Price
				,CP.PercentChange24h
				,ROW_NUMBER() OVER (PARTITION BY CP.CryptoId ORDER BY CP.DateRecorded DESC) AS RowNum
			FROM CryptoPrices CP
		) sub
		WHERE sub.RowNum = 1
	)
	, cteUnrealizedProfit AS (

		SELECT
			 T.CryptoId
			,CP.CryptoPriceId
			,T.CryptoName
			,T.TickerSymbol
			,T.CryptoIcon
			,T.NumberOfOrders
			,ISNULL(CP.Price,0.00) AS Price
			,CP.PercentChange24h
			,CP.DateRecorded
			,T.Quantity
			,T.InvestmentTotal
			,T.FeeTotal
			,T.AverageBuyPrice
			,(T.Quantity * ISNULL(CP.Price,0.00)) - (T.Quantity * T.AverageBuyPrice) - T.FeeTotal AS UnrealizedProfit
		FROM cteTransactionGroupTotal T
		INNER JOIN dbo.Cryptos C ON C.TickerSymbol = T.TickerSymbol
		LEFT JOIN cteCryptoPrices CP ON CP.CryptoId = C.CryptoId 
	)
	SELECT
		P.CryptoId
		,P.CryptoName
		,P.TickerSymbol
		,P.CryptoIcon
		,P.NumberOfOrders
		,CAST(P.Quantity AS decimal(15,4)) AS Quantity
		,CAST(P.AverageBuyPrice AS decimal(10,3)) AS AverageBuyPrice
		,CAST(P.Price AS decimal(15,3)) AS Price
		,P.PercentChange24h
		,P.DateRecorded
		,CAST(P.InvestmentTotal AS decimal(15,3)) AS InvestmentTotal
		,CAST(P.UnrealizedProfit AS decimal(15,3)) AS UnrealizedProfit
		,CAST(P.FeeTotal as decimal(10,3)) AS FeeTotal
		,CAST((P.InvestmentTotal + UnrealizedProfit) AS decimal(15,3)) AS CurrentWorth
		,1 AS RowType
	FROM cteUnrealizedProfit P

	UNION

	SELECT
			-1
		,''
		,'TOTAL'
		,''
		,SUM(P.NumberOfOrders)
		,CAST(SUM(P.Quantity) AS decimal(15,3))
		,0.00
		,0.00
		,0.00
		,NULL
		,SUM(CAST(P.InvestmentTotal AS decimal(15,3)))
		,SUM(CAST(P.UnrealizedProfit AS decimal(15,3))) 
		,SUM(CAST(P.FeeTotal as decimal(15,3)))
		,CAST(SUM(P.InvestmentTotal) + SUM(UnrealizedProfit) AS decimal(15,3)) as currentWorth
		,99 as RowType
	FROM cteUnrealizedProfit P
	ORDER BY RowType, InvestmentTotal DESC;
END
";

            migrationBuilder.Sql(updatePortfolioSummary);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE Transactions DROP COLUMN InvestmentTotal");

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "Transactions",
                type: "decimal(18,10)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,10)");

            migrationBuilder.Sql("ALTER TABLE Transactions ADD InvestmentTotal AS (Price * Quantity)");

            var updatePortfolioSummary = @"CREATE OR ALTER PROCEDURE [dbo].[PortfolioSummary]
(	
	@AppUserId	INT
)
AS
BEGIN

	SET NOCOUNT ON;

	
	DECLARE @TransactionTypes AS Table (TransactionTypeId int);
	INSERT INTO @TransactionTypes 
	VALUES
	(1),(5),(6),(7); --* Buy, Loan Interest, Perk Reward, Deposit

	;WITH cteTransactionGroupTotal AS (
		SELECT
				C.CryptoId
			,C.CryptoName
			,C.TickerSymbol
			,C.CryptoIcon
			,COUNT(C.TickerSymbol) NumberOfOrders
			,SUM(T.Quantity) Quantity
			,SUM(T.InvestmentTotal) InvestmentTotal
			,SUM(T.Fee) FeeTotal
			,SUM(T.InvestmentTotal) / SUM(T.Quantity) AverageBuyPrice
		FROM [AltFuture].[dbo].[Transactions] T
		INNER JOIN dbo.Cryptos C ON C.CryptoId = T.CryptoId
		INNER JOIN dbo.ExchangeTransactionTypes ETT ON ETT.ExchangeTransactionTypeId = T.ExchangeTransactionTypeId
		WHERE ETT.TransactionTypeId IN (SELECT TransactionTypeId FROM @TransactionTypes)
		AND T.AppUserId = @AppUserId
		GROUP BY C.CryptoId, C.CryptoName, C.TickerSymbol, C.CryptoIcon
	) 
	, cteCryptoPrices AS
	(
		SELECT
				sub.CryptoPriceId
			,sub.CryptoId
			,sub.DateRecorded
			,sub.Price
			,sub.PercentChange24h
			,sub.RowNum
		FROM (
			SELECT 
			 		CP.CryptoPriceId
				,CP.CryptoId
				,CP.DateRecorded
				,CP.Price
				,CP.PercentChange24h
				,ROW_NUMBER() OVER (PARTITION BY CP.CryptoId ORDER BY CP.DateRecorded DESC) AS RowNum
			FROM CryptoPrices CP
		) sub
		WHERE sub.RowNum = 1
	)
	, cteUnrealizedProfit AS (

		SELECT
			T.CryptoId
			,CP.CryptoPriceId
			,T.CryptoName
			,T.TickerSymbol
			,T.CryptoIcon
			,T.NumberOfOrders
			,ISNULL(CP.Price,0.00) AS Price
			,CP.PercentChange24h
			,CP.DateRecorded
			,T.Quantity
			,T.InvestmentTotal
			,T.FeeTotal
			,T.AverageBuyPrice
			,(T.Quantity * ISNULL(CP.Price,0.00)) - (T.Quantity * T.AverageBuyPrice) - T.FeeTotal AS UnrealizedProfit
		FROM cteTransactionGroupTotal T
		INNER JOIN dbo.Cryptos C ON C.TickerSymbol = T.TickerSymbol
		LEFT JOIN cteCryptoPrices CP ON CP.CryptoId = C.CryptoId 
	)
	SELECT
		P.CryptoId
		,P.CryptoName
		,P.TickerSymbol
		,P.CryptoIcon
		,P.NumberOfOrders
		,CAST(P.Quantity AS decimal(15,4)) AS Quantity
		,CAST(P.AverageBuyPrice AS decimal(10,3)) AS AverageBuyPrice
		,CAST(P.Price AS decimal(10,3)) AS Price
		,P.PercentChange24h
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
		,''
		,SUM(P.NumberOfOrders)
		,CAST(SUM(P.Quantity) AS decimal(10,3))
		,0.00
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
END
";

            migrationBuilder.Sql(updatePortfolioSummary);
        }
    }
}
