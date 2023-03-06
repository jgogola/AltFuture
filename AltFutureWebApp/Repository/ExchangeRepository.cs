using AltFutureWebApp.Areas.Portfolios.Models;
using AltFutureWebApp.Data;
using AltFutureWebApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AltFutureWebApp.Repository
{
    public class ExchangeRepository : IExchangeRepository
    {
        private readonly AppDbContext _context;

        public ExchangeRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Exchange>> GetAllAsync()
        {
            return await _context.Exchanges.ToListAsync();
        }

        public async Task<Exchange> GetAsync(int id)
        {
            return await _context.Exchanges.FirstOrDefaultAsync(e => e.ExchangeId == id);
        }

        public bool Add(Exchange exchange)
        {
            _context.Add(exchange);
            return Save();
        }

        public bool Update(Exchange exchange)
        {
            _context.Update(exchange);
            return Save();
        }

        public bool Delete(Exchange exchange)
        {
            _context.Remove(exchange);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }


    }
}
