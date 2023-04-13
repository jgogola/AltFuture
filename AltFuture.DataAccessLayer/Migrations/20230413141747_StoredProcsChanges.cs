using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AltFuture.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class StoredProcsChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            var dropProcSql = "DROP PROC dbo.PortfolioRunningTotalGetAll";
            migrationBuilder.Sql(dropProcSql);

            var dropProcSql2 = "DROP PROC dbo.PortfolioSummaryGetAll";
            migrationBuilder.Sql(dropProcSql2);

			var createProcSql = @"CREATE OR ALTER PROCEDURE [dbo].[PortfolioSummary]
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
		GROUP BY C.CryptoId, C.CryptoName,  C.TickerSymbol
	) 
	, cteCryptoPrices AS
	(
		SELECT
			 sub.CryptoPriceId
			,sub.CryptoId
			,sub.DateRecorded
			,sub.Price
			,sub.RowNum
		FROM (
			SELECT 
			 	 CP.CryptoPriceId
				,CP.CryptoId
				,CP.DateRecorded
				,CP.Price
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
		LEFT JOIN cteCryptoPrices CP ON CP.CryptoId = C.CryptoId 
	)
	SELECT
		P.CryptoId
	   ,P.CryptoName
	   ,P.TickerSymbol
	   ,P.NumberOfOrders
	   ,CAST(P.Quantity AS decimal(15,4)) AS Quantity
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
END
";

            migrationBuilder.Sql(createProcSql);

            var createProcSql2 = @"CREATE OR ALTER PROCEDURE [dbo].[PortfolioRunningTotalByMonth]
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


	;WITH PricesPerDayCTE AS
	(
		SELECT
			CAST(Sub.DateRecorded AS DATE) AS PriceDate,
			Sub.CryptoId,
			Sub.Price
		FROM 
		(
			SELECT 
				CP.DateRecorded,
				CP.CryptoId,
				CP.Price,
				ROW_NUMBER() OVER (PARTITION BY CP.CryptoId,  DatePart(Year,CP.DateRecorded), DatePart(Month,CP.DateRecorded) ORDER BY CP.DateRecorded DESC) AS RowNum
			FROM CryptoPrices CP
		) AS Sub
		WHERE Sub.RowNum = 1
	)
	, InvestmentRunningTotalsCTE AS (
		SELECT 
			  P.PriceDate
			 ,P.CryptoId
			 ,P.Price AS PriceForDay
			 ,SUM(T.Quantity) AS Quantity
			 ,SUM(T.InvestmentTotal) AS InvestmentTotal
			 ,SUM(T.Fee) Fee
			 ,SUM(T.InvestmentTotal) / SUM(T.Quantity) AverageBuyPrice
		FROM
		  PricesPerDayCTE P
		  LEFT JOIN Transactions T ON P.CryptoId = T.CryptoId AND T.TransactionDate <= P.PriceDate
		  INNER JOIN dbo.ExchangeTransactionTypes ETT ON ETT.ExchangeTransactionTypeId = T.ExchangeTransactionTypeId
		  WHERE ETT.TransactionTypeId IN (SELECT TransactionTypeId FROM @TransactionTypes)
		  AND T.AppUserId = @AppUserId
		GROUP BY
		  P.PriceDate, P.CryptoId, P.Price

	)
	, UnrealizedProfitCTE AS (
		SELECT
			 C.CryptoId
			,C.CryptoName
			,C.TickerSymbol
			,T.PriceDate
			,DATEPART(YEAR,T.PriceDate) AS PriceYear
			,DATEPART(MONTH,T.PriceDate) AS PriceMonth
			,T.Quantity
			,T.InvestmentTotal
			,T.Fee
			,T.AverageBuyPrice
			,(T.Quantity * ISNULL(T.PriceForDay,0.00)) - (T.Quantity * T.AverageBuyPrice) - T.Fee AS UnrealizedProfit
		FROM InvestmentRunningTotalsCTE T
		INNER JOIN dbo.Cryptos C ON C.CryptoId = T.CryptoId

	)
		SELECT
			 CONCAT(P.PriceYear , '-' , LEFT(DATENAME(MONTH, DATEFROMPARTS(2000, P.PriceMonth, 1)), 3)) AS RunningTotalInterval
			,SUM(CAST(P.InvestmentTotal AS decimal(10,3))) AS InvestmentRunningTotal
			,CAST(SUM(P.InvestmentTotal) + SUM(UnrealizedProfit) AS decimal(10,3)) as CurrentWorthRunningTotal
		FROM UnrealizedProfitCTE P
		GROUP BY P.PriceYear, P.PriceMonth
		ORDER BY
		  P.PriceYear, P.PriceMonth;

END
";


			            migrationBuilder.Sql(createProcSql2);



			var createProcSql3 = @"CREATE OR ALTER PROCEDURE [dbo].[PortfolioRunningTotalByDay]
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

	;WITH PricesPerDayCTE AS
	(
		SELECT
			CAST(Sub.DateRecorded AS DATE) AS PriceDate,
			Sub.CryptoId,
			Sub.Price
		FROM 
		(
			SELECT 
				CP.DateRecorded,
				CP.CryptoId,
				CP.Price,
				ROW_NUMBER() OVER (PARTITION BY CP.CryptoId, CAST(CP.DateRecorded AS DATE) ORDER BY CP.DateRecorded DESC) AS RowNum
			FROM CryptoPrices CP
		) AS Sub
		WHERE Sub.RowNum = 1
	)
	, InvestmentRunningTotalsCTE AS (
		SELECT 
			  P.PriceDate
			 ,P.CryptoId
			 ,P.Price AS PriceForDay
			 ,SUM(T.Quantity) AS Quantity
			 ,SUM(T.InvestmentTotal) AS InvestmentTotal
			 ,SUM(T.Fee) Fee
			 ,SUM(T.InvestmentTotal) / SUM(T.Quantity) AverageBuyPrice
		FROM
		  PricesPerDayCTE P
		  LEFT JOIN Transactions T ON P.CryptoId = T.CryptoId AND T.TransactionDate <= P.PriceDate
		  INNER JOIN dbo.ExchangeTransactionTypes ETT ON ETT.ExchangeTransactionTypeId = T.ExchangeTransactionTypeId
		  WHERE ETT.TransactionTypeId IN (SELECT TransactionTypeId FROM @TransactionTypes)
		  AND T.AppUserId = @AppUserId
		GROUP BY
		  P.PriceDate, P.CryptoId, P.Price

	)
	, UnrealizedProfitCTE AS (
		SELECT
			 C.CryptoId
			,C.CryptoName
			,C.TickerSymbol
			,T.PriceDate
			,T.Quantity
			,T.InvestmentTotal
			,T.Fee
			,T.AverageBuyPrice
			,(T.Quantity * ISNULL(T.PriceForDay,0.00)) - (T.Quantity * T.AverageBuyPrice) - T.Fee AS UnrealizedProfit
		FROM InvestmentRunningTotalsCTE T
		INNER JOIN dbo.Cryptos C ON C.CryptoId = T.CryptoId

	)
		SELECT
			 P.PriceDate AS RunningTotalInterval
			,SUM(CAST(P.InvestmentTotal AS decimal(10,3))) AS InvestmentRunningTotal
			,CAST(SUM(P.InvestmentTotal) + SUM(UnrealizedProfit) AS decimal(10,3)) as CurrentWorthRunningTotal
		FROM UnrealizedProfitCTE P
		GROUP BY P.PriceDate
		ORDER BY
		  P.PriceDate;
END";


            migrationBuilder.Sql(createProcSql3);

        }





        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var createProcSql = @"CREATE OR ALTER PROCEDURE [dbo].[PortfolioRunningTotalGetAll]
