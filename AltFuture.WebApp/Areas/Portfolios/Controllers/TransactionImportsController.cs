using Microsoft.AspNetCore.Mvc;
using AltFuture.DataAccessLayer.Models;
using AutoMapper;
using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.DataAccessLayer.Data.Enums;
using System.IO;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using AltFuture.BusinessLogicLayer.ExchangeTransactionCsvImport.Interfaces;
using AltFuture.BusinessLogicLayer.ExchangeTransactionCsvImport.Models;
using AltFuture.WebApp.Enums;
using AltFuture.WebApp.Helpers;

namespace AltFuture.WebApp.Areas.Portfolios.Controllers
{
    [Area("Portfolios")]
    public class TransactionImportsController : Controller
    {
        private readonly IExchangeTransactionCsvImport _exchangeTransactionCsvImport;

        public TransactionImportsController(IExchangeTransactionCsvImport exchangeTransactionCsvImport)
        {
            _exchangeTransactionCsvImport = exchangeTransactionCsvImport;
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
            if (csvFile is null || csvFile.Length == 0)
            {
                return RedirectToAction("Coinbase");
            }

            int appUserId = 1;

            using var csvData = new StreamReader(csvFile.OpenReadStream());
            var numTransactionsImported = await _exchangeTransactionCsvImport.ImportCsvToDb<CoinbaseTransactionDto>(csvData, appUserId);



            //* Display success message back to user on Dashboard Index
            var userMessagePartial = new UserMessagePartial(TempData);
            userMessagePartial.SetUserMessage(
                UserMessageTypes.Success,
                $"{numTransactionsImported} Coinbase transactions were successfully imported.",
                8
            );

            return RedirectToAction(nameof(Index), "Dashboard");
        }




        public IActionResult CryptoDotCom()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CryptoDotCom(IFormFile csvFile)
        {

            if (csvFile is null || csvFile.Length == 0)
            {
                return RedirectToAction("CryptoDotCom");
            }

            int appUserId = 1;
            int exchangeId = (int)ExchangeEnum.CryptoDotCom;
            var transactionTypeFilter = new Dictionary<int, List<int>>
                                            {
                                                {1, new List<int> { (int)TransactionTypeEnum.Buy } },
                                                {2, new List<int> { (int)TransactionTypeEnum.StakingReward, (int)TransactionTypeEnum.PerkReward } }
                                            };

            using var csvData = new StreamReader(csvFile.OpenReadStream());
            var numTransactionsImported = await _exchangeTransactionCsvImport.ImportCsvToDb<CryptoDotComBuyTransactionDto, CryptoDotComRewardTransactionDto>(csvData, 
                                                                                                                                                             appUserId, 
                                                                                                                                                             exchangeId, 
                                                                                                                                                             transactionTypeFilter);

            //* Display success message back to user on Dashboard Index
            var userMessagePartial = new UserMessagePartial(TempData);
            userMessagePartial.SetUserMessage(
                UserMessageTypes.Success,
                $"{numTransactionsImported} Crypto.com transactions were successfully imported.",
                8
            );

            return RedirectToAction(nameof(Index), "Dashboard");

       
        }





        public IActionResult CoinbasePro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CoinbasePro(IFormFile csvFile)
        {

            if (csvFile is null || csvFile.Length == 0)
            {
                return RedirectToAction("CoinbasePro");
            }

            int appUserId = 1;

            using var csvData = new StreamReader(csvFile.OpenReadStream());
            var numTransactionsImported = await _exchangeTransactionCsvImport.ImportCsvToDb<CoinbaseProTransactionDto>(csvData, appUserId);


            //* Display success message back to user on Dashboard Index
            var userMessagePartial = new UserMessagePartial(TempData);
            userMessagePartial.SetUserMessage(
                UserMessageTypes.Success,
                $"{numTransactionsImported} Coinbase Pro transactions were successfully imported.",
                8
            );

            return RedirectToAction(nameof(Index), "Dashboard");

        }




        public IActionResult Etoro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Etoro(IFormFile csvFile)
        {

            if (csvFile is null || csvFile.Length == 0)
            {
                return RedirectToAction("Etoro");
            }

            int appUserId = 1;

            using var csvData = new StreamReader(csvFile.OpenReadStream());
            var numTransactionsImported = await _exchangeTransactionCsvImport.ImportCsvToDb<EtoroTransactionDto>(csvData, appUserId, "\t");


            //* Display success message back to user on Dashboard Index
            var userMessagePartial = new UserMessagePartial(TempData);
            userMessagePartial.SetUserMessage(
                UserMessageTypes.Success,
                $"{numTransactionsImported} Etoro transactions were successfully imported.",
                8
            );

            return RedirectToAction(nameof(Index), "Dashboard");

        }



        public IActionResult Binance()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Binance(IFormFile csvFile)
        {

            if (csvFile is null || csvFile.Length == 0)
            {
                return RedirectToAction("Binance");
            }

            int appUserId = 1;

            using var csvData = new StreamReader(csvFile.OpenReadStream());
            var numTransactionsImported = await _exchangeTransactionCsvImport.ImportCsvToDb<BinanceTransactionDto>(csvData, appUserId);


            //* Display success message back to user on Dashboard Index
            var userMessagePartial = new UserMessagePartial(TempData);
            userMessagePartial.SetUserMessage(
                UserMessageTypes.Success,
                $"{numTransactionsImported} Binance transactions were successfully imported.",
                8
            );

            return RedirectToAction(nameof(Index), "Dashboard");

        }

    }
}
