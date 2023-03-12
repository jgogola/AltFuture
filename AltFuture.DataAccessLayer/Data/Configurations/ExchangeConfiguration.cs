using AltFuture.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AltFuture.DataAccessLayer.Data.Configurations
{
    public class ExchangeConfiguration : IEntityTypeConfiguration<Exchange>
    {
        public void Configure(EntityTypeBuilder<Exchange> builder)
        {
            builder.HasData
            (
                new Exchange
                {
                    ExchangeId = 1,
                    ExchangeName = "Coinbase"
                },
                new Exchange
                {
                    ExchangeId = 2,
                    ExchangeName = "Crypto.com"
                },
                new Exchange
                {
                    ExchangeId = 3,
                    ExchangeName = "Kucoin"
                },
                new Exchange
                {
                    ExchangeId = 4,
                    ExchangeName = "Etoro"
                },
                new Exchange
                {
                    ExchangeId = 5,
                    ExchangeName = "CDC Defi Wallet"
                },
                new Exchange
                {
                    ExchangeId = 6,
                    ExchangeName = "Yoroi Wallet"
                }
            );
        }
    }
}
