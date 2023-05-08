using AltFuture.DataAccessLayer.Data;
using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace AltFuture.DataAccessLayer.Repository;

public class ExchangeApiRepository : IExchangeApiRepository
{
    private readonly AppDbContext _context;

    public ExchangeApiRepository(AppDbContext context)
    {
        _context = context;
    }

    // Get All by UserId and ExchangeId
    public async Task<IEnumerable<ExchangeApi>> GetAllByUserIdExchangeIdAsync(int appUserId, int exchangeId)
    {
        return await _context.ExchangeApis.Where(ea => ea.AppUserId == appUserId & ea.ExchangeId == exchangeId).ToListAsync();
    }

    // Get By Id
    public async Task<ExchangeApi> GetByIdAsync(int exchangeApiId)
    {
        return await _context.ExchangeApis.FirstOrDefaultAsync(ea => ea.ExchangeApiId == exchangeApiId);
    }

    // Add ExchangeApi to database
    public bool Add(ExchangeApi exchangeApi)
    {
        _context.Add(exchangeApi);
        return Save();
    }

    // Update ExchangeApi in database
    public bool Update(ExchangeApi exchangeApi)
    {
        _context.Update(exchangeApi);
        return Save();
    }

    // Delete ExchangeApi from database
    public bool Delete(ExchangeApi exchangeApi)
    {
        _context.Remove(exchangeApi);
        return Save();
    }

    // Save()
    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0;
    }
}
