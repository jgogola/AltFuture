using AltFuture.DataAccessLayer.Data;
using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.Models;
using Microsoft.EntityFrameworkCore;

namespace AltFuture.DataAccessLayer.Repository
{
    public class CryptoRepository : ICryptoRepository
    {
        private readonly AppDbContext _context;

        public CryptoRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Crypto>> GetAllAsync()
        {
            return await _context.Cryptos.ToListAsync();
        }

        public async Task<Crypto?> GetByIdAsync(int id)
        {
            return await _context.Cryptos.FirstOrDefaultAsync(c => c.CryptoId == id);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Cryptos.CountAsync();
        }


        public bool Add(Crypto crypto)
        {
            _context.Add(crypto);
            return Save();
        }

        public bool Update(Crypto crypto)
        {
            _context.Update(crypto);
            return Save();
        }

        public bool Delete(Crypto crypto)
        {
            _context.Remove(crypto);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }


    }
}
