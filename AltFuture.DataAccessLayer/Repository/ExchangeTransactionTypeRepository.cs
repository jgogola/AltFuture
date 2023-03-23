using AltFuture.DataAccessLayer.Data;
using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace AltFuture.DataAccessLayer.Repository
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

        public async Task<ExchangeTransactionType> GetByIdAsync(int id)
        {
            return await _context.ExchangeTransactionTypes.FirstOrDefaultAsync(e => e.ExchangeTransactionTypeId == id);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.ExchangeTransactionTypes.CountAsync();
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
