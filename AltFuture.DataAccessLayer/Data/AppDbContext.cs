using AltFuture.DataAccessLayer.Data.Configurations;
using AltFuture.Models;
using AltFuture.Models.StoredProcs;
using Microsoft.EntityFrameworkCore;

namespace AltFuture.DataAccessLayer.Data
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Crypto> Cryptos { get; set; }
        public DbSet<Exchange> Exchanges { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        public DbSet<ExchangeTransactionType> ExchangeTransactionTypes { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<CryptoPrice> CryptoPrices { get; set; }

        // Note: All Stored Procs and Views are defined in partial class file: AppDbContextStoredProcs.cs


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CryptoConfiguration());
            modelBuilder.ApplyConfiguration(new ExchangeConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ExchangeTransactionTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());

            modelBuilder.Entity<PortfolioSummaryGetAll>(entity =>
            {
                entity.HasNoKey().ToTable("PortfolioSummaryGetAll", t => t.ExcludeFromMigrations());
            });
        }

    }
}
