﻿using AltFuture.DataAccessLayer.Data.Configurations;
using AltFuture.DataAccessLayer.Models;
using AltFuture.DataAccessLayer.Models.StoredProcs;
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
        public DbSet<ExchangeTransactionType> ExchangeTransactionTypes { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<CryptoPrice> CryptoPrices { get; set; }
        public DbSet<ExchangeApiProfile> ExchangeApiProfiles { get; set; }
        public DbSet<TransactionWithInvestmentTotals> TransactionWithInvestmentTotals { get; set; }
        public DbSet<ResortUnitWeek> ResortUnitWeeks { get; set; }


        // Note: All Stored Procs and Views are defined in partial class: AppDbContextStoredProcs.cs
        //       All Enum Entities are defined in partial class: AppDbContextEnums.cs


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Exchange>(entity =>
            {
                entity.HasKey(e => e.ExchangeId);
                entity.Property(e => e.ExchangeName).IsRequired();
            });

            modelBuilder.Entity<DataImportType>(entity =>
            {
                entity.HasKey(e => e.DataImportTypeId);
                entity.Property(e => e.DataImportTypeName).IsRequired();
            });

            modelBuilder.Entity<TransactionType>(entity =>
            {
                entity.HasKey(e => e.TransactionTypeId);
                entity.Property(e => e.TransactionTypeName).IsRequired();
            });

            modelBuilder.Entity<PortfolioSummary>(entity =>
            {
                entity.HasNoKey().ToTable("PortfolioSummaryGetAll", t => t.ExcludeFromMigrations());
            });


            modelBuilder.Entity<PortfolioRunningTotalByMonth>(entity =>
            {
                entity.HasNoKey().ToTable("PortfolioRunningTotalByMonth", t => t.ExcludeFromMigrations());
            });

            modelBuilder.Entity<PortfolioRunningTotalByDay>(entity =>
            {
                entity.HasNoKey().ToTable("PortfolioRunningTotalByDay", t => t.ExcludeFromMigrations());
            });

            //modelBuilder.Entity<Transaction>()
            //    .Property(t => t.InvestmentTotal)
            //    .HasComputedColumnSql("Price * Quantity");

            modelBuilder.Entity<ResortUnitWeek>(entity =>
            {
                entity.HasNoKey();
            });

            //* Matches to a read-only View in the database:
            modelBuilder.Entity<TransactionWithInvestmentTotals>().HasNoKey().ToView("v_TransactionWithInvestmentTotals");


            modelBuilder.ApplyConfiguration(new CryptoConfiguration());
            modelBuilder.ApplyConfiguration(new ExchangeConfiguration());
            modelBuilder.ApplyConfiguration(new DataImportTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ExchangeTransactionTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new TransactionConfiguration());


        }

    }
}
