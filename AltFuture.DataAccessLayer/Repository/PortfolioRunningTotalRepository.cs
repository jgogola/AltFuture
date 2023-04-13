using AltFuture.DataAccessLayer.Data;
using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.DataAccessLayer.Models.StoredProcs;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;

namespace AltFuture.DataAccessLayer.Repository
{
    public class PortfolioRunningTotalRepository : IPortfolioRunningTotalRepository
    {
        private readonly AppDbContext _db;

        public PortfolioRunningTotalRepository(AppDbContext dbContext)
        {
            _db = dbContext;
        }
        public async Task<IEnumerable<PortfolioRunningTotalByMonth>> GetByMonthAsync(int appUserId)
        {
            FormattableString sql = $"EXEC dbo.PortfolioRunningTotalByMonth @AppUserId = {appUserId}";
            try
            {
                var result = await _db.PortfolioRunningTotalByMonths.FromSql(sql).ToListAsync();
                return result;
            }
            catch (SqlNullValueException)
            {
                return Enumerable.Empty<PortfolioRunningTotalByMonth>();
            }
        }


        public async Task<IEnumerable<PortfolioRunningTotalByDay>> GetByDayAsync(int appUserId)
        {
            FormattableString sql = $"EXEC dbo.PortfolioRunningTotalByDay @AppUserId = {appUserId}";
            try
            {
                var result = await _db.PortfolioRunningTotalByDays.FromSql(sql).ToListAsync();
                return result;
            }
            catch (SqlNullValueException)
            {
                return Enumerable.Empty<PortfolioRunningTotalByDay>();
            }
        }

    }
}
