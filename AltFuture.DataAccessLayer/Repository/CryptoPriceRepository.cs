﻿using AltFuture.DataAccessLayer.Data;
using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace AltFuture.DataAccessLayer.Repository
{
    public class CryptoPriceRepository : ICryptoPriceRepository
    {
        private readonly AppDbContext _context;

        public CryptoPriceRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<CryptoPrice>> GetAllAsync()
        {
            return await _context.CryptoPrices.ToListAsync();
        }

        public async Task<IEnumerable<CryptoPrice>> GetLatestPricesAsync()
        {
            return await _context.CryptoPrices
                                 .Include(c => c.Crypto)
                                 .GroupBy(cp => cp.CryptoId)
                                 .Select(g => g.OrderByDescending(cp => cp.DateRecorded).First())
                                 .ToListAsync();
        }

        public async Task<DateTime> GetLastSyncedDate()
        {
            return await _context.CryptoPrices.MaxAsync(c => c.DateRecorded);
        }

        public async Task<CryptoPrice> GetByIdAsync(int id)
        {

            return await _context.CryptoPrices.FirstOrDefaultAsync(c => c.CryptoPriceId == id);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.CryptoPrices.CountAsync();
        }


        public bool Add(CryptoPrice cryptoPrice)
        {
            _context.Add(cryptoPrice);
            return Save();
        }

        public async Task<bool> AddRangeAsync(IEnumerable<CryptoPrice> cryptoPrices)
        {
            await _context.AddRangeAsync(cryptoPrices);
            return Save();
        }

        public bool Update(CryptoPrice cryptoPrice)
        {
            _context.Update(cryptoPrice);
            return Save();
        }

        public bool Delete(CryptoPrice cryptoPrice)
        {
            _context.Remove(cryptoPrice);
            return Save();
        }


        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }


    }
}
