using AltFuture.DataAccessLayer.Data;
using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace AltFuture.DataAccessLayer.Repository;

public class ExchangeApiProfileRepository : IExchangeApiProfileRepository
{
    private readonly AppDbContext _context;

    public ExchangeApiProfileRepository(AppDbContext context)
    {
        _context = context;
    }

    // Get All by UserId
    public IEnumerable<ExchangeApiProfile> GetAllByUserId(int appUserId)
    {
        return  _context.ExchangeApiProfiles.Where(ea => ea.AppUserId == appUserId).AsEnumerable();
    }

    // Get All by UserId and ExchangeId
    public IEnumerable<ExchangeApiProfile> GetAllByUserIdExchangeId(int appUserId, int exchangeId)
    {
        return _context.ExchangeApiProfiles.Where(ea => ea.AppUserId == appUserId & ea.ExchangeId == exchangeId).AsEnumerable();
    }

    // Get By Id
    public async Task<ExchangeApiProfile> GetByIdAsync(int exchangeApiId)
    {
        return await _context.ExchangeApiProfiles.FirstOrDefaultAsync(ea => ea.ExchangeApiProfileId == exchangeApiId);
    }

    // Add ExchangeApiProfile to database
    public bool Add(ExchangeApiProfile exchangeApiProfile)
    {
        _context.ExchangeApiProfiles.Add(exchangeApiProfile);
        return Save();
    }

    // Update ExchangeApiProfile in database
    public bool Update(ExchangeApiProfile exchangeApiProfile)
    {
        _context.ExchangeApiProfiles.Update(exchangeApiProfile);
        return Save();
    }

    // Delete ExchangeApiProfile from database
    public bool Delete(ExchangeApiProfile exchangeApiProfile)
    {
        _context.ExchangeApiProfiles.Remove(exchangeApiProfile);
        return Save();
    }

    // Save()
    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0;
    }
}
