using AltFuture.DataAccessLayer.Models;

namespace AltFuture.DataAccessLayer.Interfaces
{
    public interface IResortUnitWeekRepository
    {

        IEnumerable<ResortUnitWeek> GetAllByRegionWeek(string regionCode, DateTime startDate, DateTime endDate);

        bool Add(Transaction transaction);

        bool Update(Transaction transaction);

        bool Delete(Transaction transaction);

        bool Save();

        Task<bool> SaveAsync();
    }
}
