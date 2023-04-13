using AltFuture.DataAccessLayer.Data;
using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.DataAccessLayer.Models.StoredProcs;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;

namespace AltFuture.DataAccessLayer.Repository
{
    public class PortfolioSummaryRepository : IPortfolioSummaryRepository
    {
        private readonly AppDbContext _db;

        public PortfolioSummaryRepository(AppDbContext dbContext)
        {
            _db = dbContext;
        }
        public async Task<IEnumerable<PortfolioSummary>> GetAllAsync(int appUserId)
        {
            FormattableString sql = $"EXEC dbo.PortfolioSummary @AppUserId = {appUserId}";
            try
            {
                var result = await _db.PortfolioSummaries.FromSql(sql).ToListAsync();
                return result;
            }
            catch (SqlNullValueException)
            {
                return Enumerable.Empty<PortfolioSummary>();
            }
        }
    }
}
