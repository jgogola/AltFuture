IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AppUsers] (
    [AppUserId] int NOT NULL IDENTITY,
    [UserName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_AppUsers] PRIMARY KEY ([AppUserId])
);
GO

CREATE TABLE [Cryptos] (
    [CryptoId] int NOT NULL IDENTITY,
    [CryptoName] nvarchar(max) NOT NULL,
    [TickerSymbol] nvarchar(max) NOT NULL,
    [CryptoIcon] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Cryptos] PRIMARY KEY ([CryptoId])
);
GO

CREATE TABLE [DataImportTypes] (
    [DataImportTypeId] int NOT NULL IDENTITY,
    [DataImportTypeName] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_DataImportTypes] PRIMARY KEY ([DataImportTypeId])
);
GO

CREATE TABLE [Exchanges] (
    [ExchangeId] int NOT NULL IDENTITY,
    [ExchangeName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Exchanges] PRIMARY KEY ([ExchangeId])
);
GO

CREATE TABLE [TransactionTypes] (
    [TransactionTypeId] int NOT NULL IDENTITY,
    [TransactionTypeName] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_TransactionTypes] PRIMARY KEY ([TransactionTypeId])
);
GO

CREATE TABLE [CryptoPrices] (
    [CryptoPriceId] int NOT NULL IDENTITY,
    [DateRecorded] datetime2 NOT NULL,
    [CryptoId] int NOT NULL,
    [Price] decimal(30,20) NOT NULL,
    [Volume24h] decimal(25,10) NOT NULL,
    [VolumeChange24h] decimal(20,10) NOT NULL,
    [PercentChange1h] decimal(20,10) NOT NULL,
    [PercentChange24h] decimal(20,10) NOT NULL,
    [PercentChange7d] decimal(20,10) NOT NULL,
    [PercentChange30d] decimal(20,10) NOT NULL,
    [PercentChange60d] decimal(20,10) NOT NULL,
    [PercentChange90d] decimal(20,10) NOT NULL,
    [MarketCap] decimal(25,10) NOT NULL,
    [MarketCapDominance] decimal(25,10) NOT NULL,
    [IsMonthStartPrice] bit NOT NULL,
    CONSTRAINT [PK_CryptoPrices] PRIMARY KEY ([CryptoPriceId]),
    CONSTRAINT [FK_CryptoPrices_Cryptos_CryptoId] FOREIGN KEY ([CryptoId]) REFERENCES [Cryptos] ([CryptoId]) ON DELETE CASCADE
);
GO

CREATE TABLE [ExchangeTransactionTypes] (
    [ExchangeTransactionTypeId] int NOT NULL IDENTITY,
    [ExchangeTransactionTypeName] nvarchar(max) NOT NULL,
    [ExchangeId] int NOT NULL,
    [TransactionTypeId] int NOT NULL,
    [DataImportTypeId] int NOT NULL,
    CONSTRAINT [PK_ExchangeTransactionTypes] PRIMARY KEY ([ExchangeTransactionTypeId]),
    CONSTRAINT [FK_ExchangeTransactionTypes_DataImportTypes_DataImportTypeId] FOREIGN KEY ([DataImportTypeId]) REFERENCES [DataImportTypes] ([DataImportTypeId]) ON DELETE CASCADE,
    CONSTRAINT [FK_ExchangeTransactionTypes_Exchanges_ExchangeId] FOREIGN KEY ([ExchangeId]) REFERENCES [Exchanges] ([ExchangeId]) ON DELETE CASCADE,
    CONSTRAINT [FK_ExchangeTransactionTypes_TransactionTypes_TransactionTypeId] FOREIGN KEY ([TransactionTypeId]) REFERENCES [TransactionTypes] ([TransactionTypeId]) ON DELETE CASCADE
);
GO

