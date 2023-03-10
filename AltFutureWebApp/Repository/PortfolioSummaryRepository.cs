using AltFutureWebApp.Data;
using AltFutureWebApp.Interfaces;
using AltFutureWebApp.Models.StoredProcs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace AltFutureWebApp.Repository
{
    public class PortfolioSummaryRepository : IPortfolioSummaryRepository
    {
        private readonly AppDbContext _db;

        public PortfolioSummaryRepository(AppDbContext dbContext)
        {
            _db = dbContext;
        }
        public async Task<IEnumerable<PortfolioSummaryGetAll>> GetAllAsync()
        {
            FormattableString sql = $"EXEC dbo.PortfolioSummaryGetAll";
            return await _db.PortfolioSummaries.FromSql(sql).ToListAsync();
        }
    }
}
