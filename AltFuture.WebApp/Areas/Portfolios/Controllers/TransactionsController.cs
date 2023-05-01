using AltFuture.BusinessLogicLayer.ViewModels.Transactions;
using AltFuture.DataAccessLayer.Data;
using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.DataAccessLayer.Models;
using AltFuture.WebApp.Enums;
using AltFuture.WebApp.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using X.PagedList;

namespace AltFuture.WebApp.Areas.Portfolios.Controllers
{
    [Area("Portfolios")]
    public class TransactionsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IExchangeTransactionTypeRepository _exchangeTransactionTypeRepository;
        private readonly IMapper _mapper;


        public TransactionsController(AppDbContext context, IExchangeTransactionTypeRepository exchangeTransactionTypeRepository, IMapper mapper)
        {
            _context = context;
            _exchangeTransactionTypeRepository = exchangeTransactionTypeRepository;
            _mapper = mapper;
        }

        // GET: Portfolios/Transactions
        public async Task<IActionResult> Index(int? page)
        {
            var userMessageJson = TempData["UserMessage"] as string;
            var transactions = _context.Transactions
                                       .Include(t => t.AppUser)
                                       .Include(t => t.Crypto)
                                       .Include(t => t.FromExchange)
                                       .Include(t => t.ToExchange)
                                       .OrderByDescending(t => t.TransactionDate);
            var pageNumber = page ?? 1;
            var pageSize = 10;
            var pagedTransactions = await transactions.ToPagedListAsync(pageNumber, pageSize);


            return View(pagedTransactions);
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
                .Include(t => t.ExchangeTransactionType)
                .Include(t => t.FromExchange)
                .Include(t => t.ToExchange)
                .FirstOrDefaultAsync(m => m.TransactionId == id);
            if (transaction == null)
            {
                return NotFound();
            }

            var transactionDetail = _mapper.Map<TransactionDetail>(transaction);

            return View(transactionDetail);
        }

        // GET: Portfolios/Transactions/Create
        public IActionResult Create()
        {

            ViewData["CryptoId"] = new SelectList(_context.Cryptos, "CryptoId", "CryptoName").Prepend(new SelectListItem
            {
                Value = "",
                Text = "Select a crypto currency..."
            });
            //ViewData["ExchangeTransactionTypeId"] = new SelectList(_context.ExchangeTransactionTypes, "ExchangeTransactionTypeId", "ExchangeTransactionTypeName").Prepend(new SelectListItem
            //{
            //    Value = "",
            //    Text = "Select a transaction type..."
            //});
            ViewData["FromExchangeId"] = new SelectList(_context.Exchanges, "ExchangeId", "ExchangeName").Prepend(new SelectListItem
            {
                Value = "",
                Text = "Select exchange transaction started from..."
            });
            ViewData["ToExchangeId"] = new SelectList(_context.Exchanges, "ExchangeId", "ExchangeName").Prepend(new SelectListItem
            {
                Value = "",
                Text = "Select exchange transaction transfered to..."
            }); 
            return View();
        }