CREATE TABLE [Transactions] (
    [TransactionId] int NOT NULL IDENTITY,
    [TransactionReferenceNum] int NOT NULL,
    [AppUserId] int NOT NULL,
    [CryptoId] int NOT NULL,
    [ExchangeTransactionTypeId] int NOT NULL,
    [Price] decimal(18,10) NOT NULL,
    [Quantity] decimal(18,10) NOT NULL,
    [InvestmentTotal] AS Price * Quantity,
    [Fee] decimal(18,10) NOT NULL,
    [TransactionTotal] decimal(18,10) NOT NULL,
    [TransactionDate] datetime2 NOT NULL,
    [FromExchangeId] int NOT NULL,
    [ToExchangeId] int NULL,
    [CreatedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Transactions] PRIMARY KEY ([TransactionId]),
    CONSTRAINT [FK_Transactions_AppUsers_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [AppUsers] ([AppUserId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Transactions_Cryptos_CryptoId] FOREIGN KEY ([CryptoId]) REFERENCES [Cryptos] ([CryptoId]),
    CONSTRAINT [FK_Transactions_ExchangeTransactionTypes_ExchangeTransactionTypeId] FOREIGN KEY ([ExchangeTransactionTypeId]) REFERENCES [ExchangeTransactionTypes] ([ExchangeTransactionTypeId]),
    CONSTRAINT [FK_Transactions_Exchanges_FromExchangeId] FOREIGN KEY ([FromExchangeId]) REFERENCES [Exchanges] ([ExchangeId]),
    CONSTRAINT [FK_Transactions_Exchanges_ToExchangeId] FOREIGN KEY ([ToExchangeId]) REFERENCES [Exchanges] ([ExchangeId])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CryptoId', N'CryptoIcon', N'CryptoName', N'TickerSymbol') AND [object_id] = OBJECT_ID(N'[Cryptos]'))
    SET IDENTITY_INSERT [Cryptos] ON;
INSERT INTO [Cryptos] ([CryptoId], [CryptoIcon], [CryptoName], [TickerSymbol])
VALUES (1, N'<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 32 32"><g fill="none" fill-rule="evenodd"><circle cx="16" cy="16" r="16" fill="#F7931A"/><path fill="#FFF" fill-rule="nonzero" d="M23.189 14.02c.314-2.096-1.283-3.223-3.465-3.975l.708-2.84-1.728-.43-.69 2.765c-.454-.114-.92-.22-1.385-.326l.695-2.783L15.596 6l-.708 2.839c-.376-.086-.746-.17-1.104-.26l.002-.009-2.384-.595-.46 1.846s1.283.294 1.256.312c.7.175.826.638.805 1.006l-.806 3.235c.048.012.11.03.18.057l-.183-.045-1.13 4.532c-.086.212-.303.531-.793.41.018.025-1.256-.313-1.256-.313l-.858 1.978 2.25.561c.418.105.828.215 1.231.318l-.715 2.872 1.727.43.708-2.84c.472.127.93.245 1.378.357l-.706 2.828 1.728.43.715-2.866c2.948.558 5.164.333 6.097-2.333.752-2.146-.037-3.385-1.588-4.192 1.13-.26 1.98-1.003 2.207-2.538zm-3.95 5.538c-.533 2.147-4.148.986-5.32.695l.95-3.805c1.172.293 4.929.872 4.37 3.11zm.535-5.569c-.487 1.953-3.495.96-4.47.717l.86-3.45c.975.243 4.118.696 3.61 2.733z"/></g></svg>', N'Bitcoin', N'BTC'),
(2, N'<svg width="32" height="32" viewBox="0 0 32 32" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink"><defs><filter x="-7.5%" y="-6%" width="115.5%" height="117.2%" filterUnits="objectBoundingBox" id="a"><feOffset dy=".5" in="SourceAlpha" result="shadowOffsetOuter1"/><feGaussianBlur stdDeviation=".5" in="shadowOffsetOuter1" result="shadowBlurOuter1"/><feColorMatrix values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0.204257246 0" in="shadowBlurOuter1"/></filter><path d="M15.725 6.06c.479-.247 1.064.324.81.795-.149.384-.71.486-.996.193-.303-.28-.204-.836.186-.989zm-5.155.546c.291-.118.66.144.63.457.03.338-.39.588-.687.427-.393-.15-.348-.778.057-.884zm10.558.893c-.455-.054-.527-.758-.09-.9.34-.162.652.143.702.46-.072.27-.302.518-.612.44zm-9.385 1.265c.487-.303 1.181.148 1.106.705-.025.561-.783.887-1.211.507-.414-.298-.351-.982.105-1.212zm7.43.322c.217-.55 1.097-.568 1.344-.032.245.417-.056.934-.491 1.076-.577.106-1.124-.508-.853-1.044zm-4.069 1.013c-.005-.474.433-.826.89-.859.304.06.634.187.764.488.243.416.027.987-.41 1.178-.2.11-.438.069-.656.056-.333-.16-.614-.477-.588-.863zm-7.666.69c.445-.27 1.045.22.876.696-.092.411-.654.578-.975.316-.343-.246-.289-.837.1-1.013zm16.462-.002c.377-.288 1 .043.954.511.026.427-.513.75-.887.53-.412-.183-.455-.807-.067-1.04zm-6.64.851c.622-.22 1.362.043 1.716.59.468.667.22 1.683-.507 2.066-.752.453-1.851.07-2.13-.758-.315-.74.145-1.666.92-1.898zm-3.653.073c.69-.32 1.619-.052 1.952.642.392.676.089 1.617-.612 1.966-.702.393-1.693.095-2.032-.63-.381-.702-.043-1.655.692-1.978zM9.95 12.94c.053-.437.472-.722.895-.752a.98.98 0 01.87.857c-.03.45-.383.888-.867.886-.533.045-1-.477-.898-.991zm10.802-.656c.547-.313 1.306.142 1.282.76.037.655-.803 1.116-1.347.732-.566-.32-.522-1.22.065-1.492zm-8.63 2.307c.638-.173 1.37.123 1.683.701.343.582.203 1.39-.33 1.818-.685.626-1.946.374-2.31-.48-.419-.783.09-1.833.956-2.04zm6.927-.003c.621-.175 1.351.06 1.685.617.442.637.231 1.588-.426 1.998-.69.477-1.756.227-2.136-.519-.46-.771.003-1.861.877-2.096zm-11.04.726c.552-.205 1.164.394.94.933-.136.49-.839.672-1.202.31-.425-.34-.268-1.095.262-1.243zm14.969.782a.836.836 0 01.788-.874c.378.06.746.36.716.765.035.535-.62.898-1.084.647-.217-.109-.328-.328-.42-.538zM5.294 15.58c.332-.143.743.14.667.503-.018.411-.635.57-.861.226-.2-.239-.08-.606.194-.73zm20.949-.009c.234-.163.61-.046.702.223.157.294-.131.696-.467.647-.472.042-.624-.665-.235-.87zm-12.317 1.973c.874-.223 1.814.494 1.82 1.38.056.895-.87 1.688-1.764 1.482-.692-.11-1.235-.766-1.212-1.453-.002-.658.502-1.27 1.156-1.409zm3.462-.001c.887-.244 1.855.486 1.841 1.392.047.878-.85 1.645-1.726 1.47-.825-.104-1.433-.995-1.203-1.783.116-.524.562-.95 1.088-1.08zm-6.676.545c.614-.103 1.19.57.941 1.144-.182.612-1.086.777-1.486.278-.468-.48-.118-1.356.545-1.422zm10.154.027c.548-.226 1.22.24 1.178.825.022.643-.808 1.087-1.343.711-.607-.337-.496-1.33.165-1.536zm2.838 2.8c-.214-.393.175-.914.62-.841.22-.004.375.167.516.311.029.233.078.511-.119.69-.267.333-.872.238-1.017-.16zm-16.268-.732c.415-.271 1.012.134.918.61-.05.423-.59.664-.945.424-.382-.217-.368-.836.027-1.034zm8.193.883c.543-.235 1.235.23 1.183.818.04.65-.815 1.1-1.346.71-.59-.335-.491-1.321.163-1.528zm-3.794.871c.462-.239 1.082.174 1.04.684.014.418-.4.774-.82.712-.347-.007-.573-.314-.685-.605.006-.317.139-.67.465-.79zm7.686.008c.476-.29 1.152.126 1.107.67.012.57-.752.934-1.195.56-.428-.293-.376-.997.088-1.23zm1.337 3.25c-.212-.314.037-.693.38-.765.277.055.57.26.511.574-.04.427-.674.557-.891.192zm-10.611-.273c.084-.25.288-.497.587-.432.435.03.564.676.183.875-.342.227-.74-.084-.77-.443zm5.12.287c.083-.37.568-.549.888-.353.212.09.274.322.328.52a8.822 8.822 0 00-.08.31c-.131.152-.3.305-.518.3-.405.047-.771-.404-.619-.777z" id="b"/></defs><g fill="none"><circle cx="16" cy="16" r="16" fill="#0D1E30"/><use fill="#000" filter="url(#a)" xlink:href="#b"/><use fill="#FFF" xlink:href="#b"/></g></svg>', N'Cardano', N'ADA'),
(3, N'<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 32 32"><g fill="none" fill-rule="evenodd"><circle cx="16" cy="16" r="16" fill="#627EEA"/><g fill="#FFF" fill-rule="nonzero"><path fill-opacity=".602" d="M16.498 4v8.87l7.497 3.35z"/><path d="M16.498 4L9 16.22l7.498-3.35z"/><path fill-opacity=".602" d="M16.498 21.968v6.027L24 17.616z"/><path d="M16.498 27.995v-6.028L9 17.616z"/><path fill-opacity=".2" d="M16.498 20.573l7.497-4.353-7.497-3.348z"/><path fill-opacity=".602" d="M9 16.22l7.498 4.353v-7.701z"/></g></g></svg>', N'Ethereum', N'ETH'),
(4, N'<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 32 32"><g fill="none"><circle cx="16" cy="16" r="16" fill="#F3BA2F"/><path fill="#FFF" d="M12.116 14.404L16 10.52l3.886 3.886 2.26-2.26L16 6l-6.144 6.144 2.26 2.26zM6 16l2.26-2.26L10.52 16l-2.26 2.26L6 16zm6.116 1.596L16 21.48l3.886-3.886 2.26 2.259L16 26l-6.144-6.144-.003-.003 2.263-2.257zM21.48 16l2.26-2.26L26 16l-2.26 2.26L21.48 16zm-3.188-.002h.002V16L16 18.294l-2.291-2.29-.004-.004.004-.003.401-.402.195-.195L16 13.706l2.293 2.293z"/></g></svg>', N'Bianance', N'BNB'),
(5, N'<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 32 32"><g fill="none" fill-rule="evenodd"><circle cx="16" cy="16" r="16" fill="#103F68"/><path fill="#FFF" fill-rule="nonzero" d="M15.98 5.018l9.52 5.483v11L15.991 27l-.077-.019-9.414-5.48v-11l9.414-5.483h.066zm-.031 1.138L7.5 11.076v9.85l8.448 4.919 1.032-.597 7.52-4.325v-9.845l-7.52-4.35-1.031-.572zm-7.14 10.61l2.501-1.87 2.211 1.412v2.54l1.673 1.612-.001.756-1.612 1.51H12.22l-3.41-5.96zm7.903 4.452l-.003-.76 1.667-1.61v-2.54l2.187-1.43 2.496 1.889-3.393 5.942h-1.344l-1.61-1.491zm-2.37-4.91l-.814-2.131h4.838l-.798 2.131.236 2.382-1.867.004-1.845.003.25-2.389zm1.595-2.715l-4.598-.002.855-3.82h7.464l.9 3.825-4.621-.003z"/></g></svg>', N'Cronos', N'CRO'),
(6, N'<svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px"	 viewBox="0 0 641.7 649" style="enable-background:new 0 0 641.7 649;" xml:space="preserve"><style type="text/css">	.st0{fill:#F00500;}	.st1{fill:#FFFFFF;}	.st2{fill:none;}	.st3{fill:#FFA409;}	.st4{fill:#FF9300;}	.st5{fill:#FF8300;}</style><g id="Group_938" transform="translate(-953.348 -232.003)">	<g id="Group_931">		<path id="Path_8573" class="st0" d="M1423.2,289.1c-16.7,16.7-32,34.8-45.6,54.1l-4.3-1.4c-24.8-8.1-50.5-13.1-76.4-14.8			c-7.8-0.6-36.9-0.6-46.4,0c-28.7,1.9-50.7,6.2-75.7,14.9c-1,0.4-1.9,0.7-2.8,1c-14.1-19.2-29.8-37.2-47.1-53.7			c83-42.5,180.1-47.6,267-14.1c9.3,3.5,18.4,7.6,27.3,12L1423.2,289.1z"/>		<path id="Path_8574" class="st0" d="M1578,668.4c-12.6,35.9-31.5,69.2-56,98.2c-13.3,15.6-28,29.9-43.8,42.9			c-37,29.8-80.2,51.1-126.3,62.3c-51,12.3-104.2,12.3-155.2,0c-46.2-11.2-89.3-32.5-126.3-62.3c-15.8-13-30.5-27.3-43.8-42.9			c-70.5-83.3-91.9-197.7-56-300.8c6.8-19.1,15.3-37.5,25.5-55c4.7-8,10.5-17,13.3-20.7c9.6,41.3,21.4,77.6,25.8,90.7			c-0.3,0.7-0.7,1.5-1.1,2.2c-23.3,47.2-35.9,92.7-38.9,141.1c0,0.6-0.1,1.3-0.1,1.9c-0.8,14.5-0.4,21.4,2,30.5			c7.5,28.5,30.6,59.9,66.8,90.7c59.3,50.4,139.7,86.8,200.6,91c63.5,4.3,157.2-32.8,219.6-87c9.8-8.7,19-17.9,27.6-27.7			c6.2-7.2,15.4-19.6,14.7-19.6c-0.2,0,0-0.2,0.4-0.4c0.3-0.2,0.6-0.5,0.4-0.7c-0.1-0.1,0.1-0.4,0.4-0.5c0.3-0.1,0.5-0.3,0.4-0.5			s0-0.4,0.4-0.5c0.3-0.1,0.4-0.4,0.3-0.6c-0.1-0.2,0-0.4,0.2-0.4c0.2,0,0.4-0.3,0.4-0.5s0.2-0.5,0.4-0.5c0.2,0,0.4-0.2,0.4-0.4			c0.3-0.8,0.8-1.6,1.3-2.3c1.5-2.3,7.5-13,8.3-14.8c5.7-12.9,9.3-24.4,11-35.9c0.8-5.5,1.3-15.6,0.9-18c-0.1-0.4-0.1-1.1-0.2-2.2			c-0.2-2-0.3-5-0.5-8c-0.2-4.5-0.6-10.8-0.9-13.9c-4.3-45.4-15.9-82.5-37.9-121.8c-0.9-1.5-1.7-3-2.2-4.2c-0.2-0.3-0.3-0.6-0.4-0.8			v0c3.1-9.3,15.8-48.2,25.9-92.7l0.2,0.2l1.5,2c2.6,3.5,9.5,13.9,12.6,18.9c14.1,23,25.1,47.7,32.9,73.5			C1600.7,541,1599.2,607.2,1578,668.4z"/>		<path id="Path_8575" d="M1448.2,548.9c-0.3,1.6-5,6.4-9.7,9.9c-13.8,10.1-38.5,19.5-63.6,24c-14.3,2.6-28.6,3-32.9,0.9			c-2.8-1.4-3.2-2.5-2.2-6.1c2.1-7.5,8.9-15.7,19.5-23.4c5.4-3.9,27.3-17.2,37.5-22.8c16.8-9.2,30.4-14.7,39.6-16			c2.9-0.4,6.4-0.5,7.5,0c1.8,0.7,3.8,6,4.6,12.4C1448.9,531.2,1448.7,546.2,1448.2,548.9z"/>		<path id="Path_8576" d="M1212.6,582c-0.6,1.2-3.6,2.5-7,3c-3.4,0.5-13.8,0.2-19.7-0.5c-21.2-2.8-43.7-9.3-60.1-17.3			c-9.2-4.5-15.8-9-20.7-13.9l-2.8-2.9l-0.3-3.8c-0.5-6.6-0.4-17,0.4-20.7c0.5-3.1,1.6-6.1,3.1-8.8c0.8-1,0.8-1,4.4-1			c4.4,0,8.1,0.7,14.1,2.6c12.3,3.8,30.6,13,52.9,26.4c18.4,11,25.3,16.4,30.6,23.8C1211.1,573.5,1213.5,580,1212.6,582z"/>		<path id="Path_8577" d="M1354.2,721.2c0,0.3-1.2,5.1-2.7,10.8c-1.5,5.7-2.7,10.3-2.7,10.5c-1.1,0.1-2.1,0.2-3.2,0.1h-3.2L1338,753			c-2.4,5.7-4.6,11-4.9,11.8l-0.6,1.4l-2.1-3.4l-2.1-3.4v-27.7l-0.8,0.2c-1.7,0.4-13.8,2-19.1,2.5c-21.9,2.2-43.9,1.7-65.7-1.4			c-3.5-0.5-6.5-0.9-6.6-0.8c-0.1,0.1,0,6.5,0.2,14.4l0.3,14.2l-1.7,2.6c-0.9,1.4-1.8,2.6-1.8,2.7c-0.3,0.3-1.3-1.3-3.6-5.9			c-2.5-4.8-4.3-9.9-5.6-15.2l-0.7-2.9l-3.1,0.2l-3.1,0.3l-0.8-3.7c-0.4-2-0.9-5.1-1.1-6.8l-0.3-3.2l-2.6-2.3			c-1.5-1.3-3-2.6-3.3-2.8c-0.5-0.4-0.7-1.1-0.7-1.7v-1.2l12.5,0.1l12.5,0.1l0.4,1.3l0.4,1.3l4.3,0.2c2.4,0.1,10,0.3,16.8,0.5			l12.5,0.3l3.2-5l3.3-5h4.2l-0.1-10.4l-0.1-10.4l-5.5-2.4c-17.7-7.8-28.1-16.7-32-27.5c-0.8-2.2-0.8-3-1-13			c-0.1-9.9-0.1-10.8,0.6-13c1.4-5,5.3-8.9,10.3-10.2c1.7-0.5,6.2-0.5,29.8-0.5l27.8,0.1l3.1,1.5c3.7,1.8,5.4,3.1,7.5,5.9			c2.4,3.2,3.1,5.7,3.1,11.8c0,8.7-0.6,16.2-1.5,19.2c-1.3,4.1-3.3,8-5.8,11.5c-5,6.4-14.1,12.8-22.4,15.7l-2.4,0.9l0.1,10.5			l0.1,10.5l2.2,0.2l2.2,0.2l3,4.5l2.9,4.5l13.4,0c7.4,0,13.9,0.1,14.5,0.2c1,0.2,1.2,0.1,2.2-1.6l1.2-1.8h11.5			C1351.6,720.7,1354.2,720.9,1354.2,721.2z"/>		<path id="Path_8578" d="M1301.4,749.9c-1.9,1.1-3.2,1.6-3.5,1.4c-0.3-0.1-1.7-1.3-3.2-2.4l-2.7-2.2l-2.8,3c-6.1,6.6-6.3,6.9-8.1,7			c-2.8,0.3-3.4-0.2-6.8-5.3c-1.8-2.6-3.2-4.8-3.2-4.8c0,0-1.3-0.2-2.8-0.3l-2.8-0.3l-1.3,2.7l-1.3,2.7l-2.3-0.7			c-1.8-0.6-3.6-1.3-5.3-2.1l-3-1.5v-5.7l26.6,0.1l26.6,0.1l0.1,2.7C1305.7,747.5,1305.8,747.4,1301.4,749.9z"/>	</g>	<path id="Path_8579" class="st1" d="M1551.4,627.9c-0.1-0.4-0.1-1.1-0.2-2.2c-24-2.6-92.5-4.2-156.1,48.3c0,0-20.5-94-116.5-94		s-131.6,94-131.6,94c-53.8-57.9-125.8-53.7-151.8-49.9c0,0.6-0.1,1.3-0.1,1.9c-0.8,14.5-0.4,21.4,2,30.5		c7.5,28.5,30.6,59.9,66.8,90.7c59.3,50.4,139.7,86.8,200.6,91c63.5,4.3,157.2-32.8,219.6-87c9.8-8.7,19-17.9,27.6-27.7		c6.2-7.2,15.4-19.6,14.7-19.6c-0.2,0,0-0.2,0.4-0.4c0.3-0.2,0.6-0.5,0.4-0.7c-0.1-0.1,0.1-0.4,0.4-0.5c0.3-0.1,0.5-0.3,0.4-0.5		s0-0.4,0.4-0.5c0.3-0.1,0.4-0.4,0.3-0.6c-0.1-0.2,0-0.4,0.2-0.4c0.2,0,0.4-0.3,0.4-0.5s0.2-0.5,0.4-0.5c0.2,0,0.4-0.2,0.4-0.4		c0.3-0.8,0.8-1.6,1.3-2.3c1.5-2.3,7.5-13,8.3-14.8c5.7-12.9,9.3-24.4,11-35.9C1551.3,640.4,1551.8,630.3,1551.4,627.9z		 M1301.4,749.9c-1.9,1.1-3.2,1.6-3.5,1.4c-0.3-0.1-1.7-1.3-3.2-2.4l-2.7-2.2l-2.8,3c-6.1,6.6-6.3,6.9-8.1,7		c-2.8,0.3-3.4-0.2-6.8-5.3c-1.8-2.6-3.2-4.8-3.2-4.8c0,0-1.3-0.2-2.8-0.3l-2.8-0.3l-1.3,2.7l-1.3,2.7l-2.3-0.7		c-1.8-0.6-3.6-1.3-5.3-2.1l-3-1.5v-5.7l26.6,0.1l26.6,0.1l0.1,2.7C1305.7,747.5,1305.8,747.4,1301.4,749.9z M1351.4,731.9		c-1.5,5.7-2.7,10.3-2.7,10.5c-1.1,0.1-2.1,0.2-3.2,0.1h-3.2L1338,753c-2.4,5.7-4.6,11-4.9,11.8l-0.6,1.4l-2.1-3.4l-2.1-3.4v-27.7		l-0.8,0.2c-1.7,0.4-13.8,2-19.1,2.5c-21.9,2.2-43.9,1.7-65.7-1.4c-3.5-0.5-6.5-0.9-6.6-0.8c-0.1,0.1,0,6.5,0.2,14.4l0.3,14.2		l-1.7,2.6c-0.9,1.4-1.8,2.6-1.8,2.7c-0.3,0.3-1.3-1.3-3.6-5.9c-2.5-4.8-4.3-9.9-5.6-15.2l-0.7-2.9l-3.1,0.2l-3.1,0.3l-0.8-3.7		c-0.4-2-0.9-5.1-1.1-6.8l-0.3-3.2l-2.6-2.3c-1.5-1.3-3-2.6-3.3-2.8c-0.5-0.4-0.7-1.1-0.7-1.7v-1.2l12.5,0.1l12.5,0.1l0.4,1.3		l0.4,1.3l4.3,0.2c2.4,0.1,10,0.3,16.8,0.5l12.5,0.3l3.2-5l3.3-5h4.2l-0.1-10.4l-0.1-10.4l-5.5-2.4c-17.7-7.8-28.1-16.7-32-27.5		c-0.8-2.2-0.8-3-1-13c-0.1-9.9-0.1-10.8,0.6-13c1.4-5,5.3-8.9,10.3-10.2c1.7-0.5,6.2-0.5,29.8-0.5l27.8,0.1l3.1,1.5		c3.7,1.8,5.4,3.1,7.5,5.9c2.4,3.2,3.1,5.7,3.1,11.8c0,8.7-0.6,16.2-1.5,19.2c-1.3,4.1-3.3,8-5.8,11.5c-5,6.4-14.1,12.8-22.4,15.7		l-2.4,0.9l0.1,10.5l0.1,10.5l2.2,0.2l2.2,0.2l3,4.5l2.9,4.5l13.4,0c7.4,0,13.9,0.1,14.5,0.2c1,0.2,1.2,0.1,2.2-1.6l1.2-1.8h11.5		c8.9,0,11.5,0.1,11.5,0.4C1354.2,721.4,1352.9,726.3,1351.4,731.9z"/>	<path id="Path_8580" class="st2" d="M995,624.1c-6.3,0.9-10,1.9-10,1.9"/>	<path id="Path_8581" class="st2" d="M1560.1,627c0,0-3.2-0.7-8.9-1.3"/>	<path id="Path_8582" class="st3" d="M1550.6,617.7c-0.2-4.5-0.6-10.8-0.9-13.9c-4.3-45.4-15.9-82.5-37.9-121.8		c-0.9-1.5-1.7-3-2.2-4.2c-0.2-0.3-0.3-0.6-0.4-0.8v0c3.1-9.3,15.8-48.2,25.9-92.7c13.7-60.4,22.4-131-4.2-152.2		c0,0-46-3.4-107.6,57c-16.7,16.7-32,34.8-45.6,54.1l-4.3-1.4c-24.8-8.1-50.5-13.1-76.4-14.8c-7.8-0.6-36.9-0.6-46.4,0		c-28.7,1.9-50.7,6.2-75.7,14.9c-1,0.4-1.9,0.7-2.8,1c-14.1-19.2-29.8-37.2-47.1-53.7c-64.1-60.7-111.8-57.2-111.8-57.2		c-28.2,21.9-18.6,95.7-4,158.1c9.6,41.3,21.4,77.6,25.8,90.7c-0.3,0.7-0.7,1.5-1.1,2.2c-23.3,47.2-35.9,92.7-38.9,141.1		c26.1-3.8,98-8.1,151.9,49.9c0,0,35.6-94,131.6-94s116.5,94,116.5,94c63.6-52.5,132.1-50.8,156.1-48.3		C1551,623.7,1550.8,620.7,1550.6,617.7z M1053.9,443.1c0,0-37.4-93.5-27.8-146.9c0,0,0,0,0,0c1.6-9,4.6-16.9,9.3-22.9		c0,0,42.4,4.2,110.8,80.2c0,0-13,6.3-30.4,19.7c0,0-0.1,0.1-0.2,0.1C1096.7,387.8,1072.5,410.8,1053.9,443.1L1053.9,443.1z		 M1212.6,582c-0.6,1.2-3.6,2.5-7,3c-3.4,0.5-13.8,0.2-19.7-0.5c-21.2-2.8-43.7-9.3-60.1-17.3c-9.2-4.5-15.8-9-20.7-13.9l-2.8-2.9		l-0.3-3.8c-0.5-6.6-0.4-17,0.4-20.7c0.5-3.1,1.6-6.1,3.1-8.8c0.8-1,0.8-1,4.4-1c4.4,0,8.1,0.7,14.1,2.6c12.3,3.8,30.6,13,52.9,26.4		c18.4,11,25.3,16.4,30.6,23.8C1211.1,573.5,1213.5,580,1212.6,582z M1448.2,548.9c-0.3,1.6-5,6.4-9.7,9.9		c-13.8,10.1-38.5,19.5-63.6,24c-14.3,2.6-28.6,3-32.9,0.9c-2.8-1.4-3.2-2.5-2.2-6.1c2.1-7.5,8.9-15.7,19.5-23.4		c5.4-3.9,27.3-17.2,37.5-22.8c16.8-9.2,30.4-14.7,39.6-16c2.9-0.4,6.4-0.5,7.5,0c1.8,0.7,3.8,6,4.6,12.4		C1448.9,531.2,1448.7,546.2,1448.2,548.9z M1431.6,373.2l-0.2-0.1c-16.8-13.4-29.4-19.7-29.4-19.7c66.1-76,107-80.2,107-80.2		c4.5,6.1,7.4,13.9,9,22.9c0,0,0,0,0,0c9.3,53.4-26.8,147-26.8,147C1476.1,416.2,1455.8,392.4,1431.6,373.2L1431.6,373.2z"/>	<path id="Path_8583" class="st2" d="M1377.6,343.2c-0.2,0.2-0.3,0.5-0.5,0.7"/>	<path id="Path_8584" class="st2" d="M1509.1,476.9c-0.5,1.4-0.7,2.1-0.7,2.1"/>	<g id="Group_934">		<g id="Group_933">			<g id="Group_932">				<path id="Path_8585" class="st4" d="M1518.1,296.1c-7.8-1-44.7-1.2-86.5,77.1l-0.2-0.1c-16.8-13.4-29.4-19.7-29.4-19.7					c66.1-76,107-80.2,107-80.2C1513.6,279.3,1516.5,287.1,1518.1,296.1z"/>			</g>			<path id="Path_8586" class="st5" d="M1491.3,443.1c-15.2-27-35.4-50.7-59.7-69.9c41.8-78.3,78.6-78.1,86.5-77.1c0,0,0,0,0,0				C1527.4,349.6,1491.3,443.1,1491.3,443.1z"/>			<path id="Path_8587" class="st5" d="M1519.6,296.4c-0.5-0.1-1-0.2-1.5-0.3L1519.6,296.4z"/>		</g>	</g>	<path id="Path_8588" class="st2" d="M1175.1,347.1c-1-1.4-2.1-2.8-3.1-4.2"/>	<path id="Path_8589" class="st2" d="M1035,480.8c1.1,3.4,1.8,5.2,1.8,5.2"/>	<g id="Group_937">		<g id="Group_936">			<g id="Group_935">				<path id="Path_8590" class="st4" d="M1146.2,353.4c0,0-13,6.3-30.4,19.7c0,0-0.1,0.1-0.2,0.1c-43.3-78.4-81.4-78.1-89.6-77.1					c1.6-9,4.6-16.9,9.3-22.9C1035.4,273.2,1077.8,277.4,1146.2,353.4z"/>			</g>			<path id="Path_8591" class="st5" d="M1115.7,373.2c-19,14.6-43.2,37.5-61.8,69.9c0,0-37.4-93.5-27.8-147c0,0,0,0,0,0				C1034.2,295.1,1072.4,294.9,1115.7,373.2z"/>			<path id="Path_8592" class="st5" d="M1026.1,296.1c-0.5,0.1-1.1,0.2-1.6,0.3L1026.1,296.1z"/>		</g>	</g>	<path id="Path_8593" class="st1" d="M1390.1,495c0,0-32,2-28-23s29-28,36-27s35,11,30,32s-12,17-16,18S1390.1,495,1390.1,495z"/>	<path id="Path_8594" class="st1" d="M1154.1,495c0,0-32,2-28-23s29-28,36-27s35,11,30,32s-12,17-16,18S1154.1,495,1154.1,495z"/></g></svg>', N'Shiba Inu', N'SHIB'),
(7, CONCAT(CAST(nchar(13) AS nvarchar(max)), nchar(10), N'<svg width="32" height="32" viewBox="0 0 32 32" xmlns="http://www.w3.org/2000/svg"><g fill="none"><circle fill="#6F41D8" cx="16" cy="16" r="16"/><path d="M21.092 12.693c-.369-.215-.848-.215-1.254 0l-2.879 1.654-1.955 1.078-2.879 1.653c-.369.216-.848.216-1.254 0l-2.288-1.294c-.369-.215-.627-.61-.627-1.042V12.19c0-.431.221-.826.627-1.042l2.25-1.258c.37-.216.85-.216 1.256 0l2.25 1.258c.37.216.628.611.628 1.042v1.654l1.955-1.115v-1.653a1.16 1.16 0 00-.627-1.042l-4.17-2.372c-.369-.216-.848-.216-1.254 0l-4.244 2.372A1.16 1.16 0 006 11.076v4.78c0 .432.221.827.627 1.043l4.244 2.372c.369.215.849.215 1.254 0l2.879-1.618 1.955-1.114 2.879-1.617c.369-.216.848-.216 1.254 0l2.251 1.258c.37.215.627.61.627 1.042v2.552c0 .431-.22.826-.627 1.042l-2.25 1.294c-.37.216-.85.216-1.255 0l-2.251-1.258c-.37-.216-.628-.611-.628-1.042v-1.654l-1.955 1.115v1.653c0 .431.221.827.627 1.042l4.244 2.372c.369.216.848.216 1.254 0l4.244-2.372c.369-.215.627-.61.627-1.042v-4.78a1.16 1.16 0 00-.627-1.042l-4.28-2.409z" fill="#FFF"/></g></svg>'), N'Polygon', N'MATIC'),
(8, N'<svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px"	 viewBox="0 0 1000 1000" style="enable-background:new 0 0 1000 1000;" xml:space="preserve"><style type="text/css">	.st0{fill-rule:evenodd;clip-rule:evenodd;fill:#0055D5;}	.st1{fill-rule:evenodd;clip-rule:evenodd;fill:#FFFFFF;}</style><g>	<g>		<g>			<path id="SVGID_1_" class="st0" d="M500,0c276.1,0,500,223.9,500,500s-223.9,500-500,500S0,776.1,0,500S223.9,0,500,0L500,0z"/>		</g>	</g>	<g>		<g>			<path id="SVGID_3_" class="st1" d="M272,330.8c0,0-53.8-43-57.5,63.2c-3.7,82.3-3.6,242.1,34.5,317.6				c16.1,28.4,37.6,19.7,77.6-8.6c34.8-25.7,160.7-141.7,214-192.6S702,313.5,724.5,412.7c8.9,41.7,19.9,77.1,11.5,169.6				c-2.6,28.5-14.8,50.1-70.4,11.5c-42-27-81.9-56-81.9-56s-7.7-13.8-25.9,8.6c-14.3,17.9-21.5,22.3,1.4,35.9				c23,13.6,139.3,93.8,183.9,106.3c36.8,10.7,57.8,10.5,58.9-61.8c-0.2-57.4-3.5-221.7-24.4-291.7c-15.8-43.6-39-63.4-89.1-14.4				S480.4,522.9,394,592.3c-57.3,43-81.8,81.8-103.4,10.1c-15.2-54.3-34.9-223.3,27.3-191.1c38.9,18.6,58.3,18.5,43.1,30.2				c-15.2,11.6-58.9,51.7-58.9,51.7s-13.5,10.9-7.2,34.5c6.3,16.5,6.3,24.7,40.2-7.2s58.9-56,58.9-56s5.4-8.5,21.5,7.2				s18.7,20.1,18.7,20.1s4.3,11.7,21.5-8.6c17.3-20.3,28.4-32.7-4.3-51.7C418.8,412.3,272,330.8,272,330.8L272,330.8z"/>		</g>	</g></g></svg>', N'Axie-Infinity', N'AXS'),
(9, N'<svg width="32" height="32" viewBox="0 0 32 32" xmlns="http://www.w3.org/2000/svg"><defs><filter id="a"><feColorMatrix in="SourceGraphic" values="0 0 0 0 1.000000 0 0 0 0 1.000000 0 0 0 0 1.000000 0 0 0 1.000000 0"/></filter></defs><g fill="none" fill-rule="evenodd"><circle fill="#FF2D55" fill-rule="nonzero" cx="16" cy="16" r="16"/><g filter="url(#a)"><path d="M12.793 11.534l-7.045 8.454A10.912 10.912 0 015 16C5 9.923 9.923 5 16 5c6.078 0 11 4.923 11 11 0 3.36-1.507 6.369-3.883 8.387H8.883A11.511 11.511 0 017.2 22.6h12.562v-4.763l3.965 4.763H24.8l-5.043-6.05-1.392 1.672-5.571-6.688zM19.758 9.4a2.751 2.751 0 000 5.5 2.751 2.751 0 000-5.5zm-6.963-1.991a1.376 1.376 0 100 2.751 1.376 1.376 0 000-2.751zM9.989 25.212h12.023A10.97 10.97 0 0116 27a10.97 10.97 0 01-6.011-1.788zm7.843-6.346l-2.426 2.909H6.639a11.056 11.056 0 01-.891-1.787h7.046V12.82l5.038 6.045z" fill="#16141A" fill-rule="nonzero"/></g></g></svg>', N'Decentraland', N'MANA'),
(10, N'<svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px"	 viewBox="0 0 31.8 31.8" style="enable-background:new 0 0 31.8 31.8;" xml:space="preserve"><style type="text/css">	.st0{fill:none;stroke:#222326;stroke-width:2.5;}	.st1{fill:#222326;}</style><circle class="st0" cx="15.9" cy="15.9" r="14.7"/><path class="st1" d="M18.7,21.2c-0.1-0.1-0.1-0.3-0.2-0.5c0-0.2-0.1-0.4-0.1-0.6c-0.2,0.2-0.4,0.3-0.6,0.5c-0.2,0.2-0.5,0.3-0.7,0.4	c-0.3,0.1-0.5,0.2-0.9,0.3c-0.3,0.1-0.7,0.1-1,0.1c-0.6,0-1.1-0.1-1.6-0.3c-0.5-0.2-0.9-0.4-1.3-0.7c-0.4-0.3-0.6-0.7-0.8-1.1	c-0.2-0.4-0.3-0.9-0.3-1.4c0-1.2,0.5-2.2,1.4-2.8c0.9-0.7,2.3-1,4.1-1h1.7v-0.7c0-0.6-0.2-1-0.5-1.3c-0.4-0.3-0.9-0.5-1.6-0.5	c-0.6,0-1,0.1-1.3,0.4c-0.3,0.3-0.4,0.6-0.4,1h-3c0-0.5,0.1-1,0.3-1.4c0.2-0.4,0.5-0.8,1-1.2c0.4-0.3,0.9-0.6,1.5-0.8	c0.6-0.2,1.3-0.3,2.1-0.3c0.7,0,1.3,0.1,1.9,0.3c0.6,0.2,1.1,0.4,1.6,0.8c0.4,0.3,0.8,0.8,1,1.3c0.2,0.5,0.4,1.1,0.4,1.8v5	c0,0.6,0,1.1,0.1,1.5c0.1,0.4,0.2,0.8,0.3,1v0.2H18.7z M15.8,19.1c0.3,0,0.6,0,0.8-0.1c0.3-0.1,0.5-0.2,0.7-0.3	c0.2-0.1,0.4-0.2,0.5-0.4c0.1-0.1,0.3-0.3,0.4-0.4v-2h-1.5c-0.5,0-0.9,0-1.2,0.1c-0.3,0.1-0.6,0.2-0.8,0.4c-0.2,0.2-0.4,0.3-0.5,0.6	c-0.1,0.2-0.1,0.5-0.1,0.7c0,0.4,0.1,0.7,0.4,1C14.8,19,15.3,19.1,15.8,19.1z"/></svg>', N'Arweave', N'AR'),
(11, N'<svg width="32" height="32" viewBox="0 0 32 32" xmlns="http://www.w3.org/2000/svg"><g fill="none" fill-rule="evenodd"><circle fill="#E84142" fill-rule="nonzero" cx="16" cy="16" r="16"/><path d="M11.518 22.75H8.49c-.636 0-.95 0-1.142-.123A.77.77 0 017 22.025c-.012-.226.145-.503.46-1.055l7.472-13.193c.318-.56.48-.84.682-.944a.77.77 0 01.698 0c.203.104.364.384.682.944l1.536 2.686.008.014c.343.6.517.906.593 1.226a2.26 2.26 0 010 1.066c-.076.323-.249.63-.597 1.24l-3.926 6.95-.01.017c-.346.606-.52.913-.764 1.145a2.284 2.284 0 01-.93.54c-.319.089-.675.089-1.387.089zm7.643 0h4.336c.64 0 .962 0 1.154-.126a.768.768 0 00.348-.607c.011-.219-.142-.484-.443-1.005l-.032-.054-2.172-3.722-.025-.042c-.305-.517-.46-.778-.657-.879a.762.762 0 00-.693 0c-.2.104-.36.377-.678.925l-2.165 3.722-.007.013c-.317.548-.476.821-.464 1.046a.777.777 0 00.348.606c.188.123.51.123 1.15.123z" fill="#FFF"/></g></svg>', N'Avalanche', N'AVAX'),
(12, N'<svg width="32" height="32" viewBox="0 0 32 32" xmlns="http://www.w3.org/2000/svg"><g fill="none"><circle fill="#E6007A" cx="16" cy="16" r="16"/><path d="M16.272 6.625c-3.707 0-6.736 3.012-6.736 6.736 0 .749.124 1.48.356 2.192a.95.95 0 001.194.589.95.95 0 00.588-1.194 4.745 4.745 0 01-.267-1.73c.071-2.512 2.103-4.58 4.616-4.704a4.86 4.86 0 015.115 4.847 4.862 4.862 0 01-4.58 4.848s-.945.053-1.408.125c-.232.035-.41.071-.535.089-.054.018-.107-.036-.09-.09l.161-.783.873-4.028a.934.934 0 00-.712-1.105.934.934 0 00-1.105.713s-2.103 9.802-2.121 9.909a.934.934 0 00.713 1.105.934.934 0 001.105-.713c.017-.107.303-1.408.303-1.408a2.367 2.367 0 011.996-1.854 21.43 21.43 0 011.051-.089 6.744 6.744 0 006.22-6.719c0-3.724-3.03-6.736-6.737-6.736zm.481 15.505a1.122 1.122 0 00-1.336.873c-.125.606.25 1.212.873 1.337a1.122 1.122 0 001.337-.874c.124-.623-.25-1.212-.874-1.336z" fill="#FFF"/></g></svg>', N'Polkadot', N'DOT'),
(13, N'<svg width="32" height="32" viewBox="0 0 32 32" xmlns="http://www.w3.org/2000/svg"><g fill="none"><circle fill="#3E73C4" cx="16" cy="16" r="16"/><g fill="#FFF"><path d="M20.022 18.124c0-2.124-1.28-2.852-3.84-3.156-1.828-.243-2.193-.728-2.193-1.578 0-.85.61-1.396 1.828-1.396 1.097 0 1.707.364 2.011 1.275a.458.458 0 00.427.303h.975a.416.416 0 00.427-.425v-.06a3.04 3.04 0 00-2.743-2.489V9.142c0-.243-.183-.425-.487-.486h-.915c-.243 0-.426.182-.487.486v1.396c-1.829.242-2.986 1.456-2.986 2.974 0 2.002 1.218 2.791 3.778 3.095 1.707.303 2.255.668 2.255 1.639 0 .97-.853 1.638-2.011 1.638-1.585 0-2.133-.667-2.316-1.578-.06-.242-.244-.364-.427-.364h-1.036a.416.416 0 00-.426.425v.06c.243 1.518 1.219 2.61 3.23 2.914v1.457c0 .242.183.425.487.485h.915c.243 0 .426-.182.487-.485V21.34c1.829-.303 3.047-1.578 3.047-3.217z"/><path d="M12.892 24.497c-4.754-1.7-7.192-6.98-5.424-11.653.914-2.55 2.925-4.491 5.424-5.402.244-.121.365-.303.365-.607v-.85c0-.242-.121-.424-.365-.485-.061 0-.183 0-.244.06a10.895 10.895 0 00-7.13 13.717c1.096 3.4 3.717 6.01 7.13 7.102.244.121.488 0 .548-.243.061-.06.061-.122.061-.243v-.85c0-.182-.182-.424-.365-.546zm6.46-18.936c-.244-.122-.488 0-.548.242-.061.061-.061.122-.061.243v.85c0 .243.182.485.365.607 4.754 1.7 7.192 6.98 5.424 11.653-.914 2.55-2.925 4.491-5.424 5.402-.244.121-.365.303-.365.607v.85c0 .242.121.424.365.485.061 0 .183 0 .244-.06a10.895 10.895 0 007.13-13.717c-1.096-3.46-3.778-6.07-7.13-7.162z"/></g></g></svg>', N'USD Coin', N'USDC');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CryptoId', N'CryptoIcon', N'CryptoName', N'TickerSymbol') AND [object_id] = OBJECT_ID(N'[Cryptos]'))
    SET IDENTITY_INSERT [Cryptos] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'DataImportTypeId', N'DataImportTypeName') AND [object_id] = OBJECT_ID(N'[DataImportTypes]'))
    SET IDENTITY_INSERT [DataImportTypes] ON;
INSERT INTO [DataImportTypes] ([DataImportTypeId], [DataImportTypeName])
VALUES (1, N'API'),
(2, N'CSV');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'DataImportTypeId', N'DataImportTypeName') AND [object_id] = OBJECT_ID(N'[DataImportTypes]'))
    SET IDENTITY_INSERT [DataImportTypes] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ExchangeId', N'ExchangeName') AND [object_id] = OBJECT_ID(N'[Exchanges]'))
    SET IDENTITY_INSERT [Exchanges] ON;
