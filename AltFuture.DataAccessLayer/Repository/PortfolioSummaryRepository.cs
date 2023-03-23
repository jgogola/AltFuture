using AltFuture.DataAccessLayer.Data;
using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.DataAccessLayer.Models.StoredProcs;
using Microsoft.EntityFrameworkCore;

namespace AltFuture.DataAccessLayer.Repository
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