        // POST: Portfolios/Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransactionReferenceNum,CryptoId,ExchangeTransactionTypeId,Price,Quantity,TransactionTotal,Fee,TransactionDate,FromExchangeId,ToExchangeId")] TransactionCreate transactionCreate)
        {
            var appUserId = 1;

            if (ModelState.IsValid)
            {
                var transaction = _mapper.Map<Transaction>(transactionCreate);
                transaction.CreatedDate = DateTime.Now;
                transaction.AppUserId = appUserId;

                 _context.Add(transaction);
                await _context.SaveChangesAsync();


                //* Display success message back to user on Dashboard Index
                var userMessagePartial = new UserMessagePartial(TempData);
                userMessagePartial.SetUserMessage(
                    UserMessageTypes.Success,
                    $"Your transaction was successfully created.",
                    8
                );

                return RedirectToAction(nameof(Index));
            }

            ViewData["CryptoId"] = new SelectList(_context.Cryptos, "CryptoId", "CryptoName", transactionCreate.CryptoId).Prepend(new SelectListItem
            {
                Value = "",
                Text = "Select a crypto currency..."
            });
            //ViewData["ExchangeTransactionTypeId"] = new SelectList(_context.ExchangeTransactionTypes, "ExchangeTransactionTypeId", "ExchangeTransactionTypeName").Prepend(new SelectListItem
            //{
            //    Value = "",
            //    Text = "Select a transaction type..."
            //});
            ViewData["FromExchangeId"] = new SelectList(_context.Exchanges, "ExchangeId", "ExchangeName", transactionCreate.FromExchangeId).Prepend(new SelectListItem
            {
                Value = "",
                Text = "Select exchange transaction started from..."
            }); 
            ViewData["ToExchangeId"] = new SelectList(_context.Exchanges, "ExchangeId", "ExchangeName", transactionCreate.ToExchangeId).Prepend(new SelectListItem
            {
                Value = "",
                Text = "Select exchange transaction transfered to..."
            }); 

            return View(transactionCreate);
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

            var transactionEdit = _mapper.Map<TransactionEdit>(transaction);

            ViewData["CryptoId"] = new SelectList(_context.Cryptos, "CryptoId", "CryptoName", transactionEdit.CryptoId);
            ViewData["FromExchangeId"] = new SelectList(_context.Exchanges, "ExchangeId", "ExchangeName", transactionEdit.FromExchangeId);
            ViewData["ToExchangeId"] = new SelectList(_context.Exchanges, "ExchangeId", "ExchangeName", transactionEdit.ToExchangeId).Prepend(new SelectListItem
            {
                Value = "0",
                Text = "Select an Exchange"
            });
            return View(transactionEdit);
        }

        // POST: Portfolios/Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransactionId,ExchangeTransactionTypeId,TransactionReferenceNum,AppUserId,CryptoId,Price,Quantity,TransactionTotal,Fee,TransactionDate,FromExchangeId,ToExchangeId,CreatedDate")] TransactionEdit transactionEdit)
        {
            if (id != transactionEdit.TransactionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var transaction = _mapper.Map<Transaction>(transactionEdit);
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();

                    //* Display success message back to user on Dashboard Index
                    var userMessagePartial = new UserMessagePartial(TempData);
                    userMessagePartial.SetUserMessage(
                        UserMessageTypes.Success,
                        $"Your transaction was successfully updated.",
                        8
                    );
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transactionEdit.TransactionId))
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
         //   ViewData["AppUserId"] = new SelectList(_context.AppUsers, "AppUserId", "AppUserId", transactionEdit.AppUserId);
            ViewData["CryptoId"] = new SelectList(_context.Cryptos, "CryptoId", "CryptoId", transactionEdit.CryptoId);
            ViewData["FromExchangeId"] = new SelectList(_context.Exchanges, "ExchangeId", "ExchangeId", transactionEdit.FromExchangeId);
            ViewData["ToExchangeId"] = new SelectList(_context.Exchanges, "ExchangeId", "ExchangeId", transactionEdit.ToExchangeId);
            return View(transactionEdit);
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
                .Include(t => t.ExchangeTransactionType)
                .Include(t => t.FromExchange)
                .Include(t => t.ToExchange)
                .FirstOrDefaultAsync(m => m.TransactionId == id);
            if (transaction == null)
            {
                return NotFound();
            }

            var transactionDetail = _mapper.Map<TransactionDetail>(transaction);

            return View(transactionDetail);
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

            //* Display success message back to user on Dashboard Index
            var userMessagePartial = new UserMessagePartial(TempData);
            userMessagePartial.SetUserMessage(
                UserMessageTypes.Success,
                $"Your transaction was successfully deleted.",
                8
            );

            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
          return _context.Transactions.Any(e => e.TransactionId == id);
        }


        [HttpGet]
        public async Task<JsonResult> GetExchangeTransactionTypesByExchangeId(int exchangeId)
        {
            var exchangeTransactionTypes = await _exchangeTransactionTypeRepository.GetAllByExchangeIdAsync(exchangeId);

            return Json(new SelectList(exchangeTransactionTypes, "ExchangeTransactionTypeId", "ExchangeTransactionTypeName"));

        }
    }
}