INSERT INTO [Exchanges] ([ExchangeId], [ExchangeName])
VALUES (1, N'Coinbase'),
(2, N'Coinbase Pro'),
(3, N'Crypto.com'),
(4, N'Etoro'),
(5, N'Binance');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ExchangeId', N'ExchangeName') AND [object_id] = OBJECT_ID(N'[Exchanges]'))
    SET IDENTITY_INSERT [Exchanges] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'TransactionTypeId', N'TransactionTypeName') AND [object_id] = OBJECT_ID(N'[TransactionTypes]'))
    SET IDENTITY_INSERT [TransactionTypes] ON;
INSERT INTO [TransactionTypes] ([TransactionTypeId], [TransactionTypeName])
VALUES (1, N'Buy'),
(2, N'Sell'),
(3, N'Withdrawl'),
(4, N'Staking Reward'),
(5, N'Loan Interest'),
(6, N'Perk Reward'),
(7, N'Deposit');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'TransactionTypeId', N'TransactionTypeName') AND [object_id] = OBJECT_ID(N'[TransactionTypes]'))
    SET IDENTITY_INSERT [TransactionTypes] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ExchangeTransactionTypeId', N'DataImportTypeId', N'ExchangeId', N'ExchangeTransactionTypeName', N'TransactionTypeId') AND [object_id] = OBJECT_ID(N'[ExchangeTransactionTypes]'))
    SET IDENTITY_INSERT [ExchangeTransactionTypes] ON;
