using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AltFuture.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class CreateStoredProcs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var createPortfolioSummary = @"CREATE OR ALTER PROCEDURE [dbo].[PortfolioSummary]
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

            var createPortfolioRunningTotalByMonth = @"CREATE OR ALTER PROCEDURE [dbo].[PortfolioRunningTotalByMonth]
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
				CONCAT(LEFT(DATENAME(MONTH, DATEFROMPARTS(2000, P.PriceMonth, 1)), 3) , ' ' , P.PriceYear) AS RunningTotalInterval
			,SUM(CAST(P.InvestmentTotal AS decimal(10,3))) AS InvestmentRunningTotal
			,CAST(SUM(P.InvestmentTotal) + SUM(UnrealizedProfit) AS decimal(10,3)) as CurrentWorthRunningTotal
		FROM UnrealizedProfitCTE P
		GROUP BY P.PriceYear, P.PriceMonth
		ORDER BY
			P.PriceYear, P.PriceMonth;

END 
";

            var createPortfolioRunningTotalByDay = @"CREATE OR ALTER PROCEDURE [dbo].[PortfolioRunningTotalByDay]
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
END

";

            var createSeedCryptoPricesByMonth = @"CREATE OR ALTER PROCEDURE [dbo].[SeedCryptoPricesByMonth]
