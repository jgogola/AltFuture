using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using AltFuture.DataAccessLayer.Models;
using AltFuture.WebApp.Enums;
using AltFuture.WebApp.Helpers;
using AltFuture.DataAccessLayer.Data;
using AltFuture.BusinessLogicLayer.MoonShot;
using AltFuture.DataAccessLayer.Extensions;

namespace AltFuture.WebApp.Areas.Portfolios.Controllers
{
    [Area("Portfolios")]
    public class DashboardController : Controller
    {
        private readonly IPortfolioSummaryRepository _portfolioSummaryRepository;
        private readonly ICryptoRepository _cryptoRepository;
        private readonly ICryptoPriceRepository _cryptoPriceRepository;
        private readonly IPortfolioChartData _portfolioChartData;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMoonShotFactory _moonShotFactory;
        private readonly AppDbContext _context;

        private const int _bitcoinMoonShotReferenceNum = 777777777;
        private const int _shibaInuMoonShotReferenceNum = 66666666;

        public DashboardController(IPortfolioSummaryRepository portfolioSummaryRepository, 
                                   ICryptoRepository cryptoRepository, 
                                   ICryptoPriceRepository cryptoPriceRepository, 
                                   IPortfolioChartData portfolioChartData,
                                   ITransactionRepository transactionRepository,
                                   IMoonShotFactory moonShotFactory,
                                   AppDbContext context)
        {
            _portfolioSummaryRepository = portfolioSummaryRepository;
            _cryptoRepository = cryptoRepository;
            _cryptoPriceRepository = cryptoPriceRepository;
            _portfolioChartData = portfolioChartData;
            _transactionRepository = transactionRepository;
            _moonShotFactory = moonShotFactory;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userMessageJson = TempData["UserMessage"] as string;
            var appUserId = 1;

            MoonShot moonShot = _moonShotFactory.Create(MoonShotTypeEnum.ShibaInu, appUserId);
            ViewBag.ShibaInuMoonShotExists = moonShot.MoonShotExists;

            return View(await _portfolioSummaryRepository.GetAllAsync(appUserId));
        }

        [HttpPost]
        public async Task<IActionResult> ResetMoonShot(MoonShotTypeEnum moonShotType)
        {
            var userMessagePartial = new UserMessagePartial(TempData);
            var appUserId = 1;

            MoonShot moonShot = _moonShotFactory.Create(moonShotType, appUserId);
            var success = moonShot.Remove();

            if (success)
            {
                //* Display success message back to user on Dashboard Index           
                userMessagePartial.SetUserMessage(
                    UserMessageTypes.Success,
                    $"{moonShotType.GetDisplayName()} moon-shot transaction was successfully reset.",
                    5
                );

                return RedirectToAction(nameof(Index));
            }
            else
            {
                //* Display warning message back to user on Dashboard Index           
                userMessagePartial.SetUserMessage(
                    UserMessageTypes.Warning,
                    $"{moonShotType.GetDisplayName()} moon-shot transaction was not found. Reset request canceled.",
                    8
                );

                return RedirectToAction(nameof(Index));
            }
        }



        [HttpPost]
        public async Task<IActionResult> AddMoonShot(MoonShotTypeEnum moonShotType)
        {
            var userMessagePartial = new UserMessagePartial(TempData);
            var appUserId = 1;

            MoonShot moonShot = _moonShotFactory.Create(moonShotType, appUserId);
            var success = moonShot.Add();

            if (success)
            {
                //* Display success message back to user on Dashboard Index           
                userMessagePartial.SetUserMessage(
                    UserMessageTypes.Success,
                    $"{moonShotType.GetDisplayName()} moon-shot transaction was successfully added.",
                    5
                );

                return RedirectToAction(nameof(Index));
            }
            else
            {
                //* Display warning message back to user on Dashboard Index           
                userMessagePartial.SetUserMessage(
                    UserMessageTypes.Warning,
                    $"{moonShotType.GetDisplayName()} moon-shot transaction already exists. Please reset and try again.",
                    8
                );

                return RedirectToAction(nameof(Index));
            }
        }


        [HttpGet]
        public async Task<JsonResult> GetAssetAllocationData()
        {
            var appUserId = 1;
            var assetAllocationData = await _portfolioChartData.GetAssetAllocationDataAsync(appUserId);

            var data = new JArray { };

            data.Add(new JArray { "Crypto", "Allocation" });

            assetAllocationData.ForEach(c =>
            {
                data.Add(new JArray { c.TickerSymbol, c.InvestmentPercentage });
            });


            return Json(data);
        }


        [HttpGet]
        public async Task<JsonResult> GetAssetPerformanceData()
        {
            var appUserId = 1;
            var assetPerformanceData = await _portfolioChartData.GetAssetPerformanceDataAsync(appUserId);

            var data = new JArray { };

            data.Add(new JArray { "Crypto Assets", "Investment", "Current Worth" });

            assetPerformanceData.ForEach(c =>
            {
                data.Add(new JArray { c.TickerSymbol, c.Investment, c.Investment + c.UnrealizedProfit }); 
            });


            return Json(data);
        }


        [HttpGet]
        public async Task<JsonResult> GetExchangeUsageData()
        {
            var appUserId = 1;
            var exchangeUsageData = await _portfolioChartData.GetExchangeUsageDataAsync(appUserId);

            var data = new JArray { };

            data.Add(new JArray { "Exchange", "Usage" });

            exchangeUsageData.ForEach(c =>
            {
                data.Add(new JArray { c.ExchangeName, c.UsagePercentage });
            });


            return Json(data);
        }


        [HttpGet]
        public async Task<JsonResult> GetPortfolioRunningTotalByMonthData()
        {
            var appUserId = 1;
            var dailyRunningTotalData = await _portfolioChartData.GetPortfolioRunningTotalByMonthDataAsync(appUserId);

            var data = new JArray { };

            data.Add(new JArray { "Month", "Investment", "Current Worth" });

            dailyRunningTotalData.ForEach(c =>
            {
                data.Add(new JArray { c.RunningTotalInterval, c.InvestmentRunningTotal, c.CurrentWorthRunningTotal });
            });


            return Json(data);
        }

    }
}
