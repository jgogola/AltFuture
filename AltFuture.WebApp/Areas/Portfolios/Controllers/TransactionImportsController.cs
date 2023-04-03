using AltFuture.BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AltFuture.DataAccessLayer.Models;
using AutoMapper;
using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.BusinessLogicLayer.Models.ExchangeTransactions;
using AltFuture.DataAccessLayer.Data.Enums;
using System.IO;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;

namespace AltFuture.WebApp.Areas.Portfolios.Controllers
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
                    incomingCoinbaseTransactions = (List<CoinbaseTransactionHistoryDto>)await _csvImports.ImportExchangeTransactionHistory<CoinbaseTransactionHistoryDto>(reader);
                }

                // AutoMap the incoming Coinbase transaction DTOs to the application's Transaction model.
                var mappedTransactions = new List<Transaction>();
                if(incomingCoinbaseTransactions.Any())
                {
                    mappedTransactions = _mapper.Map<List<Transaction>>(incomingCoinbaseTransactions, opts =>
                                                    {
                                                        opts.Items["ExchangeId"] = (int)ExchangeEnum.Coinbase;
                                                    });
                }

                // Save the range of Transactions into the DB.
                if (mappedTransactions.Any())
                {
                    var saved = _transactionRepository.AddRangeAsync(mappedTransactions);
                }
            }

            return RedirectToAction("Coinbase"); // Redirect to the same page after processing the CSV
        }

        public IActionResult CryptoDotCom()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CryptoDotCom(IFormFile csvFile)
        {

            // Read the CSV file and create a touple of CryptoDotCom "Buy/Reward" transaction DTOs.
            (IEnumerable<CryptoDotComBuyTransactionHistoryDto> buyTypeTransactions, IEnumerable<CryptoDotComRewardTransactionHistoryDto> rewardtypeTransactions) incomingCryptoDotComTransactions;
          
            using (var reader = new StreamReader(csvFile.OpenReadStream()))
            {

                var transactionTypeFilter = new Dictionary<int, List<int>>
                {
                    {1, new List<int> { (int)TransactionTypeEnum.Buy } },
                    {2, new List<int> { (int)TransactionTypeEnum.StakingReward, (int)TransactionTypeEnum.PerkReward } }
                };

                incomingCryptoDotComTransactions = await _csvImports.ImportExchangeTransactionHistory<CryptoDotComBuyTransactionHistoryDto, CryptoDotComRewardTransactionHistoryDto>(reader, 
                                                                                                                                                                                     (int)ExchangeEnum.CryptoDotCom, 
                                                                                                                                                                                     transactionTypeFilter);
            }

            // Process the incomming CryptoDotCom "Buy" and "Reward" transactions separately.
            // AutoMap the "Buy" transactions the application's Transaction model.
            var mappedTransactions = new List<Transaction>();
            if (incomingCryptoDotComTransactions.buyTypeTransactions.Any())
            {

                mappedTransactions = _mapper.Map<List<Transaction>>(incomingCryptoDotComTransactions.buyTypeTransactions, opts =>
                                                {
                                                    opts.Items["ExchangeId"] = (int)ExchangeEnum.CryptoDotCom;
                                                });
            }

            // AutoMap the "Reward" transactions the application's Transaction model.
            if (incomingCryptoDotComTransactions.rewardtypeTransactions.Any())
            {

                var mappedRewardTransactions = _mapper.Map<List<Transaction>>(incomingCryptoDotComTransactions.rewardtypeTransactions, opts =>
                                                        {
                                                            opts.Items["ExchangeId"] = (int)ExchangeEnum.CryptoDotCom;
                                                        });

                mappedTransactions.AddRange(mappedRewardTransactions);
            }

            // Save all the Transactions into the DB.
            if (mappedTransactions.Any())
            {
                var saved = _transactionRepository.AddRangeAsync(mappedTransactions);
            }

            return RedirectToAction("CryptoDotCom");
        }

    }
}