AS
BEGIN


	--* 9/2021
	If NOT EXISTS(SELECT CryptoPriceId FROM CryptoPrices WHERE DateRecorded = '2021-09-05')
	BEGIN
		INSERT INTO CryptoPrices ( CryptoId, DateRecorded, IsMonthStartPrice, Price, MarketCap)
		Values
		(1, '2021-09-05',1,51753.41,97354882472.78),
		(2, '2021-09-05',1,2.9108,93187277515.33),
		(3, '2021-09-05',1,3952.13,463997267858.18),
		(4, '2021-09-05',1,504.62,84845287773.55),
		(5, '2021-09-05',1,0.185,4673720705.75),
		(6, '2021-09-05',1,0.00000756,2984980426.77),
		(7, '2021-09-05',1,1.6795,11104723325.82),
		(8, '2021-09-05',1,82.31,5013136844.14),
		(9, '2021-09-05',1,1.0776,1934732954.64),
		(10,'2021-09-05',1,50.10,1672955685.35),
		(11,'2021-09-05',1,48.18,10595363408.10),
		(12,'2021-09-05',1,34.41,33981408465.06),
		(13,'2021-09-05',1,0.9996,2779709080.19)
	END;

	--* 10/2021
	If NOT EXISTS(SELECT CryptoPriceId FROM CryptoPrices WHERE DateRecorded = '2021-10-03')
	BEGIN
		INSERT INTO CryptoPrices ( CryptoId, DateRecorded, IsMonthStartPrice, Price, MarketCap)
		Values
		(1, '2021-10-03',1,48199.95,907791525235.93),
		(2, '2021-10-03',1,2.2529,72177771731.76),
		(3, '2021-10-03',1,3418.36,402619122253.94),
		(4, '2021-10-03',1,430.51,72384475441.52),
		(5, '2021-10-03',1,0.1824,4608945841.41),
		(6, '2021-10-03',1,0.00000854,3372010281.69),
		(7, '2021-10-03',1,1.3254,8843328492.30),
		(8, '2021-10-03',1,138.32,8424612358.11),
		(9, '2021-10-03',1,0.7653,1390482900.20),
		(10,'2021-10-03',1,57.51,1920669039.07),
		(11,'2021-10-03',1,68.72,15137441435.11),
		(12,'2021-10-03',1,32.11,31706484564.47),
		(13,'2021-10-03',1,0.9997,32215973731.74)
	END;

	--* 11/2021
	If NOT EXISTS(SELECT CryptoPriceId FROM CryptoPrices WHERE DateRecorded = '2021-11-07')
	BEGIN
		INSERT INTO CryptoPrices ( CryptoId, DateRecorded, IsMonthStartPrice, Price, MarketCap)
		Values
		(1, '2021-11-07',1,63326.99,1194778776481.15),
		(2, '2021-11-07',1,2.0219,67297292605.33),
		(3, '2021-11-07',1,4620.55,546381671971.18),
		(4, '2021-11-07',1,650.45,108496484780.49),
		(5, '2021-11-07',1,0.3541,8944455877.25),
		(6, '2021-11-07',1,0.00005689,31236713863.56),
		(7, '2021-11-07',1,1.8841,12853999197.07),
		(8, '2021-11-07',1,160.36,9767082277.58),
		(9, '2021-11-07',1,2.8238,5130639236.81),
		(10,'2021-11-07',1,76.20,2544814439.26),
		(11,'2021-11-07',1,87.71,19320327631.81),
		(12,'2021-11-07',1,52.28,51628651803.81),
		(13,'2021-11-07',1,1.0006,34343466491.13)
	END;

	--* 12/2021
	If NOT EXISTS(SELECT CryptoPriceId FROM CryptoPrices WHERE DateRecorded = '2021-12-05')
	BEGIN
		INSERT INTO CryptoPrices ( CryptoId, DateRecorded, IsMonthStartPrice, Price, MarketCap)
		Values
		(1, '2021-12-05',1,49368.85,932695365142.40),
		(2, '2021-12-05',1,1.3781,5909141522.15),
		(3, '2021-12-05',1,4198.32,498007875686.39),
		(4, '2021-12-05',1,557.78,93038857104.01),
		(5, '2021-12-05',1,0.5639,14246995016.60),
		(6, '2021-12-05',1,0.00003622,19887113761.12),
		(7, '2021-12-05',1,2.0456,14363566604.26),
		(8, '2021-12-05',1,107.37,6539453524.72),
		(9, '2021-12-05',1,3.7021,6754846153.70),
		(10,'2021-12-05',1,44.85,1497844104.41),
		(11,'2021-12-05',1,85.79,20824940381.42),
		(12,'2021-12-05',1,28.31,27958507597.69),
		(13,'2021-12-05',1,0.9987,40884696965.55)
	END;

	--* 01/2022
	If NOT EXISTS(SELECT CryptoPriceId FROM CryptoPrices WHERE DateRecorded = '2022-01-02')
	BEGIN
		INSERT INTO CryptoPrices ( CryptoId, DateRecorded, IsMonthStartPrice, Price, MarketCap)
		Values
		(1, '2022-01-02',1,47345.22,895688387523.14),
		(2, '2022-01-02',1,1.3776,46129061735.77),
		(3, '2022-01-02',1,3829.57,455713570380.85),
		(4, '2022-01-02',1,531.40,88637570485.46),
		(5, '2022-01-02',1,0.5877,14847022636.66),
		(6, '2022-01-02',1,0.00003404,18692252748.29),
		(7, '2022-01-02',1,2.5492,18259576689.35),
		(8, '2022-01-02',1,96.51,5878248214.72),
		(9, '2022-01-02',1,3.3399,6093730774.27),
		(10,'2022-01-02',1,60.11,2007281975.96),
		(11,'2022-01-02',1,113.19,27588210907.87),
		(12,'2022-01-02',1,29.73,29361884232.15),
		(13,'2022-01-02',1,0.9998,42562534941.23)
	END;

	--* 02/2022
	If NOT EXISTS(SELECT CryptoPriceId FROM CryptoPrices WHERE DateRecorded = '2022-02-06')
	BEGIN
		INSERT INTO CryptoPrices ( CryptoId, DateRecorded, IsMonthStartPrice, Price, MarketCap)
		Values
		(1, '2022-02-06',1,42412.43,803744511638.35),
		(2, '2022-02-06',1,1.1445,38433324420.14),
		(3, '2022-02-06',1,3057.48,365288302066.81),
		(4, '2022-02-06',1,419.55,69274003460.63),
		(5, '2022-02-06',1,0.4616,11661744341.53),
		(6, '2022-02-06',1,0.00002841,15596217120.71),
		(7, '2022-02-06',1,1.7400,12981893615.00),
		(8, '2022-02-06',1,68.42,4167356867.44),
		(9, '2022-02-06',1,3.3008,6047605202.47),
		(10,'2022-02-06',1,38.03,1269850918.56),
		(11,'2022-02-06',1,78.76,19306314558.70),
		(12,'2022-02-06',1,21.90,21631280299.89),
		(13,'2022-02-06',1,1.0004,51054224335.10)
	END;
	

	--* 03/2022
	If NOT EXISTS(SELECT CryptoPriceId FROM CryptoPrices WHERE DateRecorded = '2022-03-06')
	BEGIN	
		INSERT INTO CryptoPrices ( CryptoId, DateRecorded, IsMonthStartPrice, Price, MarketCap)
		Values
		(1, '2022-03-06',1,38419.98,729062407067.32),
		(2, '2022-03-06',1,0.8238,27739380365.39),
		(3, '2022-03-06',1,2555.04,306229430306.07),
		(4, '2022-03-06',1,375.01,61920633562.54),
		(5, '2022-03-06',1,0.3915,9891083820.35),
		(6, '2022-03-06',1,0.00002354,12922909175.38),
		(7, '2022-03-06',1,1.4367,10914235773.67),
		(8, '2022-03-06',1,48.86,2975883653.27),
		(9, '2022-03-06',1,2.4185,4430835840.09),
		(10,'2022-03-06',1,28.20,941818384.56),
		(11,'2022-03-06',1,72.53,19267144791.27),
		(12,'2022-03-06',1,16.98,16767936113.35),
		(13,'2022-03-06',1,0.9999,52838496192.22)
	END;

	--* 04/2022
	If NOT EXISTS(SELECT CryptoPriceId FROM CryptoPrices WHERE DateRecorded = '2022-04-03')
	BEGIN
		INSERT INTO CryptoPrices ( CryptoId, DateRecorded, IsMonthStartPrice, Price, MarketCap)
		Values
		(1, '2022-04-03',1,46453.57,882703992270.52),
		(2, '2022-04-03',1,1.1859,40012506060.12),
		(3, '2022-04-03',1,3522.83,423557081705.00),
		(4, '2022-04-03',1,450.35,74360146489.04),
		(5, '2022-04-03',1,0.4801,12129220476.96),
		(6, '2022-04-03',1,0.00002679,14709211244.44),
		(7, '2022-04-03',1,1.6888,13102616373.19),
		(8, '2022-04-03',1,66.47,4048404435.54),
		(9, '2022-04-03',1,2.7224,5011489673.16),
		(10,'2022-04-03',1,40.90,1365802079.89),
		(11,'2022-04-03',1,98.08,26224674828.73),
		(12,'2022-04-03',1,23.21,22919985030.75),
		(13,'2022-04-03',1,1.0001,51633984578.70)
	END;

	--* 05/2022
	If NOT EXISTS(SELECT CryptoPriceId FROM CryptoPrices WHERE DateRecorded = '2022-05-01')
	BEGIN
		INSERT INTO CryptoPrices ( CryptoId, DateRecorded, IsMonthStartPrice, Price, MarketCap)
		Values
		(1, '2022-05-01',1,38469.09,731986764311.88),
		(2, '2022-05-01',1,0.7903,26676154076.97),
		(3, '2022-05-01',1,2827.76,341049885442.04),
		(4, '2022-05-01',1,390.28,63724060207.07),
		(5, '2022-05-01',1,0.3135,7918783506.17),
		(6, '2022-05-01',1,0.00002158,11848512411.93),
		(7, '2022-05-01',1,1.1012,8642885732.64),
		(8, '2022-05-01',1,31.82,1938280828.45),
		(9, '2022-05-01',1,1.5334,2825247651.61),
		(10,'2022-05-01',1,25.27,843845991.03),
		(11,'2022-05-01',1,58.86,15810356887.26),
		(12,'2022-05-01',1,15.38,15190677414.53),
		(13,'2022-05-01',1,1.0004,49298296906.65)
	END;


	--* 06/2022
	If NOT EXISTS(SELECT CryptoPriceId FROM CryptoPrices WHERE DateRecorded = '2022-06-05')
	BEGIN
		INSERT INTO CryptoPrices ( CryptoId, DateRecorded, IsMonthStartPrice, Price, MarketCap)
		Values
		(1, '2022-06-05',1,29906.66,569996838236.87),
		(2, '2022-06-05',1,0.5671,19133051325.98),
		(3, '2022-06-05',1,1805.20,218544227813.81),
		(4, '2022-06-05',1,298.93,48809157475.70),
		(5, '2022-06-05',1,0.1789,4518806273.75),
		(6, '2022-06-05',1,0.00001081,5936850198.71),
		(7, '2022-06-05',1,0.5979,4757398372.57),
		(8, '2022-06-05',1,20.02,1276185344.54),
		(9, '2022-06-05',1,0.9766,1806034055.60),
		(10,'2022-06-05',1,12.69,423721733.46),
		(11,'2022-06-05',1,24.16,6785877568.63),
		(12,'2022-06-05',1,9.3450,9228925960.86),
		(13,'2022-06-05',1,1.0001,54094982708.31)
	END;

	--* 07/2022
	If NOT EXISTS(SELECT CryptoPriceId FROM CryptoPrices WHERE DateRecorded = '2022-07-03')
	BEGIN
		INSERT INTO CryptoPrices ( CryptoId, DateRecorded, IsMonthStartPrice, Price, MarketCap)
		Values
		(1, '2022-07-03',1,19297.08,368272636367.55),
		(2, '2022-07-03',1,0.4556,15377222959.95),
		(3, '2022-07-03',1,1073.77,130354920626.22),
		(4, '2022-07-03',1,218.98,35754443110.53),
		(5, '2022-07-03',1,0.1133,2861761183.54),
		(6, '2022-07-03',1,0.00001004,5512757228.87),
		(7, '2022-07-03',1,0.4606,3688244673.68),
		(8, '2022-07-03',1,14.18,1169753613.69),
		(9, '2022-07-03',1,0.8377,1549025789.40),
		(10,'2022-07-03',1,11.64,388599710.68),
		(11,'2022-07-03',1,16.66,4707781238.09),
		(12,'2022-07-03',1,6.8438,6758841572.72),
		(13,'2022-07-03',1,1.0004,55823349220.82)
	END;


	--* 08/2022
	If NOT EXISTS(SELECT CryptoPriceId FROM CryptoPrices WHERE DateRecorded = '2022-08-07')
	BEGIN
		INSERT INTO CryptoPrices ( CryptoId, DateRecorded, IsMonthStartPrice, Price, MarketCap)
		Values
		(1, '2022-08-07',1,23175.89,443014963855.39),
		(2, '2022-08-07',1,0.5275,17796329481.18 ),
		(3, '2022-08-07',1,1699.35,207103611495.11 ),
		(4, '2022-08-07',1,322.92,52099211446.08 ),
		(5, '2022-08-07',1,0.1466,3704236298.18 ),
		(6, '2022-08-07',1,0.00001203,6607752112.39),
		(7, '2022-08-07',1,0.9067,7285073636.08 ),
		(8, '2022-08-07',1,18.48,1537429682.15 ),
		(9, '2022-08-07',1,1.0596,1962763409.27 ),
		(10,'2022-08-07',1,14.63,488634796.26 ),
		(11,'2022-08-07',1,27.89,7933884703.30 ),
		(12,'2022-08-07',1,8.6464,9552753760.30 ),
		(13,'2022-08-07',1,0.9999,54275863425.54 )
	END;

	--* 09/2022
	If NOT EXISTS(SELECT CryptoPriceId FROM CryptoPrices WHERE DateRecorded = '2022-09-04')
	BEGIN
		INSERT INTO CryptoPrices ( CryptoId, DateRecorded, IsMonthStartPrice, Price, MarketCap)
		Values
		(1, '2022-09-04',1,19986.71,382576542382.12),
		(2, '2022-09-04',1,0.5031,17196438526.17),
		(3, '2022-09-04',1,1577.64,192853381586.06),
		(4, '2022-09-04',1,278.84,44987580579.29),
		(5, '2022-09-04',1,0.1201,3034526688.56),
		(6, '2022-09-04',1,0.00001283,7043373460.54),
		(7, '2022-09-04',1,0.8942,7763819651.46),
		(8, '2022-09-04',1,14.55,1217126301.97),
		(9, '2022-09-04',1,0.8082,1497131479.19),
		(10,'2022-09-04',1,10.26,342590535.15),
		(11,'2022-09-04',1,19.01,5596931299.35),
		(12,'2022-09-04',1,7.3704,8207842547.61),
		(13,'2022-09-04',1,1.0001,51881772221.08)
	END;		

	--* 10/2022
	If NOT EXISTS(SELECT CryptoPriceId FROM CryptoPrices WHERE DateRecorded = '2022-10-02')
	BEGIN
		INSERT INTO CryptoPrices ( CryptoId, DateRecorded, IsMonthStartPrice, Price, MarketCap)
		Values
		(1, '2022-10-02',1,19044.11,365024821953.69),
		(2, '2022-10-02',1,0.4196,14373740433.11),
		(3, '2022-10-02',1,1276.09,156481992824.69),
		(4, '2022-10-02',1,284.43,45889655979.06),
		(5, '2022-10-02',1,0.1095,2766095858.82),
		(6, '2022-10-02',1,0.00001094,6009131509.30),
		(7, '2022-10-02',1,0.7643,6675209425.46),
		(8, '2022-10-02',1,12.15,1008881983.23),
		(9, '2022-10-02',1,0.6829,1266785585.24),
		(10,'2022-10-02',1,9.0519,302285657.03),
		(11,'2022-10-02',1,16.53,4894804403.14),
		(12,'2022-10-02',1,6.1633,6952768096.08),
		(13,'2022-10-02',1,0.9999,47325233841.02)
	END;

	--* 11/2022
	If NOT EXISTS(SELECT CryptoPriceId FROM CryptoPrices WHERE DateRecorded = '2022-11-06')
	BEGIN
		INSERT INTO CryptoPrices ( CryptoId, DateRecorded, IsMonthStartPrice, Price, MarketCap)
		Values
		(1, '2022-11-06',1,20926.49,401792318391.33),
		(2, '2022-11-06',1,0.403,13840734879.51),
		(3, '2022-11-06',1,1572.23,192400441552.21),
		(4, '2022-11-06',1,339.39,54294354064.46),
		(5, '2022-11-06',1,0.1183,2989418474.70),
		(6, '2022-11-06',1,0.00001183,6496405100.98),
		(7, '2022-11-06',1,1.1380,9939375600.36),
		(8, '2022-11-06',1,9.8289,946643043.65),
		(9, '2022-11-06',1,0.6635,1230807499.60),
		(10,'2022-11-06',1,14.22,474779841.35),
		(11,'2022-11-06',1,18.28,5469964214.63),
		(12,'2022-11-06',1,6.8209,7848721049.44),
		(13,'2022-11-06',1,0.9999,42187827667.80)
	END;

	--* 12/2022
	If NOT EXISTS(SELECT CryptoPriceId FROM CryptoPrices WHERE DateRecorded = '2022-12-04')
	BEGIN
		INSERT INTO CryptoPrices ( CryptoId, DateRecorded, IsMonthStartPrice, Price, MarketCap)
		Values
		(1, '2022-12-04',1,17130.49,329323845088.74),
		(2, '2022-12-04',1,0.3227,11119224610.72 ),
		(3, '2022-12-04',1,1280.26,156669949121.35 ),
		(4, '2022-12-04',1,292.29,46757282793.45 ),
		(5, '2022-12-04',1,0.06478,1636661265.23 ),
		(6, '2022-12-04',1,0.00000936,5140995141.76),
		(7, '2022-12-04',1,0.9221,8054081647.03 ),
		(8, '2022-12-04',1,6.8888,687878723.45 ),
		(9, '2022-12-04',1,0.4102,761028407.31 ),
		(10,'2022-12-04',1,9.3146,311058888.61 ),
		(11,'2022-12-04',1,13.93,4319131735.52 ),
		(12,'2022-12-04',1,5.5957,6388210650.93 ),
		(13,'2022-12-04',1,1.0000,43408651648.57 )
	END;

	--* 01/2023
	If NOT EXISTS(SELECT CryptoPriceId FROM CryptoPrices WHERE DateRecorded = '2023-01-01')
	BEGIN
		INSERT INTO CryptoPrices ( CryptoId, DateRecorded, IsMonthStartPrice, Price, MarketCap)
		Values
		(1, '2023-01-01',1,16625.08,320025834692.40),
		(2, '2023-01-01',1,0.2498,8621727158.32),
		(3, '2023-01-01',1,1200.96,146966709631.13),
		(4, '2023-01-01',1,244.14,39053263491.06),
		(5, '2023-01-01',1,0.05683,1435710780.75),
		(6, '2023-01-01',1,0.00000812,4460494919.94),
		(7, '2023-01-01',1,0.76,6637843574.08),
		(8, '2023-01-01',1,6.3139,632185620.11),
		(9, '2023-01-01',1,0.3014,559156404.41),
		(10,'2023-01-01',1,6.3592,212365236.60),
		(11,'2023-01-01',1,10.87,3385625121.75),
		(12,'2023-01-01',1,4.3708,5035576982.99),
		(13,'2023-01-01',1,0.9999,44584840756.22)
	END;

	--* 02/2023
	If NOT EXISTS(SELECT CryptoPriceId FROM CryptoPrices WHERE DateRecorded = '2023-02-05')
	BEGIN
		INSERT INTO CryptoPrices ( CryptoId, DateRecorded, IsMonthStartPrice, Price, MarketCap)
		Values
		(1, '2023-02-05',1,22955.67,442642331535.60),
		(2, '2023-02-05',1,0.3925,13583144197.66),
		(3, '2023-02-05',1,1631.65,199670814561.87),
		(4, '2023-02-05',1,327.87,51770723358.62),
		(5, '2023-02-05',1,0.08079,2040886048.37),
		(6, '2023-02-05',1,0.00001435,7879341633.77),
		(7, '2023-02-05',1,1.2045,10520492555.05),
		(8, '2023-02-05',1,10.91,1103705529.31),
		(9, '2023-02-05',1,0.7369,1367028224.00),
		(10,'2023-02-05',1,11.53,384907194.24),
		(11,'2023-02-05',1,20.11,6335560558.99),
		(12,'2023-02-05',1,6.6447,7669500078.57),
		(13,'2023-02-05',1,0.9999,42031783646.39)
	END;



	--* 03/2023
	If NOT EXISTS(SELECT CryptoPriceId FROM CryptoPrices WHERE DateRecorded = '2023-03-05')
	BEGIN
		INSERT INTO CryptoPrices ( CryptoId, DateRecorded, IsMonthStartPrice, Price, MarketCap)
		Values
		(1, '2023-03-05',1,22435.51,433216160786.44),
		(2, '2023-03-05',1,0.3371,11691782766.97),
		(3, '2023-03-05',1,1564.47,191450085930.19),
		(4, '2023-03-05',1,288.70,45583995083.98),
		(5, '2023-03-05',1,0.07356,1858248147.45),
		(6, '2023-03-05',1,0.00001111,6100188949.78),
		(7, '2023-03-05',1,1.1375,9934851222.82),
		(8, '2023-03-05',1,8.7808,999285644.38),
		(9, '2023-03-05',1,0.5769,1070276505.60),
		(10,'2023-03-05',1,8.7170,291103231.44),
		(11,'2023-03-05',1,16.16,5252884573.10),
		(12,'2023-03-05',1,5.9801,6955830776.74),
		(13,'2023-03-05',1,1.0000,43980818759.52)
	END;

	--* 04/2023
	If NOT EXISTS(SELECT CryptoPriceId FROM CryptoPrices WHERE DateRecorded = '2023-04-02')
	BEGIN
		INSERT INTO CryptoPrices ( CryptoId, DateRecorded, IsMonthStartPrice, Price, MarketCap)
		Values
		(1, '2023-04-02',1,28199.31,545245950698.04),
		(2, '2023-04-02',1,0.3818,13263150756.35),
		(3, '2023-04-02',1,1795.71,216307003100.75),
		(4, '2023-04-02',1,313.93,49565588033.19),
		(5, '2023-04-02',1,0.06765,1709048845.48),
		(6, '2023-04-02',1,0.00001081,6374221162.81),		
		(7, '2023-04-02',1,1.0965,9956789008.66),
		(8, '2023-04-02',1,8.3060,963069399.69),
		(9, '2023-04-02',1,0.6014,1115579114.28),
		(10,'2023-04-02',1,8.3056,277362512.44),
		(11,'2023-04-02',1,17.26,5626871630.20),
		(12,'2023-04-02',1,6.2672,7348649018.54),
		(13,'2023-04-02',1,0.9997,32531061019.39)
	END;

END
";
            migrationBuilder.Sql(createPortfolioSummary);
            migrationBuilder.Sql(createPortfolioRunningTotalByMonth);
            migrationBuilder.Sql(createPortfolioRunningTotalByDay);
            migrationBuilder.Sql(createSeedCryptoPricesByMonth);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var dropPortfolioSummary = "DROP PROC dbo.PortfolioSummary";
            var dropPortfolioRunningTotalByMonth = "DROP PROC dbo.PortfolioRunningTotalByMonth";
            var dropPortfolioRunningTotalByDay = "DROP PROC dbo.PortfolioRunningTotalByDay";
            var dropSeedCryptoPricesByMonth = "DROP PROC dbo.SeedCryptoPricesByMonth";


            migrationBuilder.Sql(dropPortfolioSummary);
            migrationBuilder.Sql(dropPortfolioRunningTotalByMonth);
            migrationBuilder.Sql(dropPortfolioRunningTotalByDay);
            migrationBuilder.Sql(dropSeedCryptoPricesByMonth);
        }
    }
}
