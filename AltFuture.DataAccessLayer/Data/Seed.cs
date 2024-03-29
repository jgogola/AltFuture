﻿using AltFuture.DataAccessLayer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AltFuture.DataAccessLayer.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                Console.WriteLine("*** Begin data seeding....");

                //Seed AppUser table:
                if (!context.AppUsers.Any())
                {
                    context.AppUsers.Add(new AppUser()
                    {
                        UserName = "JasonVoorhees"
                    });
                    context.SaveChanges();
                    Console.WriteLine("*** Added 1 new AppUser.");
                }
                else
                {
                    Console.WriteLine("*** AppUser records already exist. Did not seed.");
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
                        }


                    });
                    context.SaveChanges();
                    Console.WriteLine("Added range of new Cryptos.");
                }
                else
                {
                    Console.WriteLine("*** Crypto records already exist. Did not seed.");
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
                    context.SaveChanges();
                    Console.WriteLine("*** Added range of new Exhcanges.");
                }
                else
                {
                    Console.WriteLine("*** Exchange records already exist. Did not seed.");
                }//End Exchange seed

                //Seed ExchangeTransactionTypes table:
                if (!context.ExchangeTransactionTypes.Any())
                {
                    context.ExchangeTransactionTypes.AddRange(new List<ExchangeTransactionType>
                    {
                        //BUY
                        new ExchangeTransactionType
                        {
                            ExchangeTransactionTypeId = 1,
                            ExchangeTransactionTypeName = "Buy",
                            ExchangeId = 1, //Coinbase
                            TransactionTypeId = 1
                        },
                        new ExchangeTransactionType
                        {
                            ExchangeTransactionTypeId = 2,
                            ExchangeTransactionTypeName = "viban_purchase",
                            ExchangeId = 2, //Crypto.com
                            TransactionTypeId = 1
                        },
                        new ExchangeTransactionType
                        {
                            ExchangeTransactionTypeId = 3,
                            ExchangeTransactionTypeName = "Buy",
                            ExchangeId = 3, //Kucoin
                            TransactionTypeId = 1
                        },
                        new ExchangeTransactionType
                        {
                            ExchangeTransactionTypeId = 4,
                            ExchangeTransactionTypeName = "Open Position",
                            ExchangeId = 4, //Etoro
                            TransactionTypeId = 1
                        },
                        //SELL
                        new ExchangeTransactionType
                        {
                            ExchangeTransactionTypeId = 5,
                            ExchangeTransactionTypeName = "Sell",
                            ExchangeId = 1, //Coinbase
                            TransactionTypeId = 2
                        },
                        new ExchangeTransactionType
                        {
                            ExchangeTransactionTypeId = 6,
                            ExchangeTransactionTypeName = "Sell",
                            ExchangeId = 2, //Crypto.com
                            TransactionTypeId = 2
                        },
                        new ExchangeTransactionType
                        {
                            ExchangeTransactionTypeId = 7,
                            ExchangeTransactionTypeName = "Sell",
                            ExchangeId = 3, //Kucoin
                            TransactionTypeId = 2
                        },
                        new ExchangeTransactionType
                        {
                            ExchangeTransactionTypeId = 8,
                            ExchangeTransactionTypeName = "Sell",
                            ExchangeId = 4, //Etoro
                            TransactionTypeId = 2
                        }
                    });
                    context.SaveChanges();
                    Console.WriteLine("*** Added range of new ExchangeTransactionTypes.");
                }
                else
                {
                    Console.WriteLine("*** ExchangeTransactionTypes records already exist. Did not seed.");
                }//End ExchangeTransactionTypes seed

                //Seed Transaction table
                if (!context.Transactions.Any())
                {
                    context.Transactions.AddRange(new List<Transaction>
                    {
                        new AltFuture.DataAccessLayer.Models.Transaction()
                        {
                            TransactionReferenceNum = 1000,
                            AppUserId = 1,
                            CryptoId = 1,
                            ExchangeTransactionTypeId = 1,
                            Price = 23500.00m,
                            Quantity = 0.004m,
                            TransactionTotal = 97.00m,
                            Fee = 0.80m,
                            TransactionDate = DateTime.Now.AddDays(-2),
                            FromExchangeId = 1,
                            ToExchangeId = null,
                            CreatedDate = DateTime.Now
                        },
                        new AltFuture.DataAccessLayer.Models.Transaction()
                        {
                            TransactionReferenceNum = 1001,
                            AppUserId = 1,
                            CryptoId = 1,
                            ExchangeTransactionTypeId = 1,
                            Price = 23400.00m,
                            Quantity = 0.005m,
                            TransactionTotal = 117.00m,
                            Fee = 0.85m,
                            TransactionDate = DateTime.Now.AddDays(-1),
                            FromExchangeId = 1,
                            ToExchangeId = null,
                            CreatedDate = DateTime.Now
                        },
                        new AltFuture.DataAccessLayer.Models.Transaction()
                        {
                            TransactionReferenceNum = 1003,
                            AppUserId = 1,
                            CryptoId = 1,
                            ExchangeTransactionTypeId = 1,
                            Price = 23450.00m,
                            Quantity = 0.005m,
                            TransactionTotal = 117.25m,
                            Fee = 0.84m,
                            TransactionDate = DateTime.Now,
                            FromExchangeId = 1,
                            ToExchangeId = null,
                            CreatedDate = DateTime.Now
                        },
                        new AltFuture.DataAccessLayer.Models.Transaction()
                        {
                            TransactionReferenceNum = 2001,
                            AppUserId = 1,
                            CryptoId = 2,
                             ExchangeTransactionTypeId = 1,
                            Price = 0.35m,
                            Quantity = 100m,
                            TransactionTotal = 35m,
                            Fee = 0.50m,
                            TransactionDate = DateTime.Now.AddDays(-5),
                            FromExchangeId = 1,
                            ToExchangeId = null,
                            CreatedDate = DateTime.Now
                        },
                        new AltFuture.DataAccessLayer.Models.Transaction()
                        {
                            TransactionReferenceNum = 2002,
                            AppUserId = 1,
                            CryptoId = 2,
                            ExchangeTransactionTypeId = 1,
                            Price = 0.40m,
                            Quantity = 150m,
                            TransactionTotal = 60m,
                            Fee = 0.75m,
                            TransactionDate = DateTime.Now.AddDays(-2),
                            FromExchangeId = 1,
                            ToExchangeId = null,
                            CreatedDate = DateTime.Now
                        }

                    });
                    context.SaveChanges();
                    Console.WriteLine("*** Added range of new Transactions.");
                }
                else
                {
                    Console.WriteLine("*** Transaction records already exist. Did not seed.");
                }//End Transaction seed

                Console.WriteLine("*** Finished data seeding.");
            }
        }
    }
}
