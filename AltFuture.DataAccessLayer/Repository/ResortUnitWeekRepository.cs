using AltFuture.DataAccessLayer.Data;
using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace AltFuture.DataAccessLayer.Repository
{
    public class ResortUnitWeekRepository : IResortUnitWeekRepository
    {
        private readonly AppDbContext _context;

        public ResortUnitWeekRepository(AppDbContext context)
        {
            _context = context;
        }



        public IEnumerable<ResortUnitWeek> GetAllByRegionWeek(string regionCode, DateTime startDate, DateTime endDate)
        {
            return _context.ResortUnitWeeks.Where(w => w.RegionCode == regionCode && w.StartDate >= startDate && w.EndDate <= endDate)
                                                  .ToList();
        }


        public bool Add(Transaction transaction)
        {
            _context.Add(transaction);
            return Save();
        }

        public bool Update(Transaction transaction)
        {
            _context.Update(transaction);
            return Save();
        }

        public bool Delete(Transaction transaction)
        {
            _context.Remove(transaction);
            return Save();
        }


        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }


    }
}
