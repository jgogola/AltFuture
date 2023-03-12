using Microsoft.AspNetCore.Mvc;
using AltFuture.DataAccessLayer.Interfaces;
using AltFutureWebApp.Areas.Admin.ViewModels;
using AltFutureWebApp.Helpers;
using AltFutureWebApp.Enums;
using AltFuture.Models;

namespace AltFutureWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExchangeController : Controller
    {

        private readonly IExchangeRepository _exchangeRepository;

        public ExchangeController(IExchangeRepository exchangeRepository)
        {
            _exchangeRepository = exchangeRepository;
        }

        // GET: Admin/Exchange
        public async Task<IActionResult> Index()
        {
            return View(await _exchangeRepository.GetAllAsync());
        }

        // GET: Admin/Exchange/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exchange = await _exchangeRepository.GetByIdAsync(id ?? 0);

            if (exchange == null)
            {
                return NotFound();
            }


            return View(exchange);
        }


        // GET: Admin/Exchange/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Admin/Exchange/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExchangeViewModel exchangeVM)
        {
            if (ModelState.IsValid)
            {
                var exchange = new Exchange()
                {
                    ExchangeName = exchangeVM.ExchangeName
                };

                _exchangeRepository.Add(exchange);

                //* Display success message back to user on Index
                var userMessagePartial = new UserMessagePartial(TempData);
                userMessagePartial.SetUserMessage(
                    UserMessageTypes.Success,
                    $"{exchange.ExchangeName} was successfully added.",
                    8
                );

                return RedirectToAction(nameof(Index));
            }
            return View(exchangeVM);
        }



        // GET: Admin/Exchange/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exchange = await _exchangeRepository.GetByIdAsync(id ?? 0);
            if (exchange == null)
            {
                return NotFound();
            }

            var exchangeVM = new ExchangeViewModel()
            {
                ExchangeId = exchange.ExchangeId,
                ExchangeName = exchange.ExchangeName
            };

            return View(exchangeVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ExchangeViewModel exchangeVM)
        {
            if (id != exchangeVM.ExchangeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var exchange = new Exchange()
                {
                    ExchangeId = exchangeVM.ExchangeId,
                    ExchangeName = exchangeVM.ExchangeName
                };

                _exchangeRepository.Update(exchange);


                //* Display success message back to user on Index
                var userMessagePartial = new UserMessagePartial(TempData);
                userMessagePartial.SetUserMessage(
                    UserMessageTypes.Success,
                    $"{exchange.ExchangeName} was successfully updated.",
                    8
                );

                return RedirectToAction(nameof(Index));
            }
            return View(exchangeVM);
        }

        // GET: Admin/Exchange/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exchange = await _exchangeRepository.GetByIdAsync(id ?? 0);

            if (exchange == null)
            {
                return NotFound();
            }

            return View(exchange);
        }


        // POST: Admin/Exchange/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var exchange = await _exchangeRepository.GetByIdAsync(id);

            if (exchange != null)
            {
                //* Display success message back to user on Index
                var userMessagePartial = new UserMessagePartial(TempData);
                userMessagePartial.SetUserMessage(
                    UserMessageTypes.Success,
                    $"{exchange.ExchangeName} was successfully deleted.",
                    8
                );
            
                _exchangeRepository.Delete(exchange);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
