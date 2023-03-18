using AltFuture.Models;
using AltFuture.Models.StoredProcs;
using Microsoft.EntityFrameworkCore;

namespace AltFuture.DataAccessLayer.Data
{
    public partial class AppDbContext 
    {
        // Define all Enum Entities in this partial class. 

        public DbSet<Exchange> Exchanges { get; set; }
        public DbSet<DataImportType> DataImportTypes { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }


    }
}
