using AltFutureWebApp.Data;
using AltFutureWebApp.Interfaces;
using AltFutureWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AltFutureWebApp.Repository
{
    public class TransactionTypeRepository : ITransactionTypeRepository
    {
        private readonly AppDbContext _db;

        public TransactionTypeRepository(AppDbContext context)
        {
            _db = context;
        }


        public async Task<IEnumerable<TransactionType>> GetAllAsync()
        {
            return await _db.TransactionTypes.ToListAsync();
        }

        public async Task<TransactionType> GetByIdAsync(int id)
        {
            return await _db.TransactionTypes.FirstOrDefaultAsync(e => e.TransactionTypeId == id);
        }



    }
}