INSERT INTO [ExchangeTransactionTypes] ([ExchangeTransactionTypeId], [DataImportTypeId], [ExchangeId], [ExchangeTransactionTypeName], [TransactionTypeId])
VALUES (1, 2, 1, N'Buy', 1),
(2, 2, 1, N'Advanced Trade Buy', 1),
(3, 2, 1, N'Sell', 2),
(4, 2, 1, N'Rewards Income', 4),
(5, 2, 1, N'Learning Reward', 6),
(6, 2, 3, N'viban_purchase', 1),
(7, 2, 3, N'reimbursement', 6),
(8, 2, 3, N'referral_card_cashback', 6),
(9, 2, 3, N'card_cashback_reverted', 6),
(10, 2, 2, N'Buy', 1),
(11, 2, 4, N'Open Position', 1),
(12, 2, 4, N'Staking', 4),
(13, 2, 5, N'Buy', 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ExchangeTransactionTypeId', N'DataImportTypeId', N'ExchangeId', N'ExchangeTransactionTypeName', N'TransactionTypeId') AND [object_id] = OBJECT_ID(N'[ExchangeTransactionTypes]'))
    SET IDENTITY_INSERT [ExchangeTransactionTypes] OFF;
GO

CREATE INDEX [IX_CryptoPrices_CryptoId] ON [CryptoPrices] ([CryptoId]);
GO

CREATE INDEX [IX_ExchangeTransactionTypes_DataImportTypeId] ON [ExchangeTransactionTypes] ([DataImportTypeId]);
GO

CREATE INDEX [IX_ExchangeTransactionTypes_ExchangeId] ON [ExchangeTransactionTypes] ([ExchangeId]);
GO

CREATE INDEX [IX_ExchangeTransactionTypes_TransactionTypeId] ON [ExchangeTransactionTypes] ([TransactionTypeId]);
GO

CREATE INDEX [IX_Transactions_AppUserId] ON [Transactions] ([AppUserId]);
GO

CREATE INDEX [IX_Transactions_CryptoId] ON [Transactions] ([CryptoId]);
GO

CREATE INDEX [IX_Transactions_ExchangeTransactionTypeId] ON [Transactions] ([ExchangeTransactionTypeId]);
GO

CREATE INDEX [IX_Transactions_FromExchangeId] ON [Transactions] ([FromExchangeId]);
GO

CREATE INDEX [IX_Transactions_ToExchangeId] ON [Transactions] ([ToExchangeId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230421143824_InitialCreate', N'7.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE OR ALTER PROCEDURE [dbo].[PortfolioSummary]
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

GO

CREATE OR ALTER PROCEDURE [dbo].[PortfolioRunningTotalByMonth]
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

GO

CREATE OR ALTER PROCEDURE [dbo].[PortfolioRunningTotalByDay]
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


GO

CREATE OR ALTER PROCEDURE [dbo].[SeedCryptoPricesByMonth]
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

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230421151902_CreateStoredProcs', N'7.0.3');
GO

COMMIT;
GO

