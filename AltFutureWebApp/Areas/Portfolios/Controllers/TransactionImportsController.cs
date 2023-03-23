using AltFuture.BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AltFuture.BusinessLogicLayer.Models.DTOs;
using AltFuture.DataAccessLayer.Services;
using AltFuture.DataAccessLayer.Models;
using System.Collections.Generic;
using AutoMapper;
using AltFuture.DataAccessLayer.Interfaces;

namespace AltFutureWebApp.Areas.Portfolios.Controllers
{
    [Area("Portfolios")]
    public class TransactionImportsController : Controller
    {
        private readonly ITransactionCsvImports _csvImports;
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionImportsController(ITransactionCsvImports csvImports,
                                            IMapper mapper,
                                            ITransactionRepository transactionRepository)
        {
            _csvImports = csvImports;
            _mapper = mapper;
            _transactionRepository = transactionRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Coinbase()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Coinbase(IFormFile csvFile)
        {
            if (csvFile != null && csvFile.Length > 0)
            {
                // Read the CSV file and create a list of Coinbase transaction DTOs.
                var incomingCoinbaseTransactions = new List<CoinbaseTransactionHistoryDto>();
                using (var reader = new StreamReader(csvFile.OpenReadStream()))
                {
                    incomingCoinbaseTransactions = (List<CoinbaseTransactionHistoryDto>)await _csvImports.ImportCoinbaseTransactionHistory(reader);
                }

                // AutoMap the incoming Coinbase transaction DTOs to the application's Transaction model.
                var mappedTransactions = new List<Transaction>();
                if(incomingCoinbaseTransactions.Any())
                {
                    mappedTransactions = _mapper.Map<List<Transaction>>(incomingCoinbaseTransactions);
                }

                // Save the range of Transactions into the DB.
                if (mappedTransactions.Any())
                {
                    var saved = _transactionRepository.AddRangeAsync(mappedTransactions);
                }
            }

            return RedirectToAction("Coinbase"); // Redirect to the same page after processing the CSV
        }
    }
}
