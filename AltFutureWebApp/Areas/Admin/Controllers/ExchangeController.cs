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
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using AltFutureWebApp.Data.Enums;
using AltFutureWebApp.ViewModels;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

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
            var userMessageJson = TempData["UserMessage"] as string;
            
            if(userMessageJson != null)
            {
                var userMessage = JsonConvert.DeserializeObject<UserMessageViewModel>(userMessageJson);
                ViewBag.UserMessage = userMessage;
            }

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

                var userMessage = new UserMessageViewModel()
                {
                    UserMessageType = UserMessageTypes.Success,
                    UserMessage = $"{exchange.ExchangeName} was successfully added.",
                    FadeOutSeconds = 8
                };

                TempData["UserMessage"] = JsonConvert.SerializeObject(userMessage);

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

                var userMessage = new UserMessageViewModel()
                {
                    UserMessageType = UserMessageTypes.Success,
                    UserMessage = $"{exchange.ExchangeName} was successfully updated.",
                    FadeOutSeconds = 8
                };

                TempData["UserMessage"] = JsonConvert.SerializeObject(userMessage);

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
                _exchangeRepository.Delete(exchange);
            }

            var userMessage = new UserMessageViewModel()
            {
                UserMessageType = UserMessageTypes.Success,
                UserMessage = $"{exchange.ExchangeName} was successfully deleted.",
                FadeOutSeconds = 8
            };

            TempData["UserMessage"] = JsonConvert.SerializeObject(userMessage);

            return RedirectToAction(nameof(Index));
        }

    }
}
