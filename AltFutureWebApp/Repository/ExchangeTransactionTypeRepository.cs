using AltFutureWebApp.Areas.Portfolios.Models;
using AltFutureWebApp.Data;
using AltFutureWebApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AltFutureWebApp.Repository
{
    public class ExchangeTransactionTypeRepository : IExchangeTransactionTypeRepository
    {
        private readonly AppDbContext _context;

        public ExchangeTransactionTypeRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<ExchangeTransactionType>> GetAllAsync()
        {
            return await _context.ExchangeTransactionTypes.ToListAsync();
        }

        public async Task<ExchangeTransactionType> GetAsync(int id)
        {
            return await _context.ExchangeTransactionTypes.FirstOrDefaultAsync(e => e.ExchangeTransactionTypeId == id);
        }


        public bool Add(ExchangeTransactionType exchangeTransactionType)
        {
            _context.Add(exchangeTransactionType);
            return Save();
        }

        public bool Update(ExchangeTransactionType exchangeTransactionType)
        {
            _context.Update(exchangeTransactionType);
            return Save();
        }

        public bool Delete(ExchangeTransactionType exchangeTransactionType)
        {
            _context.Remove(exchangeTransactionType);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

    }
}
