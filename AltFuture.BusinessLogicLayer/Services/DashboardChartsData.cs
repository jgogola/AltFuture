using AltFuture.BusinessLogicLayer.Interfaces;
using AltFuture.BusinessLogicLayer.Models.DTOs.Dashboard;
using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.DataAccessLayer.Interfaces.Services;
using AltFuture.DataAccessLayer.Services;
using AltFuture.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Transactions;

namespace AltFuture.BusinessLogicLayer.Services
{
    public class DashboardChartsData : IDashboardChartsData
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICryptoPriceRepository _cryptoPriceRepository;
        private readonly List<Crypto> _cryptos;

        public DashboardChartsData(ITransactionRepository transactionRepository, ICryptoDataService cryptoDataService, ICryptoPriceRepository cryptoPriceRepository)
        {
            _transactionRepository = transactionRepository;
            _cryptoPriceRepository = cryptoPriceRepository;
            _cryptos = cryptoDataService.CryptoList;
        }
        public async Task<List<CryptoQuantityPercentageDto>> GetCryptoQuantityPercentageAsync(int userId)
        {
            var transactions = await _transactionRepository.GetAllForUserAsync(userId);

            var groupedTransactions = transactions.GroupBy(t => t.CryptoId).ToList();

            decimal totalQuantity = transactions.Sum(t => t.Quantity);

            var cryptoPercentages = groupedTransactions.Select(g => new CryptoQuantityPercentageDto
            {
                CryptoId = g.Key,
                TickerSymbol = _cryptos.FirstOrDefault(c => c.CryptoId == g.Key).TickerSymbol,
                QuantityPercentage = (decimal)g.Sum(t => t.Quantity) / totalQuantity * 100
            });

            return cryptoPercentages.ToList();
        }


        public async Task<List<CryptoInvestmentPercentageDto>> GetCryptoInvestmentPercentageAsync(int userId)
        {
            var transactions = await _transactionRepository.GetAllForUserAsync(userId);

            var groupedTransactions = transactions.GroupBy(t => t.CryptoId).ToList();

            decimal totalInvestment = transactions.Sum(t => t.TransactionTotal);

            var cryptoPercentages = groupedTransactions.Select(g => new CryptoInvestmentPercentageDto
            {
                CryptoId = g.Key,
                TickerSymbol = _cryptos.FirstOrDefault(c => c.CryptoId == g.Key).TickerSymbol,
                InvestmentPercentage = (decimal)g.Sum(t => t.TransactionTotal) / totalInvestment * 100
            });

            return cryptoPercentages.ToList();
        }


        public async Task<List<CryptoInvestmentPerformanceDto>> GetCryptoInvestmentPerformanceAsync(int userId)
        {
            var prices = await _cryptoPriceRepository.GetLatestAsync();
            var transactions = await _transactionRepository.GetAllForUserAsync(userId);

            var cryptoInvestementPerformances = from price in prices
                                                join transaction in transactions on price.Crypto equals transaction.Crypto into transGroup
                                                where transGroup.Count() > 0
                                                select new CryptoInvestmentPerformanceDto {

                                                    CryptoId = price.CryptoId,
                                                    TickerSymbol = price.Crypto.TickerSymbol,
                                                    Investment = (decimal)transGroup.Sum(t => t.InvestmentTotal),
                                                    UnrealizedProfit = (decimal)(transGroup.Sum(t => t.Quantity) * price.Price) - (transGroup.Sum(t => t.Quantity) * (transGroup.Sum(t => t.InvestmentTotal) / transGroup.Sum(t => t.Quantity))) - transGroup.Sum(t => t.Fee)

                                                };

            return cryptoInvestementPerformances.ToList();
        }
    }
}
 