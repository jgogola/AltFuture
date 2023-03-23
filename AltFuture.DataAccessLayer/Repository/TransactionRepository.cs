using AltFuture.DataAccessLayer.Data;
using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace AltFuture.DataAccessLayer.Repository
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

        public async Task<List<Transaction>> GetAllForUserAsync(int userId)
        {
            return await _context.Transactions.Where(t => t.AppUserId == userId).ToListAsync();
        }

        public async Task<Transaction> GetByIdAsync(int id)
        {
            return await _context.Transactions.FirstOrDefaultAsync(t => t.TransactionId == id);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Transactions.CountAsync();
        }


        public async Task<int> GetCountForUserAsync(int userId)
        {
            
            return await _context.Transactions.Where(t => t.AppUserId == userId).CountAsync();
        }

        public async Task<bool> AddRangeAsync(IEnumerable<Transaction> transactions)
        {
            await _context.AddRangeAsync(transactions);
            return await SaveAsync();
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