AS
BEGIN

	SET NOCOUNT ON;

	;WITH PricesPerDayCTE AS
	(
		SELECT
			CAST(Sub.DateRecorded AS DATE) AS PriceDate,
			Sub.CryptoId,
			Sub.Price
		FROM 
		(
			SELECT 
				CP.DateRecorded,
				CP.CryptoId,
				CP.Price,
				ROW_NUMBER() OVER (PARTITION BY CP.CryptoId, CAST(CP.DateRecorded AS DATE) ORDER BY CP.DateRecorded DESC) AS RowNum
			FROM CryptoPrices CP
		) AS Sub
		WHERE Sub.RowNum = 1
	)
	, InvestmentRunningTotalsCTE AS (
		SELECT 
			  P.PriceDate
			 ,P.CryptoId
			 ,P.Price AS PriceForDay
			 ,SUM(T.Quantity) AS Quantity
			 ,SUM(T.InvestmentTotal) AS InvestmentTotal
			 ,SUM(T.Fee) Fee
			 ,SUM(T.InvestmentTotal) / SUM(T.Quantity) AverageBuyPrice
		FROM
		  PricesPerDayCTE P
		  LEFT JOIN Transactions T ON P.CryptoId = T.CryptoId AND T.TransactionDate <= P.PriceDate
		GROUP BY
		  P.PriceDate, P.CryptoId, P.Price

	)
	, UnrealizedProfitCTE AS (
		SELECT
			 C.CryptoId
			,C.CryptoName
			,C.TickerSymbol
			,T.PriceDate
			,T.Quantity
			,T.InvestmentTotal
			,T.Fee
			,T.AverageBuyPrice
			,(T.Quantity * ISNULL(T.PriceForDay,0.00)) - (T.Quantity * T.AverageBuyPrice) - T.Fee AS UnrealizedProfit
		FROM InvestmentRunningTotalsCTE T
		INNER JOIN dbo.Cryptos C ON C.CryptoId = T.CryptoId

	)
		SELECT
			 P.PriceDate
			,SUM(CAST(P.InvestmentTotal AS decimal(10,3))) AS InvestmentRunningTotal
			,CAST(SUM(P.InvestmentTotal) + SUM(UnrealizedProfit) AS decimal(10,3)) as CurrentWorthRunningTotal
		FROM UnrealizedProfitCTE P
		GROUP BY P.PriceDate
		ORDER BY
		  P.PriceDate;

END";
            migrationBuilder.Sql(createProcSql);


            var updateProcSql = @"CREATE OR ALTER PROCEDURE [dbo].[PortfolioSummaryGetAll]
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
			 sub.CryptoPriceId
			,sub.CryptoId
			,sub.DateRecorded
			,sub.Price
			,sub.RowNum
		FROM (
			SELECT 
			 	 CP.CryptoPriceId
				,CP.CryptoId
				,CP.DateRecorded
				,CP.Price
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
		LEFT JOIN cteCryptoPrices CP ON CP.CryptoId = C.CryptoId 
	)
	SELECT
		P.CryptoId
	   ,P.CryptoName
	   ,P.TickerSymbol
	   ,P.NumberOfOrders
	   ,CAST(P.Quantity AS decimal(15,4)) AS Quantity
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
END
";
            migrationBuilder.Sql(updateProcSql);
        }
    }
}
