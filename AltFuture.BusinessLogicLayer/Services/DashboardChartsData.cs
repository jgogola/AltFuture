using AltFuture.BusinessLogicLayer.Interfaces;
using AltFuture.BusinessLogicLayer.Models.DTOs.Dashboard;
using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.DataAccessLayer.Interfaces.Services;
using AltFuture.DataAccessLayer.Services;
using AltFuture.Models;
using System;
using System.Collections.Generic;
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
        private readonly List<Crypto> _cryptos;

        public DashboardChartsData(ITransactionRepository transactionRepository, ICryptoDataService cryptoDataService)
        {
            _transactionRepository = transactionRepository;
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
            var transactions = await _transactionRepository.GetAllForUserAsync(userId);

            var groupedTransactions = transactions.GroupBy(t => t.CryptoId).ToList();

            decimal totalInvestment = transactions.Sum(t => t.TransactionTotal);

            var cryptoPercentages = groupedTransactions.Select(g => new CryptoInvestmentPerformanceDto
            {
                CryptoId = g.Key,
                TickerSymbol = _cryptos.FirstOrDefault(c => c.CryptoId == g.Key).TickerSymbol,
                Investment = (decimal)g.Sum(t => t.TransactionTotal),
              //  UnrealizedProfit = g.Sum(t => t.Un)
            });

            return cryptoPercentages.ToList();
        }
    }
}
 