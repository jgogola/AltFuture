using AltFuture.DataAccessLayer.Data;
using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.Models;
using Microsoft.EntityFrameworkCore;

namespace AltFuture.DataAccessLayer.Repository
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
