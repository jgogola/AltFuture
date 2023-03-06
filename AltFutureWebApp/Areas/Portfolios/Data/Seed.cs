using AltFutureWebApp.Areas.Portfolios.Models;
using AltFutureWebApp.Areas.Portfolios.Data.Enums;
using AltFutureWebApp.Data;
using System.Transactions;

namespace AltFutureWebApp.Areas.Portfolios.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                //Seed AppUser table:
                if(!context.AppUsers.Any())
                {
                    context.AppUsers.AddRange(new List<AppUser>
                    {
                        new AppUser()
                        {
                            UserName = "JasonVoorhees"
                        }
                    });
                }//End AppUser seed

                //Seed Crypto table:
                if (!context.Cryptos.Any())
                {
                    context.Cryptos.AddRange(new List<Crypto>()
                    {
                        new Crypto()
                        {
                            CryptoName = "Bitcoin",
                            TickerSymbol = "BTC"
                        },
                        new Crypto()
                        {
                            CryptoName = "Cardano",
                            TickerSymbol = "ADA"
                        },
                        new Crypto()
                        {
                            CryptoName = "Ethereum",
                            TickerSymbol = "ETH"
                        },
                        new Crypto()
                        {
                            CryptoName = "Bianance",
                            TickerSymbol = "BNB"
                        },
                        new Crypto()
                        {
                            CryptoName = "Crypto.com",
                            TickerSymbol = "CRO"
                        },
                        new Crypto()
                        {
                            CryptoName = "Shiba Inu",
                            TickerSymbol = "SHIB"
                        },
                        new Crypto()
                        {
                            CryptoName = "Polygon",
                            TickerSymbol = "MATIC"
                        },
                        new Crypto()
                        {
                            CryptoName = "Axie-Infinity",
                            TickerSymbol = "AXS"
                        },
                        new Crypto()
                        {
                            CryptoName = "Decentraland",
                            TickerSymbol = "MANA"
                        },
                        new Crypto()
                        {
                            CryptoName = "Arweave",
                            TickerSymbol = "AR"
                        },
                        new Crypto()
                        {
                            CryptoName = "Avalanche",
                            TickerSymbol = "AVAX"
                        },
                        new Crypto()
                        {
                            CryptoName = "Polkadot",
                            TickerSymbol = "DOT"
                        },


                    });
                }//End Crypto seed

                //Seed Exchange table:
                if (!context.Exchanges.Any())
                {
                    context.Exchanges.AddRange(new List<Exchange>
                    {
                        new Exchange()
                        {
                            ExchangeName = "Coinbase"
                        },
                        new Exchange()
                        {
                            ExchangeName = "Crypto.com"
                        },
                        new Exchange()
                        {
                            ExchangeName = "Kucoin"
                        },
                        new Exchange()
                        {
                            ExchangeName = "Etoro"
                        },
                        new Exchange()
                        {
                            ExchangeName = "CDC Defi Wallet"
                        },
                        new Exchange()
                        {
                            ExchangeName = "Yoroi Wallet"
                        }

                    });
                }//End Exchange seed

                //Seed ExchangeTransactionTypes table:
                if (!context.ExchangeTransactionTypes.Any())
                {
                    context.ExchangeTransactionTypes.AddRange(new List<ExchangeTransactionType>
                    {
                        //BUY
                        new ExchangeTransactionType() 
                        {
                            ExchangeTransactionTypeName = "Buy",
                            ExchageId = 1, //Coinbase
                            CommonTransactionType = CommonTransactionType.Buy
                        },
                        new ExchangeTransactionType() 
                        {
                            ExchangeTransactionTypeName = "viban_purchase",
                            ExchageId = 2, //Crypto.com
                            CommonTransactionType = CommonTransactionType.Buy
                        },
                        new ExchangeTransactionType()
                        {
                            ExchangeTransactionTypeName = "Buy",
                            ExchageId = 3, //Kucoin
                            CommonTransactionType = CommonTransactionType.Buy
                        },
                        new ExchangeTransactionType() 
                        {
                            ExchangeTransactionTypeName = "Open Position",
                            ExchageId = 4, //Etoro
                            CommonTransactionType = CommonTransactionType.Buy
                        },
                        //SELL
                        new ExchangeTransactionType() 
                        {
                            ExchangeTransactionTypeName = "Sell",
                            ExchageId = 1, //Coinbase
                            CommonTransactionType = CommonTransactionType.Sell
                        },
                        new ExchangeTransactionType() 
                        {
                            ExchangeTransactionTypeName = "Sell",
                            ExchageId = 2, //Crypto.com
                            CommonTransactionType = CommonTransactionType.Sell
                        },
                        new ExchangeTransactionType()
                        {
                            ExchangeTransactionTypeName = "Sell",
                            ExchageId = 3, //Kucoin
                            CommonTransactionType = CommonTransactionType.Sell
                        },
                        new ExchangeTransactionType() 
                        {
                            ExchangeTransactionTypeName = "Sell",
                            ExchageId = 4, //Etoro
                            CommonTransactionType = CommonTransactionType.Sell
                        }
                    });
                }//End ExchangeTransactionTypes seed

                //Seed Transaction table
                if(!context.Transactions.Any())
                {
                    context.Transactions.AddRange(new List<Models.Transaction>
                    {
                        new Models.Transaction()
                        {
                            TransactionReferenceNum = 1000,
                            AppUserId = 1,
                            CryptoId = 1,
                            Price = 23500.00m,
                            Quantity = 0.004m,
                            TransactionTotal = 97.00m,
                            Fee = 0.80m,
                            TransactionDate = DateTime.Now.AddDays(-2),
                            FromExchangeId = 1,
                            ToExchangeId = null,
                            CreatedDate = DateTime.Now                         
                        },
                        new Models.Transaction()
                        {
                            TransactionReferenceNum = 1001,
                            AppUserId = 1,
                            CryptoId = 1,
                            Price = 23400.00m,
                            Quantity = 0.005m,
                            TransactionTotal = 117.00m,
                            Fee = 0.85m,
                            TransactionDate = DateTime.Now.AddDays(-1),
                            FromExchangeId = 1,
                            ToExchangeId = null,
                            CreatedDate = DateTime.Now
                        },
                        new Models.Transaction()
                        {
                            TransactionReferenceNum = 1003,
                            AppUserId = 1,
                            CryptoId = 1,
                            Price = 23450.00m,
                            Quantity = 0.005m,
                            TransactionTotal = 117.25m,
                            Fee = 0.84m,
                            TransactionDate = DateTime.Now,
                            FromExchangeId = 1,
                            ToExchangeId = null,
                            CreatedDate = DateTime.Now
                        },
                        new Models.Transaction()
                        {
                            TransactionReferenceNum = 2001,
                            AppUserId = 1,
                            CryptoId = 2,
                            Price = 0.35m,
                            Quantity = 100m,
                            TransactionTotal = 35m,
                            Fee = 0.50m,
                            TransactionDate = DateTime.Now.AddDays(-5),
                            FromExchangeId = 1,
                            ToExchangeId = null,
                            CreatedDate = DateTime.Now
                        },
                        new Models.Transaction()
                        {
                            TransactionReferenceNum = 2002,
                            AppUserId = 1,
                            CryptoId = 2,
                            Price = 0.40m,
                            Quantity = 150m,
                            TransactionTotal = 60m,
                            Fee = 0.75m,
                            TransactionDate = DateTime.Now.AddDays(-2),
                            FromExchangeId = 1,
                            ToExchangeId = null,
                            CreatedDate = DateTime.Now
                        },

                    });
                }//End Transaction seed
            }
        }
    }
}
