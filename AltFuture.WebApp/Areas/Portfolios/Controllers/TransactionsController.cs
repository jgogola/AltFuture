using AltFuture.DataAccessLayer.Data;
using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AltFuture.WebApp.Areas.Portfolios.Controllers
{
    [Area("Portfolios")]
    public class TransactionsController : Controller
    {
        private readonly AppDbContext _context;

        public TransactionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Portfolios/Transactions
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Transactions.Include(t => t.AppUser).Include(t => t.Crypto).Include(t => t.FromExchange).Include(t => t.ToExchange);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Portfolios/Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.AppUser)
                .Include(t => t.Crypto)
                .Include(t => t.FromExchange)
                .Include(t => t.ToExchange)
                .FirstOrDefaultAsync(m => m.TransactionId == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Portfolios/Transactions/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "AppUserId", "AppUserId");
            ViewData["CryptoId"] = new SelectList(_context.Cryptos, "CryptoId", "CryptoId");
            ViewData["FromExchangeId"] = new SelectList(_context.Exchanges, "ExchangeId", "ExchangeId");
            ViewData["ToExchangeId"] = new SelectList(_context.Exchanges, "ExchangeId", "ExchangeId");
            return View();
        }

        // POST: Portfolios/Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransactionId,TransactionReferenceNum,AppUserId,CryptoId,Price,Quantity,TransactionTotal,Fee,TransactionDate,FromExchangeId,ToExchangeId,CreatedDate")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "AppUserId", "AppUserId", transaction.AppUserId);
            ViewData["CryptoId"] = new SelectList(_context.Cryptos, "CryptoId", "CryptoId", transaction.CryptoId);
            ViewData["FromExchangeId"] = new SelectList(_context.Exchanges, "ExchangeId", "ExchangeId", transaction.FromExchangeId);
            ViewData["ToExchangeId"] = new SelectList(_context.Exchanges, "ExchangeId", "ExchangeId", transaction.ToExchangeId);
            return View(transaction);
        }

        // GET: Portfolios/Transactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "AppUserId", "AppUserId", transaction.AppUserId);
            ViewData["CryptoId"] = new SelectList(_context.Cryptos, "CryptoId", "CryptoId", transaction.CryptoId);
            ViewData["FromExchangeId"] = new SelectList(_context.Exchanges, "ExchangeId", "ExchangeId", transaction.FromExchangeId);
            ViewData["ToExchangeId"] = new SelectList(_context.Exchanges, "ExchangeId", "ExchangeId", transaction.ToExchangeId);
            return View(transaction);
        }

        // POST: Portfolios/Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransactionId,TransactionReferenceNum,AppUserId,CryptoId,Price,Quantity,TransactionTotal,Fee,TransactionDate,FromExchangeId,ToExchangeId,CreatedDate")] Transaction transaction)
        {
            if (id != transaction.TransactionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.TransactionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "AppUserId", "AppUserId", transaction.AppUserId);
            ViewData["CryptoId"] = new SelectList(_context.Cryptos, "CryptoId", "CryptoId", transaction.CryptoId);
            ViewData["FromExchangeId"] = new SelectList(_context.Exchanges, "ExchangeId", "ExchangeId", transaction.FromExchangeId);
            ViewData["ToExchangeId"] = new SelectList(_context.Exchanges, "ExchangeId", "ExchangeId", transaction.ToExchangeId);
            return View(transaction);
        }

        // GET: Portfolios/Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.AppUser)
                .Include(t => t.Crypto)
                .Include(t => t.FromExchange)
                .Include(t => t.ToExchange)
                .FirstOrDefaultAsync(m => m.TransactionId == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Portfolios/Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transactions == null)
            {
                return Problem("Entity set 'AppDbContext.Transactions'  is null.");
            }
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
          return _context.Transactions.Any(e => e.TransactionId == id);
        }
    }
}
