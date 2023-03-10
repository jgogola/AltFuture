using AltFutureWebApp.Models;
using AltFutureWebApp.Models.StoredProcs;
using Microsoft.EntityFrameworkCore;

namespace AltFutureWebApp.Data
{
    public partial class AppDbContext : DbContext
    {
        public DbSet<PortfolioSummaryGetAll> PortfolioSummaries { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PortfolioSummaryGetAll>(entity => entity.HasNoKey());
        }

    }
}
