using Microsoft.AspNetCore.Mvc;
using AltFuture.WebApp.Areas.Admin.ViewModels;
using AltFuture.WebApp.Helpers;
using AltFuture.WebApp.Enums;
using AltFuture.DataAccessLayer.Data;
using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.DataAccessLayer.Models;

namespace AltFuture.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CryptoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICryptoRepository _cryptoRepository;

        public CryptoController(AppDbContext context, ICryptoRepository cryptoRepository)
        {
            _context = context;
            _cryptoRepository = cryptoRepository;
        }

        // GET: Admin/Crypto
        public async Task<IActionResult> Index()
        {
            var userMessageJson = TempData["UserMessage"] as string;

            return View(await _cryptoRepository.GetAllAsync());
        }

        // GET: Admin/Crypto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crypto = await _cryptoRepository.GetByIdAsync(id ?? 0);

            if (crypto == null)
            {
                return NotFound();
            }

            return View(crypto);
        }

     
        // GET: Admin/Crypto/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Admin/Crypto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CryptoViewModel cryptoVM)
        {
            if (ModelState.IsValid)
            {
                var crypto = new Crypto()
                {
                    CryptoName = cryptoVM.CryptoName,
                    TickerSymbol = cryptoVM.TickerSymbol
                };

                _cryptoRepository.Add(crypto);

                //* Display success message back to user on Index
                var userMessagePartial = new UserMessagePartial(TempData);
                userMessagePartial.SetUserMessage(
                    UserMessageTypes.Success,
                    $"{crypto.CryptoName} was successfully added.",
                    8
                );

                return RedirectToAction(nameof(Index));
            }
            return View(cryptoVM);
        }

       
        
        // GET: Admin/Crypto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crypto = await _cryptoRepository.GetByIdAsync(id ?? 0);
            if (crypto == null)
            {
                return NotFound();
            }

            var cryptoVM = new CryptoViewModel()
            {
                CryptoId = crypto.CryptoId,
                CryptoName = crypto.CryptoName,
                TickerSymbol = crypto.TickerSymbol,
            };

            return View(cryptoVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,CryptoViewModel cryptoVM)
        {
            if (id != cryptoVM.CryptoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var crypto = new Crypto()
                {
                    CryptoId = cryptoVM.CryptoId,
                    CryptoName = cryptoVM.CryptoName,
                    TickerSymbol = cryptoVM.TickerSymbol
                };

                _cryptoRepository.Update(crypto);

                //* Display success message back to user on Index
                var userMessagePartial = new UserMessagePartial(TempData);
                userMessagePartial.SetUserMessage(
                    UserMessageTypes.Success,
                    $"{crypto.CryptoName} was successfully updated.",
                    8
                );

                return RedirectToAction(nameof(Index));
            }
            return View(cryptoVM);
        }


        // GET: Admin/Crypto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crypto = await _cryptoRepository.GetByIdAsync(id ?? 0);

            if (crypto == null)
            {
                return NotFound();
            }

            return View(crypto);
        }

      
        // POST: Admin/Crypto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var crypto = await _cryptoRepository.GetByIdAsync(id);

            if (crypto != null)
            {
                //* Display success message back to user on Index
                var userMessagePartial = new UserMessagePartial(TempData);
                userMessagePartial.SetUserMessage(
                    UserMessageTypes.Success,
                    $"{crypto.CryptoName} was successfully deleted.",
                    8
                );

                _cryptoRepository.Delete(crypto);
            }            

            return RedirectToAction(nameof(Index));
        }

    }
}
