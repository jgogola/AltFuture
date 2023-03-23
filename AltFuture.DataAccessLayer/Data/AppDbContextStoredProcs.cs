using AltFuture.DataAccessLayer.Models.StoredProcs;
using Microsoft.EntityFrameworkCore;

namespace AltFuture.DataAccessLayer.Data
{
    public partial class AppDbContext 
    {
        // Define all Stored Proc and View Entities in this partial class. 

        public DbSet<PortfolioSummaryGetAll> PortfolioSummaries { get; set; }


    }
}
