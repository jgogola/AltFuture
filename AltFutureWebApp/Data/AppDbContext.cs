using AltFutureWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AltFutureWebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Crypto> Cryptos { get; set; }
        public DbSet<Exchange> Exchanges { get; set; }
        public DbSet<ExchangeTransactionType> ExchangeTransactionTypes { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<CryptoPrice> CryptoPrices { get; set; }

    }
}
