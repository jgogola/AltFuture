using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AltFutureWebApp.Data;
using AltFutureWebApp.Models;
using AltFutureWebApp.Interfaces;
using AltFutureWebApp.Areas.Admin.ViewModels;
using Microsoft.CodeAnalysis.Text;
using AltFutureWebApp.Data.Enums;
using AltFutureWebApp.ViewModels;
using Newtonsoft.Json;

namespace AltFutureWebApp.Areas.Admin.Controllers
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
            var userMsg = TempData["UserMessage"] as string;
            if (userMsg != null)
            {
                ViewBag.UserMessageType = TempData["UserMessageType"] ?? UserMessageTypes.System; 
                ViewBag.UserMessage = userMsg;
            }

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

                var userMessage = new UserMessageViewModel()
                {
                    UserMessageType = UserMessageTypes.Success,
                    UserMessage = $"{crypto.CryptoName} was successfully added.",
                    FadeOutSeconds = 8
                };

                TempData["UserMessage"] = JsonConvert.SerializeObject(userMessage);

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

                var userMessage = new UserMessageViewModel()
                {
                    UserMessageType = UserMessageTypes.Success,
                    UserMessage = $"{crypto.CryptoName} was successfully updated.",
                    FadeOutSeconds = 8
                };

                TempData["UserMessage"] = userMessage;

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
                _cryptoRepository.Delete(crypto);
            }

            var userMessage = new UserMessageViewModel()
            {
                UserMessageType = UserMessageTypes.Success,
                UserMessage = $"{crypto.CryptoName} was successfully deleted.",
                FadeOutSeconds = 8
            };

            TempData["UserMessage"] = JsonConvert.SerializeObject(userMessage);

            return RedirectToAction(nameof(Index));
        }

    }
}
