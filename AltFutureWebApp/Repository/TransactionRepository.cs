﻿using AltFutureWebApp.Areas.Portfolios.Models;
using AltFutureWebApp.Data;
using AltFutureWebApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AltFutureWebApp.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }



        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _context.Transactions.ToListAsync();
        }

        public async Task<Transaction> GetAsync(int id)
        {
            return await _context.Transactions.FirstOrDefaultAsync(t => t.TransactionId == id);
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


    }
}
