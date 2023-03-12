using AltFuture.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AltFuture.DataAccessLayer.Data.Configurations
{
    public class CryptoConfiguration : IEntityTypeConfiguration<Crypto>
    {
        public void Configure(EntityTypeBuilder<Crypto> builder)
        {
            builder.HasData
            (
                new Crypto
                {
                    CryptoId = 1,
                    CryptoName = "Bitcoin",
                    TickerSymbol = "BTC"
                },
                new Crypto
                {
                    CryptoId = 2,
                    CryptoName = "Cardano",
                    TickerSymbol = "ADA"
                },
                new Crypto
                {
                    CryptoId = 3,
                    CryptoName = "Ethereum",
                    TickerSymbol = "ETH"
                },
                new Crypto
                {
                    CryptoId = 4,
                    CryptoName = "Bianance",
                    TickerSymbol = "BNB"
                },
                new Crypto
                {
                    CryptoId = 5,
                    CryptoName = "Crypto.com",
                    TickerSymbol = "CRO"
                },
                new Crypto
                {
                    CryptoId = 6,
                    CryptoName = "Shiba Inu",
                    TickerSymbol = "SHIB"
                },
                new Crypto
                {
                    CryptoId = 7,
                    CryptoName = "Polygon",
                    TickerSymbol = "MATIC"
                },
                new Crypto
                {
                    CryptoId = 8,
                    CryptoName = "Axie-Infinity",
                    TickerSymbol = "AXS"
                },
                new Crypto
                {
                    CryptoId = 9,
                    CryptoName = "Decentraland",
                    TickerSymbol = "MANA"
                },
                new Crypto
                {
                    CryptoId = 10,
                    CryptoName = "Arweave",
                    TickerSymbol = "AR"
                },
                new Crypto
                {
                    CryptoId = 11,
                    CryptoName = "Avalanche",
                    TickerSymbol = "AVAX"
                },
                new Crypto
                {
                    CryptoId = 12,
                    CryptoName = "Polkadot",
                    TickerSymbol = "DOT"
                }
            );
        }
    }
}
