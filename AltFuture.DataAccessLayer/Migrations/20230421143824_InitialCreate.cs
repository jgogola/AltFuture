﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AltFuture.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    AppUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.AppUserId);
                });

            migrationBuilder.CreateTable(
                name: "Cryptos",
                columns: table => new
                {
                    CryptoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CryptoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TickerSymbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CryptoIcon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cryptos", x => x.CryptoId);
                });

            migrationBuilder.CreateTable(
                name: "DataImportTypes",
                columns: table => new
                {
                    DataImportTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataImportTypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataImportTypes", x => x.DataImportTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Exchanges",
                columns: table => new
                {
                    ExchangeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExchangeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exchanges", x => x.ExchangeId);
                });

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

            migrationBuilder.CreateTable(
                name: "CryptoPrices",
                columns: table => new
                {
                    CryptoPriceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateRecorded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CryptoId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(30,20)", nullable: false),
                    Volume24h = table.Column<decimal>(type: "decimal(25,10)", nullable: false),
                    VolumeChange24h = table.Column<decimal>(type: "decimal(20,10)", nullable: false),
                    PercentChange1h = table.Column<decimal>(type: "decimal(20,10)", nullable: false),
                    PercentChange24h = table.Column<decimal>(type: "decimal(20,10)", nullable: false),
                    PercentChange7d = table.Column<decimal>(type: "decimal(20,10)", nullable: false),
                    PercentChange30d = table.Column<decimal>(type: "decimal(20,10)", nullable: false),
                    PercentChange60d = table.Column<decimal>(type: "decimal(20,10)", nullable: false),
                    PercentChange90d = table.Column<decimal>(type: "decimal(20,10)", nullable: false),
                    MarketCap = table.Column<decimal>(type: "decimal(25,10)", nullable: false),
                    MarketCapDominance = table.Column<decimal>(type: "decimal(25,10)", nullable: false),
                    IsMonthStartPrice = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoPrices", x => x.CryptoPriceId);
                    table.ForeignKey(
                        name: "FK_CryptoPrices_Cryptos_CryptoId",
                        column: x => x.CryptoId,
                        principalTable: "Cryptos",
                        principalColumn: "CryptoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeTransactionTypes",
                columns: table => new
                {
                    ExchangeTransactionTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExchangeTransactionTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExchangeId = table.Column<int>(type: "int", nullable: false),
                    TransactionTypeId = table.Column<int>(type: "int", nullable: false),
                    DataImportTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeTransactionTypes", x => x.ExchangeTransactionTypeId);
                    table.ForeignKey(
                        name: "FK_ExchangeTransactionTypes_DataImportTypes_DataImportTypeId",
                        column: x => x.DataImportTypeId,
                        principalTable: "DataImportTypes",
                        principalColumn: "DataImportTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExchangeTransactionTypes_Exchanges_ExchangeId",
                        column: x => x.ExchangeId,
                        principalTable: "Exchanges",
                        principalColumn: "ExchangeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExchangeTransactionTypes_TransactionTypes_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalTable: "TransactionTypes",
                        principalColumn: "TransactionTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionReferenceNum = table.Column<int>(type: "int", nullable: false),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    CryptoId = table.Column<int>(type: "int", nullable: false),
                    ExchangeTransactionTypeId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,10)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,10)", nullable: false),
                    InvestmentTotal = table.Column<decimal>(type: "decimal(18,10)", nullable: false, computedColumnSql: "Price * Quantity"),
                    Fee = table.Column<decimal>(type: "decimal(18,10)", nullable: false),
                    TransactionTotal = table.Column<decimal>(type: "decimal(18,10)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FromExchangeId = table.Column<int>(type: "int", nullable: false),
                    ToExchangeId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "AppUserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Cryptos_CryptoId",
                        column: x => x.CryptoId,
                        principalTable: "Cryptos",
                        principalColumn: "CryptoId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Transactions_ExchangeTransactionTypes_ExchangeTransactionTypeId",
                        column: x => x.ExchangeTransactionTypeId,
                        principalTable: "ExchangeTransactionTypes",
                        principalColumn: "ExchangeTransactionTypeId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Transactions_Exchanges_FromExchangeId",
                        column: x => x.FromExchangeId,
                        principalTable: "Exchanges",
                        principalColumn: "ExchangeId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Transactions_Exchanges_ToExchangeId",
                        column: x => x.ToExchangeId,
                        principalTable: "Exchanges",
                        principalColumn: "ExchangeId");
                });

            migrationBuilder.InsertData(
                table: "Cryptos",
                columns: new[] { "CryptoId", "CryptoIcon", "CryptoName", "TickerSymbol" },
                values: new object[,]
                {
                    { 1, "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 32 32\"><g fill=\"none\" fill-rule=\"evenodd\"><circle cx=\"16\" cy=\"16\" r=\"16\" fill=\"#F7931A\"/><path fill=\"#FFF\" fill-rule=\"nonzero\" d=\"M23.189 14.02c.314-2.096-1.283-3.223-3.465-3.975l.708-2.84-1.728-.43-.69 2.765c-.454-.114-.92-.22-1.385-.326l.695-2.783L15.596 6l-.708 2.839c-.376-.086-.746-.17-1.104-.26l.002-.009-2.384-.595-.46 1.846s1.283.294 1.256.312c.7.175.826.638.805 1.006l-.806 3.235c.048.012.11.03.18.057l-.183-.045-1.13 4.532c-.086.212-.303.531-.793.41.018.025-1.256-.313-1.256-.313l-.858 1.978 2.25.561c.418.105.828.215 1.231.318l-.715 2.872 1.727.43.708-2.84c.472.127.93.245 1.378.357l-.706 2.828 1.728.43.715-2.866c2.948.558 5.164.333 6.097-2.333.752-2.146-.037-3.385-1.588-4.192 1.13-.26 1.98-1.003 2.207-2.538zm-3.95 5.538c-.533 2.147-4.148.986-5.32.695l.95-3.805c1.172.293 4.929.872 4.37 3.11zm.535-5.569c-.487 1.953-3.495.96-4.47.717l.86-3.45c.975.243 4.118.696 3.61 2.733z\"/></g></svg>", "Bitcoin", "BTC" },
                    { 2, "<svg width=\"32\" height=\"32\" viewBox=\"0 0 32 32\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\"><defs><filter x=\"-7.5%\" y=\"-6%\" width=\"115.5%\" height=\"117.2%\" filterUnits=\"objectBoundingBox\" id=\"a\"><feOffset dy=\".5\" in=\"SourceAlpha\" result=\"shadowOffsetOuter1\"/><feGaussianBlur stdDeviation=\".5\" in=\"shadowOffsetOuter1\" result=\"shadowBlurOuter1\"/><feColorMatrix values=\"0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0.204257246 0\" in=\"shadowBlurOuter1\"/></filter><path d=\"M15.725 6.06c.479-.247 1.064.324.81.795-.149.384-.71.486-.996.193-.303-.28-.204-.836.186-.989zm-5.155.546c.291-.118.66.144.63.457.03.338-.39.588-.687.427-.393-.15-.348-.778.057-.884zm10.558.893c-.455-.054-.527-.758-.09-.9.34-.162.652.143.702.46-.072.27-.302.518-.612.44zm-9.385 1.265c.487-.303 1.181.148 1.106.705-.025.561-.783.887-1.211.507-.414-.298-.351-.982.105-1.212zm7.43.322c.217-.55 1.097-.568 1.344-.032.245.417-.056.934-.491 1.076-.577.106-1.124-.508-.853-1.044zm-4.069 1.013c-.005-.474.433-.826.89-.859.304.06.634.187.764.488.243.416.027.987-.41 1.178-.2.11-.438.069-.656.056-.333-.16-.614-.477-.588-.863zm-7.666.69c.445-.27 1.045.22.876.696-.092.411-.654.578-.975.316-.343-.246-.289-.837.1-1.013zm16.462-.002c.377-.288 1 .043.954.511.026.427-.513.75-.887.53-.412-.183-.455-.807-.067-1.04zm-6.64.851c.622-.22 1.362.043 1.716.59.468.667.22 1.683-.507 2.066-.752.453-1.851.07-2.13-.758-.315-.74.145-1.666.92-1.898zm-3.653.073c.69-.32 1.619-.052 1.952.642.392.676.089 1.617-.612 1.966-.702.393-1.693.095-2.032-.63-.381-.702-.043-1.655.692-1.978zM9.95 12.94c.053-.437.472-.722.895-.752a.98.98 0 01.87.857c-.03.45-.383.888-.867.886-.533.045-1-.477-.898-.991zm10.802-.656c.547-.313 1.306.142 1.282.76.037.655-.803 1.116-1.347.732-.566-.32-.522-1.22.065-1.492zm-8.63 2.307c.638-.173 1.37.123 1.683.701.343.582.203 1.39-.33 1.818-.685.626-1.946.374-2.31-.48-.419-.783.09-1.833.956-2.04zm6.927-.003c.621-.175 1.351.06 1.685.617.442.637.231 1.588-.426 1.998-.69.477-1.756.227-2.136-.519-.46-.771.003-1.861.877-2.096zm-11.04.726c.552-.205 1.164.394.94.933-.136.49-.839.672-1.202.31-.425-.34-.268-1.095.262-1.243zm14.969.782a.836.836 0 01.788-.874c.378.06.746.36.716.765.035.535-.62.898-1.084.647-.217-.109-.328-.328-.42-.538zM5.294 15.58c.332-.143.743.14.667.503-.018.411-.635.57-.861.226-.2-.239-.08-.606.194-.73zm20.949-.009c.234-.163.61-.046.702.223.157.294-.131.696-.467.647-.472.042-.624-.665-.235-.87zm-12.317 1.973c.874-.223 1.814.494 1.82 1.38.056.895-.87 1.688-1.764 1.482-.692-.11-1.235-.766-1.212-1.453-.002-.658.502-1.27 1.156-1.409zm3.462-.001c.887-.244 1.855.486 1.841 1.392.047.878-.85 1.645-1.726 1.47-.825-.104-1.433-.995-1.203-1.783.116-.524.562-.95 1.088-1.08zm-6.676.545c.614-.103 1.19.57.941 1.144-.182.612-1.086.777-1.486.278-.468-.48-.118-1.356.545-1.422zm10.154.027c.548-.226 1.22.24 1.178.825.022.643-.808 1.087-1.343.711-.607-.337-.496-1.33.165-1.536zm2.838 2.8c-.214-.393.175-.914.62-.841.22-.004.375.167.516.311.029.233.078.511-.119.69-.267.333-.872.238-1.017-.16zm-16.268-.732c.415-.271 1.012.134.918.61-.05.423-.59.664-.945.424-.382-.217-.368-.836.027-1.034zm8.193.883c.543-.235 1.235.23 1.183.818.04.65-.815 1.1-1.346.71-.59-.335-.491-1.321.163-1.528zm-3.794.871c.462-.239 1.082.174 1.04.684.014.418-.4.774-.82.712-.347-.007-.573-.314-.685-.605.006-.317.139-.67.465-.79zm7.686.008c.476-.29 1.152.126 1.107.67.012.57-.752.934-1.195.56-.428-.293-.376-.997.088-1.23zm1.337 3.25c-.212-.314.037-.693.38-.765.277.055.57.26.511.574-.04.427-.674.557-.891.192zm-10.611-.273c.084-.25.288-.497.587-.432.435.03.564.676.183.875-.342.227-.74-.084-.77-.443zm5.12.287c.083-.37.568-.549.888-.353.212.09.274.322.328.52a8.822 8.822 0 00-.08.31c-.131.152-.3.305-.518.3-.405.047-.771-.404-.619-.777z\" id=\"b\"/></defs><g fill=\"none\"><circle cx=\"16\" cy=\"16\" r=\"16\" fill=\"#0D1E30\"/><use fill=\"#000\" filter=\"url(#a)\" xlink:href=\"#b\"/><use fill=\"#FFF\" xlink:href=\"#b\"/></g></svg>", "Cardano", "ADA" },
                    { 3, "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 32 32\"><g fill=\"none\" fill-rule=\"evenodd\"><circle cx=\"16\" cy=\"16\" r=\"16\" fill=\"#627EEA\"/><g fill=\"#FFF\" fill-rule=\"nonzero\"><path fill-opacity=\".602\" d=\"M16.498 4v8.87l7.497 3.35z\"/><path d=\"M16.498 4L9 16.22l7.498-3.35z\"/><path fill-opacity=\".602\" d=\"M16.498 21.968v6.027L24 17.616z\"/><path d=\"M16.498 27.995v-6.028L9 17.616z\"/><path fill-opacity=\".2\" d=\"M16.498 20.573l7.497-4.353-7.497-3.348z\"/><path fill-opacity=\".602\" d=\"M9 16.22l7.498 4.353v-7.701z\"/></g></g></svg>", "Ethereum", "ETH" },
                    { 4, "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 32 32\"><g fill=\"none\"><circle cx=\"16\" cy=\"16\" r=\"16\" fill=\"#F3BA2F\"/><path fill=\"#FFF\" d=\"M12.116 14.404L16 10.52l3.886 3.886 2.26-2.26L16 6l-6.144 6.144 2.26 2.26zM6 16l2.26-2.26L10.52 16l-2.26 2.26L6 16zm6.116 1.596L16 21.48l3.886-3.886 2.26 2.259L16 26l-6.144-6.144-.003-.003 2.263-2.257zM21.48 16l2.26-2.26L26 16l-2.26 2.26L21.48 16zm-3.188-.002h.002V16L16 18.294l-2.291-2.29-.004-.004.004-.003.401-.402.195-.195L16 13.706l2.293 2.293z\"/></g></svg>", "Bianance", "BNB" },
                    { 5, "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 32 32\"><g fill=\"none\" fill-rule=\"evenodd\"><circle cx=\"16\" cy=\"16\" r=\"16\" fill=\"#103F68\"/><path fill=\"#FFF\" fill-rule=\"nonzero\" d=\"M15.98 5.018l9.52 5.483v11L15.991 27l-.077-.019-9.414-5.48v-11l9.414-5.483h.066zm-.031 1.138L7.5 11.076v9.85l8.448 4.919 1.032-.597 7.52-4.325v-9.845l-7.52-4.35-1.031-.572zm-7.14 10.61l2.501-1.87 2.211 1.412v2.54l1.673 1.612-.001.756-1.612 1.51H12.22l-3.41-5.96zm7.903 4.452l-.003-.76 1.667-1.61v-2.54l2.187-1.43 2.496 1.889-3.393 5.942h-1.344l-1.61-1.491zm-2.37-4.91l-.814-2.131h4.838l-.798 2.131.236 2.382-1.867.004-1.845.003.25-2.389zm1.595-2.715l-4.598-.002.855-3.82h7.464l.9 3.825-4.621-.003z\"/></g></svg>", "Cronos", "CRO" },
                    { 6, "<svg version=\"1.1\" id=\"Layer_1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"0px\" y=\"0px\"	 viewBox=\"0 0 641.7 649\" style=\"enable-background:new 0 0 641.7 649;\" xml:space=\"preserve\"><style type=\"text/css\">	.st0{fill:#F00500;}	.st1{fill:#FFFFFF;}	.st2{fill:none;}	.st3{fill:#FFA409;}	.st4{fill:#FF9300;}	.st5{fill:#FF8300;}</style><g id=\"Group_938\" transform=\"translate(-953.348 -232.003)\">	<g id=\"Group_931\">		<path id=\"Path_8573\" class=\"st0\" d=\"M1423.2,289.1c-16.7,16.7-32,34.8-45.6,54.1l-4.3-1.4c-24.8-8.1-50.5-13.1-76.4-14.8			c-7.8-0.6-36.9-0.6-46.4,0c-28.7,1.9-50.7,6.2-75.7,14.9c-1,0.4-1.9,0.7-2.8,1c-14.1-19.2-29.8-37.2-47.1-53.7			c83-42.5,180.1-47.6,267-14.1c9.3,3.5,18.4,7.6,27.3,12L1423.2,289.1z\"/>		<path id=\"Path_8574\" class=\"st0\" d=\"M1578,668.4c-12.6,35.9-31.5,69.2-56,98.2c-13.3,15.6-28,29.9-43.8,42.9			c-37,29.8-80.2,51.1-126.3,62.3c-51,12.3-104.2,12.3-155.2,0c-46.2-11.2-89.3-32.5-126.3-62.3c-15.8-13-30.5-27.3-43.8-42.9			c-70.5-83.3-91.9-197.7-56-300.8c6.8-19.1,15.3-37.5,25.5-55c4.7-8,10.5-17,13.3-20.7c9.6,41.3,21.4,77.6,25.8,90.7			c-0.3,0.7-0.7,1.5-1.1,2.2c-23.3,47.2-35.9,92.7-38.9,141.1c0,0.6-0.1,1.3-0.1,1.9c-0.8,14.5-0.4,21.4,2,30.5			c7.5,28.5,30.6,59.9,66.8,90.7c59.3,50.4,139.7,86.8,200.6,91c63.5,4.3,157.2-32.8,219.6-87c9.8-8.7,19-17.9,27.6-27.7			c6.2-7.2,15.4-19.6,14.7-19.6c-0.2,0,0-0.2,0.4-0.4c0.3-0.2,0.6-0.5,0.4-0.7c-0.1-0.1,0.1-0.4,0.4-0.5c0.3-0.1,0.5-0.3,0.4-0.5			s0-0.4,0.4-0.5c0.3-0.1,0.4-0.4,0.3-0.6c-0.1-0.2,0-0.4,0.2-0.4c0.2,0,0.4-0.3,0.4-0.5s0.2-0.5,0.4-0.5c0.2,0,0.4-0.2,0.4-0.4			c0.3-0.8,0.8-1.6,1.3-2.3c1.5-2.3,7.5-13,8.3-14.8c5.7-12.9,9.3-24.4,11-35.9c0.8-5.5,1.3-15.6,0.9-18c-0.1-0.4-0.1-1.1-0.2-2.2			c-0.2-2-0.3-5-0.5-8c-0.2-4.5-0.6-10.8-0.9-13.9c-4.3-45.4-15.9-82.5-37.9-121.8c-0.9-1.5-1.7-3-2.2-4.2c-0.2-0.3-0.3-0.6-0.4-0.8			v0c3.1-9.3,15.8-48.2,25.9-92.7l0.2,0.2l1.5,2c2.6,3.5,9.5,13.9,12.6,18.9c14.1,23,25.1,47.7,32.9,73.5			C1600.7,541,1599.2,607.2,1578,668.4z\"/>		<path id=\"Path_8575\" d=\"M1448.2,548.9c-0.3,1.6-5,6.4-9.7,9.9c-13.8,10.1-38.5,19.5-63.6,24c-14.3,2.6-28.6,3-32.9,0.9			c-2.8-1.4-3.2-2.5-2.2-6.1c2.1-7.5,8.9-15.7,19.5-23.4c5.4-3.9,27.3-17.2,37.5-22.8c16.8-9.2,30.4-14.7,39.6-16			c2.9-0.4,6.4-0.5,7.5,0c1.8,0.7,3.8,6,4.6,12.4C1448.9,531.2,1448.7,546.2,1448.2,548.9z\"/>		<path id=\"Path_8576\" d=\"M1212.6,582c-0.6,1.2-3.6,2.5-7,3c-3.4,0.5-13.8,0.2-19.7-0.5c-21.2-2.8-43.7-9.3-60.1-17.3			c-9.2-4.5-15.8-9-20.7-13.9l-2.8-2.9l-0.3-3.8c-0.5-6.6-0.4-17,0.4-20.7c0.5-3.1,1.6-6.1,3.1-8.8c0.8-1,0.8-1,4.4-1			c4.4,0,8.1,0.7,14.1,2.6c12.3,3.8,30.6,13,52.9,26.4c18.4,11,25.3,16.4,30.6,23.8C1211.1,573.5,1213.5,580,1212.6,582z\"/>		<path id=\"Path_8577\" d=\"M1354.2,721.2c0,0.3-1.2,5.1-2.7,10.8c-1.5,5.7-2.7,10.3-2.7,10.5c-1.1,0.1-2.1,0.2-3.2,0.1h-3.2L1338,753			c-2.4,5.7-4.6,11-4.9,11.8l-0.6,1.4l-2.1-3.4l-2.1-3.4v-27.7l-0.8,0.2c-1.7,0.4-13.8,2-19.1,2.5c-21.9,2.2-43.9,1.7-65.7-1.4			c-3.5-0.5-6.5-0.9-6.6-0.8c-0.1,0.1,0,6.5,0.2,14.4l0.3,14.2l-1.7,2.6c-0.9,1.4-1.8,2.6-1.8,2.7c-0.3,0.3-1.3-1.3-3.6-5.9			c-2.5-4.8-4.3-9.9-5.6-15.2l-0.7-2.9l-3.1,0.2l-3.1,0.3l-0.8-3.7c-0.4-2-0.9-5.1-1.1-6.8l-0.3-3.2l-2.6-2.3			c-1.5-1.3-3-2.6-3.3-2.8c-0.5-0.4-0.7-1.1-0.7-1.7v-1.2l12.5,0.1l12.5,0.1l0.4,1.3l0.4,1.3l4.3,0.2c2.4,0.1,10,0.3,16.8,0.5			l12.5,0.3l3.2-5l3.3-5h4.2l-0.1-10.4l-0.1-10.4l-5.5-2.4c-17.7-7.8-28.1-16.7-32-27.5c-0.8-2.2-0.8-3-1-13			c-0.1-9.9-0.1-10.8,0.6-13c1.4-5,5.3-8.9,10.3-10.2c1.7-0.5,6.2-0.5,29.8-0.5l27.8,0.1l3.1,1.5c3.7,1.8,5.4,3.1,7.5,5.9			c2.4,3.2,3.1,5.7,3.1,11.8c0,8.7-0.6,16.2-1.5,19.2c-1.3,4.1-3.3,8-5.8,11.5c-5,6.4-14.1,12.8-22.4,15.7l-2.4,0.9l0.1,10.5			l0.1,10.5l2.2,0.2l2.2,0.2l3,4.5l2.9,4.5l13.4,0c7.4,0,13.9,0.1,14.5,0.2c1,0.2,1.2,0.1,2.2-1.6l1.2-1.8h11.5			C1351.6,720.7,1354.2,720.9,1354.2,721.2z\"/>		<path id=\"Path_8578\" d=\"M1301.4,749.9c-1.9,1.1-3.2,1.6-3.5,1.4c-0.3-0.1-1.7-1.3-3.2-2.4l-2.7-2.2l-2.8,3c-6.1,6.6-6.3,6.9-8.1,7			c-2.8,0.3-3.4-0.2-6.8-5.3c-1.8-2.6-3.2-4.8-3.2-4.8c0,0-1.3-0.2-2.8-0.3l-2.8-0.3l-1.3,2.7l-1.3,2.7l-2.3-0.7			c-1.8-0.6-3.6-1.3-5.3-2.1l-3-1.5v-5.7l26.6,0.1l26.6,0.1l0.1,2.7C1305.7,747.5,1305.8,747.4,1301.4,749.9z\"/>	</g>	<path id=\"Path_8579\" class=\"st1\" d=\"M1551.4,627.9c-0.1-0.4-0.1-1.1-0.2-2.2c-24-2.6-92.5-4.2-156.1,48.3c0,0-20.5-94-116.5-94		s-131.6,94-131.6,94c-53.8-57.9-125.8-53.7-151.8-49.9c0,0.6-0.1,1.3-0.1,1.9c-0.8,14.5-0.4,21.4,2,30.5		c7.5,28.5,30.6,59.9,66.8,90.7c59.3,50.4,139.7,86.8,200.6,91c63.5,4.3,157.2-32.8,219.6-87c9.8-8.7,19-17.9,27.6-27.7		c6.2-7.2,15.4-19.6,14.7-19.6c-0.2,0,0-0.2,0.4-0.4c0.3-0.2,0.6-0.5,0.4-0.7c-0.1-0.1,0.1-0.4,0.4-0.5c0.3-0.1,0.5-0.3,0.4-0.5		s0-0.4,0.4-0.5c0.3-0.1,0.4-0.4,0.3-0.6c-0.1-0.2,0-0.4,0.2-0.4c0.2,0,0.4-0.3,0.4-0.5s0.2-0.5,0.4-0.5c0.2,0,0.4-0.2,0.4-0.4		c0.3-0.8,0.8-1.6,1.3-2.3c1.5-2.3,7.5-13,8.3-14.8c5.7-12.9,9.3-24.4,11-35.9C1551.3,640.4,1551.8,630.3,1551.4,627.9z		 M1301.4,749.9c-1.9,1.1-3.2,1.6-3.5,1.4c-0.3-0.1-1.7-1.3-3.2-2.4l-2.7-2.2l-2.8,3c-6.1,6.6-6.3,6.9-8.1,7		c-2.8,0.3-3.4-0.2-6.8-5.3c-1.8-2.6-3.2-4.8-3.2-4.8c0,0-1.3-0.2-2.8-0.3l-2.8-0.3l-1.3,2.7l-1.3,2.7l-2.3-0.7		c-1.8-0.6-3.6-1.3-5.3-2.1l-3-1.5v-5.7l26.6,0.1l26.6,0.1l0.1,2.7C1305.7,747.5,1305.8,747.4,1301.4,749.9z M1351.4,731.9		c-1.5,5.7-2.7,10.3-2.7,10.5c-1.1,0.1-2.1,0.2-3.2,0.1h-3.2L1338,753c-2.4,5.7-4.6,11-4.9,11.8l-0.6,1.4l-2.1-3.4l-2.1-3.4v-27.7		l-0.8,0.2c-1.7,0.4-13.8,2-19.1,2.5c-21.9,2.2-43.9,1.7-65.7-1.4c-3.5-0.5-6.5-0.9-6.6-0.8c-0.1,0.1,0,6.5,0.2,14.4l0.3,14.2		l-1.7,2.6c-0.9,1.4-1.8,2.6-1.8,2.7c-0.3,0.3-1.3-1.3-3.6-5.9c-2.5-4.8-4.3-9.9-5.6-15.2l-0.7-2.9l-3.1,0.2l-3.1,0.3l-0.8-3.7		c-0.4-2-0.9-5.1-1.1-6.8l-0.3-3.2l-2.6-2.3c-1.5-1.3-3-2.6-3.3-2.8c-0.5-0.4-0.7-1.1-0.7-1.7v-1.2l12.5,0.1l12.5,0.1l0.4,1.3		l0.4,1.3l4.3,0.2c2.4,0.1,10,0.3,16.8,0.5l12.5,0.3l3.2-5l3.3-5h4.2l-0.1-10.4l-0.1-10.4l-5.5-2.4c-17.7-7.8-28.1-16.7-32-27.5		c-0.8-2.2-0.8-3-1-13c-0.1-9.9-0.1-10.8,0.6-13c1.4-5,5.3-8.9,10.3-10.2c1.7-0.5,6.2-0.5,29.8-0.5l27.8,0.1l3.1,1.5		c3.7,1.8,5.4,3.1,7.5,5.9c2.4,3.2,3.1,5.7,3.1,11.8c0,8.7-0.6,16.2-1.5,19.2c-1.3,4.1-3.3,8-5.8,11.5c-5,6.4-14.1,12.8-22.4,15.7		l-2.4,0.9l0.1,10.5l0.1,10.5l2.2,0.2l2.2,0.2l3,4.5l2.9,4.5l13.4,0c7.4,0,13.9,0.1,14.5,0.2c1,0.2,1.2,0.1,2.2-1.6l1.2-1.8h11.5		c8.9,0,11.5,0.1,11.5,0.4C1354.2,721.4,1352.9,726.3,1351.4,731.9z\"/>	<path id=\"Path_8580\" class=\"st2\" d=\"M995,624.1c-6.3,0.9-10,1.9-10,1.9\"/>	<path id=\"Path_8581\" class=\"st2\" d=\"M1560.1,627c0,0-3.2-0.7-8.9-1.3\"/>	<path id=\"Path_8582\" class=\"st3\" d=\"M1550.6,617.7c-0.2-4.5-0.6-10.8-0.9-13.9c-4.3-45.4-15.9-82.5-37.9-121.8		c-0.9-1.5-1.7-3-2.2-4.2c-0.2-0.3-0.3-0.6-0.4-0.8v0c3.1-9.3,15.8-48.2,25.9-92.7c13.7-60.4,22.4-131-4.2-152.2		c0,0-46-3.4-107.6,57c-16.7,16.7-32,34.8-45.6,54.1l-4.3-1.4c-24.8-8.1-50.5-13.1-76.4-14.8c-7.8-0.6-36.9-0.6-46.4,0		c-28.7,1.9-50.7,6.2-75.7,14.9c-1,0.4-1.9,0.7-2.8,1c-14.1-19.2-29.8-37.2-47.1-53.7c-64.1-60.7-111.8-57.2-111.8-57.2		c-28.2,21.9-18.6,95.7-4,158.1c9.6,41.3,21.4,77.6,25.8,90.7c-0.3,0.7-0.7,1.5-1.1,2.2c-23.3,47.2-35.9,92.7-38.9,141.1		c26.1-3.8,98-8.1,151.9,49.9c0,0,35.6-94,131.6-94s116.5,94,116.5,94c63.6-52.5,132.1-50.8,156.1-48.3		C1551,623.7,1550.8,620.7,1550.6,617.7z M1053.9,443.1c0,0-37.4-93.5-27.8-146.9c0,0,0,0,0,0c1.6-9,4.6-16.9,9.3-22.9		c0,0,42.4,4.2,110.8,80.2c0,0-13,6.3-30.4,19.7c0,0-0.1,0.1-0.2,0.1C1096.7,387.8,1072.5,410.8,1053.9,443.1L1053.9,443.1z		 M1212.6,582c-0.6,1.2-3.6,2.5-7,3c-3.4,0.5-13.8,0.2-19.7-0.5c-21.2-2.8-43.7-9.3-60.1-17.3c-9.2-4.5-15.8-9-20.7-13.9l-2.8-2.9		l-0.3-3.8c-0.5-6.6-0.4-17,0.4-20.7c0.5-3.1,1.6-6.1,3.1-8.8c0.8-1,0.8-1,4.4-1c4.4,0,8.1,0.7,14.1,2.6c12.3,3.8,30.6,13,52.9,26.4		c18.4,11,25.3,16.4,30.6,23.8C1211.1,573.5,1213.5,580,1212.6,582z M1448.2,548.9c-0.3,1.6-5,6.4-9.7,9.9		c-13.8,10.1-38.5,19.5-63.6,24c-14.3,2.6-28.6,3-32.9,0.9c-2.8-1.4-3.2-2.5-2.2-6.1c2.1-7.5,8.9-15.7,19.5-23.4		c5.4-3.9,27.3-17.2,37.5-22.8c16.8-9.2,30.4-14.7,39.6-16c2.9-0.4,6.4-0.5,7.5,0c1.8,0.7,3.8,6,4.6,12.4		C1448.9,531.2,1448.7,546.2,1448.2,548.9z M1431.6,373.2l-0.2-0.1c-16.8-13.4-29.4-19.7-29.4-19.7c66.1-76,107-80.2,107-80.2		c4.5,6.1,7.4,13.9,9,22.9c0,0,0,0,0,0c9.3,53.4-26.8,147-26.8,147C1476.1,416.2,1455.8,392.4,1431.6,373.2L1431.6,373.2z\"/>	<path id=\"Path_8583\" class=\"st2\" d=\"M1377.6,343.2c-0.2,0.2-0.3,0.5-0.5,0.7\"/>	<path id=\"Path_8584\" class=\"st2\" d=\"M1509.1,476.9c-0.5,1.4-0.7,2.1-0.7,2.1\"/>	<g id=\"Group_934\">		<g id=\"Group_933\">			<g id=\"Group_932\">				<path id=\"Path_8585\" class=\"st4\" d=\"M1518.1,296.1c-7.8-1-44.7-1.2-86.5,77.1l-0.2-0.1c-16.8-13.4-29.4-19.7-29.4-19.7					c66.1-76,107-80.2,107-80.2C1513.6,279.3,1516.5,287.1,1518.1,296.1z\"/>			</g>			<path id=\"Path_8586\" class=\"st5\" d=\"M1491.3,443.1c-15.2-27-35.4-50.7-59.7-69.9c41.8-78.3,78.6-78.1,86.5-77.1c0,0,0,0,0,0				C1527.4,349.6,1491.3,443.1,1491.3,443.1z\"/>			<path id=\"Path_8587\" class=\"st5\" d=\"M1519.6,296.4c-0.5-0.1-1-0.2-1.5-0.3L1519.6,296.4z\"/>		</g>	</g>	<path id=\"Path_8588\" class=\"st2\" d=\"M1175.1,347.1c-1-1.4-2.1-2.8-3.1-4.2\"/>	<path id=\"Path_8589\" class=\"st2\" d=\"M1035,480.8c1.1,3.4,1.8,5.2,1.8,5.2\"/>	<g id=\"Group_937\">		<g id=\"Group_936\">			<g id=\"Group_935\">				<path id=\"Path_8590\" class=\"st4\" d=\"M1146.2,353.4c0,0-13,6.3-30.4,19.7c0,0-0.1,0.1-0.2,0.1c-43.3-78.4-81.4-78.1-89.6-77.1					c1.6-9,4.6-16.9,9.3-22.9C1035.4,273.2,1077.8,277.4,1146.2,353.4z\"/>			</g>			<path id=\"Path_8591\" class=\"st5\" d=\"M1115.7,373.2c-19,14.6-43.2,37.5-61.8,69.9c0,0-37.4-93.5-27.8-147c0,0,0,0,0,0				C1034.2,295.1,1072.4,294.9,1115.7,373.2z\"/>			<path id=\"Path_8592\" class=\"st5\" d=\"M1026.1,296.1c-0.5,0.1-1.1,0.2-1.6,0.3L1026.1,296.1z\"/>		</g>	</g>	<path id=\"Path_8593\" class=\"st1\" d=\"M1390.1,495c0,0-32,2-28-23s29-28,36-27s35,11,30,32s-12,17-16,18S1390.1,495,1390.1,495z\"/>	<path id=\"Path_8594\" class=\"st1\" d=\"M1154.1,495c0,0-32,2-28-23s29-28,36-27s35,11,30,32s-12,17-16,18S1154.1,495,1154.1,495z\"/></g></svg>", "Shiba Inu", "SHIB" },
                    { 7, "\r\n<svg width=\"32\" height=\"32\" viewBox=\"0 0 32 32\" xmlns=\"http://www.w3.org/2000/svg\"><g fill=\"none\"><circle fill=\"#6F41D8\" cx=\"16\" cy=\"16\" r=\"16\"/><path d=\"M21.092 12.693c-.369-.215-.848-.215-1.254 0l-2.879 1.654-1.955 1.078-2.879 1.653c-.369.216-.848.216-1.254 0l-2.288-1.294c-.369-.215-.627-.61-.627-1.042V12.19c0-.431.221-.826.627-1.042l2.25-1.258c.37-.216.85-.216 1.256 0l2.25 1.258c.37.216.628.611.628 1.042v1.654l1.955-1.115v-1.653a1.16 1.16 0 00-.627-1.042l-4.17-2.372c-.369-.216-.848-.216-1.254 0l-4.244 2.372A1.16 1.16 0 006 11.076v4.78c0 .432.221.827.627 1.043l4.244 2.372c.369.215.849.215 1.254 0l2.879-1.618 1.955-1.114 2.879-1.617c.369-.216.848-.216 1.254 0l2.251 1.258c.37.215.627.61.627 1.042v2.552c0 .431-.22.826-.627 1.042l-2.25 1.294c-.37.216-.85.216-1.255 0l-2.251-1.258c-.37-.216-.628-.611-.628-1.042v-1.654l-1.955 1.115v1.653c0 .431.221.827.627 1.042l4.244 2.372c.369.216.848.216 1.254 0l4.244-2.372c.369-.215.627-.61.627-1.042v-4.78a1.16 1.16 0 00-.627-1.042l-4.28-2.409z\" fill=\"#FFF\"/></g></svg>", "Polygon", "MATIC" },
                    { 8, "<svg version=\"1.1\" id=\"Layer_1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"0px\" y=\"0px\"	 viewBox=\"0 0 1000 1000\" style=\"enable-background:new 0 0 1000 1000;\" xml:space=\"preserve\"><style type=\"text/css\">	.st0{fill-rule:evenodd;clip-rule:evenodd;fill:#0055D5;}	.st1{fill-rule:evenodd;clip-rule:evenodd;fill:#FFFFFF;}</style><g>	<g>		<g>			<path id=\"SVGID_1_\" class=\"st0\" d=\"M500,0c276.1,0,500,223.9,500,500s-223.9,500-500,500S0,776.1,0,500S223.9,0,500,0L500,0z\"/>		</g>	</g>	<g>		<g>			<path id=\"SVGID_3_\" class=\"st1\" d=\"M272,330.8c0,0-53.8-43-57.5,63.2c-3.7,82.3-3.6,242.1,34.5,317.6				c16.1,28.4,37.6,19.7,77.6-8.6c34.8-25.7,160.7-141.7,214-192.6S702,313.5,724.5,412.7c8.9,41.7,19.9,77.1,11.5,169.6				c-2.6,28.5-14.8,50.1-70.4,11.5c-42-27-81.9-56-81.9-56s-7.7-13.8-25.9,8.6c-14.3,17.9-21.5,22.3,1.4,35.9				c23,13.6,139.3,93.8,183.9,106.3c36.8,10.7,57.8,10.5,58.9-61.8c-0.2-57.4-3.5-221.7-24.4-291.7c-15.8-43.6-39-63.4-89.1-14.4				S480.4,522.9,394,592.3c-57.3,43-81.8,81.8-103.4,10.1c-15.2-54.3-34.9-223.3,27.3-191.1c38.9,18.6,58.3,18.5,43.1,30.2				c-15.2,11.6-58.9,51.7-58.9,51.7s-13.5,10.9-7.2,34.5c6.3,16.5,6.3,24.7,40.2-7.2s58.9-56,58.9-56s5.4-8.5,21.5,7.2				s18.7,20.1,18.7,20.1s4.3,11.7,21.5-8.6c17.3-20.3,28.4-32.7-4.3-51.7C418.8,412.3,272,330.8,272,330.8L272,330.8z\"/>		</g>	</g></g></svg>", "Axie-Infinity", "AXS" },
                    { 9, "<svg width=\"32\" height=\"32\" viewBox=\"0 0 32 32\" xmlns=\"http://www.w3.org/2000/svg\"><defs><filter id=\"a\"><feColorMatrix in=\"SourceGraphic\" values=\"0 0 0 0 1.000000 0 0 0 0 1.000000 0 0 0 0 1.000000 0 0 0 1.000000 0\"/></filter></defs><g fill=\"none\" fill-rule=\"evenodd\"><circle fill=\"#FF2D55\" fill-rule=\"nonzero\" cx=\"16\" cy=\"16\" r=\"16\"/><g filter=\"url(#a)\"><path d=\"M12.793 11.534l-7.045 8.454A10.912 10.912 0 015 16C5 9.923 9.923 5 16 5c6.078 0 11 4.923 11 11 0 3.36-1.507 6.369-3.883 8.387H8.883A11.511 11.511 0 017.2 22.6h12.562v-4.763l3.965 4.763H24.8l-5.043-6.05-1.392 1.672-5.571-6.688zM19.758 9.4a2.751 2.751 0 000 5.5 2.751 2.751 0 000-5.5zm-6.963-1.991a1.376 1.376 0 100 2.751 1.376 1.376 0 000-2.751zM9.989 25.212h12.023A10.97 10.97 0 0116 27a10.97 10.97 0 01-6.011-1.788zm7.843-6.346l-2.426 2.909H6.639a11.056 11.056 0 01-.891-1.787h7.046V12.82l5.038 6.045z\" fill=\"#16141A\" fill-rule=\"nonzero\"/></g></g></svg>", "Decentraland", "MANA" },
                    { 10, "<svg version=\"1.1\" id=\"Layer_1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"0px\" y=\"0px\"	 viewBox=\"0 0 31.8 31.8\" style=\"enable-background:new 0 0 31.8 31.8;\" xml:space=\"preserve\"><style type=\"text/css\">	.st0{fill:none;stroke:#222326;stroke-width:2.5;}	.st1{fill:#222326;}</style><circle class=\"st0\" cx=\"15.9\" cy=\"15.9\" r=\"14.7\"/><path class=\"st1\" d=\"M18.7,21.2c-0.1-0.1-0.1-0.3-0.2-0.5c0-0.2-0.1-0.4-0.1-0.6c-0.2,0.2-0.4,0.3-0.6,0.5c-0.2,0.2-0.5,0.3-0.7,0.4	c-0.3,0.1-0.5,0.2-0.9,0.3c-0.3,0.1-0.7,0.1-1,0.1c-0.6,0-1.1-0.1-1.6-0.3c-0.5-0.2-0.9-0.4-1.3-0.7c-0.4-0.3-0.6-0.7-0.8-1.1	c-0.2-0.4-0.3-0.9-0.3-1.4c0-1.2,0.5-2.2,1.4-2.8c0.9-0.7,2.3-1,4.1-1h1.7v-0.7c0-0.6-0.2-1-0.5-1.3c-0.4-0.3-0.9-0.5-1.6-0.5	c-0.6,0-1,0.1-1.3,0.4c-0.3,0.3-0.4,0.6-0.4,1h-3c0-0.5,0.1-1,0.3-1.4c0.2-0.4,0.5-0.8,1-1.2c0.4-0.3,0.9-0.6,1.5-0.8	c0.6-0.2,1.3-0.3,2.1-0.3c0.7,0,1.3,0.1,1.9,0.3c0.6,0.2,1.1,0.4,1.6,0.8c0.4,0.3,0.8,0.8,1,1.3c0.2,0.5,0.4,1.1,0.4,1.8v5	c0,0.6,0,1.1,0.1,1.5c0.1,0.4,0.2,0.8,0.3,1v0.2H18.7z M15.8,19.1c0.3,0,0.6,0,0.8-0.1c0.3-0.1,0.5-0.2,0.7-0.3	c0.2-0.1,0.4-0.2,0.5-0.4c0.1-0.1,0.3-0.3,0.4-0.4v-2h-1.5c-0.5,0-0.9,0-1.2,0.1c-0.3,0.1-0.6,0.2-0.8,0.4c-0.2,0.2-0.4,0.3-0.5,0.6	c-0.1,0.2-0.1,0.5-0.1,0.7c0,0.4,0.1,0.7,0.4,1C14.8,19,15.3,19.1,15.8,19.1z\"/></svg>", "Arweave", "AR" },
                    { 11, "<svg width=\"32\" height=\"32\" viewBox=\"0 0 32 32\" xmlns=\"http://www.w3.org/2000/svg\"><g fill=\"none\" fill-rule=\"evenodd\"><circle fill=\"#E84142\" fill-rule=\"nonzero\" cx=\"16\" cy=\"16\" r=\"16\"/><path d=\"M11.518 22.75H8.49c-.636 0-.95 0-1.142-.123A.77.77 0 017 22.025c-.012-.226.145-.503.46-1.055l7.472-13.193c.318-.56.48-.84.682-.944a.77.77 0 01.698 0c.203.104.364.384.682.944l1.536 2.686.008.014c.343.6.517.906.593 1.226a2.26 2.26 0 010 1.066c-.076.323-.249.63-.597 1.24l-3.926 6.95-.01.017c-.346.606-.52.913-.764 1.145a2.284 2.284 0 01-.93.54c-.319.089-.675.089-1.387.089zm7.643 0h4.336c.64 0 .962 0 1.154-.126a.768.768 0 00.348-.607c.011-.219-.142-.484-.443-1.005l-.032-.054-2.172-3.722-.025-.042c-.305-.517-.46-.778-.657-.879a.762.762 0 00-.693 0c-.2.104-.36.377-.678.925l-2.165 3.722-.007.013c-.317.548-.476.821-.464 1.046a.777.777 0 00.348.606c.188.123.51.123 1.15.123z\" fill=\"#FFF\"/></g></svg>", "Avalanche", "AVAX" },
                    { 12, "<svg width=\"32\" height=\"32\" viewBox=\"0 0 32 32\" xmlns=\"http://www.w3.org/2000/svg\"><g fill=\"none\"><circle fill=\"#E6007A\" cx=\"16\" cy=\"16\" r=\"16\"/><path d=\"M16.272 6.625c-3.707 0-6.736 3.012-6.736 6.736 0 .749.124 1.48.356 2.192a.95.95 0 001.194.589.95.95 0 00.588-1.194 4.745 4.745 0 01-.267-1.73c.071-2.512 2.103-4.58 4.616-4.704a4.86 4.86 0 015.115 4.847 4.862 4.862 0 01-4.58 4.848s-.945.053-1.408.125c-.232.035-.41.071-.535.089-.054.018-.107-.036-.09-.09l.161-.783.873-4.028a.934.934 0 00-.712-1.105.934.934 0 00-1.105.713s-2.103 9.802-2.121 9.909a.934.934 0 00.713 1.105.934.934 0 001.105-.713c.017-.107.303-1.408.303-1.408a2.367 2.367 0 011.996-1.854 21.43 21.43 0 011.051-.089 6.744 6.744 0 006.22-6.719c0-3.724-3.03-6.736-6.737-6.736zm.481 15.505a1.122 1.122 0 00-1.336.873c-.125.606.25 1.212.873 1.337a1.122 1.122 0 001.337-.874c.124-.623-.25-1.212-.874-1.336z\" fill=\"#FFF\"/></g></svg>", "Polkadot", "DOT" },
                    { 13, "<svg width=\"32\" height=\"32\" viewBox=\"0 0 32 32\" xmlns=\"http://www.w3.org/2000/svg\"><g fill=\"none\"><circle fill=\"#3E73C4\" cx=\"16\" cy=\"16\" r=\"16\"/><g fill=\"#FFF\"><path d=\"M20.022 18.124c0-2.124-1.28-2.852-3.84-3.156-1.828-.243-2.193-.728-2.193-1.578 0-.85.61-1.396 1.828-1.396 1.097 0 1.707.364 2.011 1.275a.458.458 0 00.427.303h.975a.416.416 0 00.427-.425v-.06a3.04 3.04 0 00-2.743-2.489V9.142c0-.243-.183-.425-.487-.486h-.915c-.243 0-.426.182-.487.486v1.396c-1.829.242-2.986 1.456-2.986 2.974 0 2.002 1.218 2.791 3.778 3.095 1.707.303 2.255.668 2.255 1.639 0 .97-.853 1.638-2.011 1.638-1.585 0-2.133-.667-2.316-1.578-.06-.242-.244-.364-.427-.364h-1.036a.416.416 0 00-.426.425v.06c.243 1.518 1.219 2.61 3.23 2.914v1.457c0 .242.183.425.487.485h.915c.243 0 .426-.182.487-.485V21.34c1.829-.303 3.047-1.578 3.047-3.217z\"/><path d=\"M12.892 24.497c-4.754-1.7-7.192-6.98-5.424-11.653.914-2.55 2.925-4.491 5.424-5.402.244-.121.365-.303.365-.607v-.85c0-.242-.121-.424-.365-.485-.061 0-.183 0-.244.06a10.895 10.895 0 00-7.13 13.717c1.096 3.4 3.717 6.01 7.13 7.102.244.121.488 0 .548-.243.061-.06.061-.122.061-.243v-.85c0-.182-.182-.424-.365-.546zm6.46-18.936c-.244-.122-.488 0-.548.242-.061.061-.061.122-.061.243v.85c0 .243.182.485.365.607 4.754 1.7 7.192 6.98 5.424 11.653-.914 2.55-2.925 4.491-5.424 5.402-.244.121-.365.303-.365.607v.85c0 .242.121.424.365.485.061 0 .183 0 .244-.06a10.895 10.895 0 007.13-13.717c-1.096-3.46-3.778-6.07-7.13-7.162z\"/></g></g></svg>", "USD Coin", "USDC" }
                });

            migrationBuilder.InsertData(
                table: "DataImportTypes",
                columns: new[] { "DataImportTypeId", "DataImportTypeName" },
                values: new object[,]
                {
                    { 1, "API" },
                    { 2, "CSV" }
                });

            migrationBuilder.InsertData(
                table: "Exchanges",
                columns: new[] { "ExchangeId", "ExchangeName" },
                values: new object[,]
                {
                    { 1, "Coinbase" },
                    { 2, "Coinbase Pro" },
                    { 3, "Crypto.com" },
                    { 4, "Etoro" },
                    { 5, "Binance" }
                });

            migrationBuilder.InsertData(
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeId", "TransactionTypeName" },
                values: new object[,]
                {
                    { 1, "Buy" },
                    { 2, "Sell" },
                    { 3, "Withdrawl" },
                    { 4, "Staking Reward" },
                    { 5, "Loan Interest" },
                    { 6, "Perk Reward" },
                    { 7, "Deposit" }
                });

            migrationBuilder.InsertData(
                table: "ExchangeTransactionTypes",
                columns: new[] { "ExchangeTransactionTypeId", "DataImportTypeId", "ExchangeId", "ExchangeTransactionTypeName", "TransactionTypeId" },
                values: new object[,]
                {
                    { 1, 2, 1, "Buy", 1 },
                    { 2, 2, 1, "Advanced Trade Buy", 1 },
                    { 3, 2, 1, "Sell", 2 },
                    { 4, 2, 1, "Rewards Income", 4 },
                    { 5, 2, 1, "Learning Reward", 6 },
                    { 6, 2, 3, "viban_purchase", 1 },
                    { 7, 2, 3, "reimbursement", 6 },
                    { 8, 2, 3, "referral_card_cashback", 6 },
                    { 9, 2, 3, "card_cashback_reverted", 6 },
                    { 10, 2, 2, "Buy", 1 },
                    { 11, 2, 4, "Open Position", 1 },
                    { 12, 2, 4, "Staking", 4 },
                    { 13, 2, 5, "Buy", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CryptoPrices_CryptoId",
                table: "CryptoPrices",
                column: "CryptoId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeTransactionTypes_DataImportTypeId",
                table: "ExchangeTransactionTypes",
                column: "DataImportTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeTransactionTypes_ExchangeId",
                table: "ExchangeTransactionTypes",
                column: "ExchangeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeTransactionTypes_TransactionTypeId",
                table: "ExchangeTransactionTypes",
                column: "TransactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AppUserId",
                table: "Transactions",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CryptoId",
                table: "Transactions",
                column: "CryptoId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ExchangeTransactionTypeId",
                table: "Transactions",
                column: "ExchangeTransactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FromExchangeId",
                table: "Transactions",
                column: "FromExchangeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ToExchangeId",
                table: "Transactions",
                column: "ToExchangeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CryptoPrices");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "Cryptos");

            migrationBuilder.DropTable(
                name: "ExchangeTransactionTypes");

            migrationBuilder.DropTable(
                name: "DataImportTypes");

            migrationBuilder.DropTable(
                name: "Exchanges");

            migrationBuilder.DropTable(
                name: "TransactionTypes");
        }
    }
}
