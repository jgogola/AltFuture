﻿using AltFuture.DataAccessLayer.Models.DTOs.PortfolioCharts;
using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.DataAccessLayer.Interfaces.Services;
using AltFuture.DataAccessLayer.Models;
using AltFuture.DataAccessLayer.Data.Enums;

namespace AltFuture.DataAccessLayer.Services
{
    public class PortfolioChartData : IPortfolioChartData
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICryptoPriceRepository _cryptoPriceRepository;
        private readonly List<Crypto> _cryptos;

        public PortfolioChartData(ITransactionRepository transactionRepository, ICryptoDataService cryptoDataService, ICryptoPriceRepository cryptoPriceRepository)
        {
            _transactionRepository = transactionRepository;
            _cryptoPriceRepository = cryptoPriceRepository;
            _cryptos = cryptoDataService.CryptoList;
        }

        public async Task<List<AssetAllocationDataDto>> GetAssetAllocationDataAsync(int userId)
        {
            var transactions = await _transactionRepository.GetAllForUserAsync(userId);

            var groupedTransactions = transactions.GroupBy(t => t.CryptoId).ToList();

            decimal totalInvestment = transactions.Sum(t => t.TransactionTotal);

            var assetAllocationData = groupedTransactions.Select(g => new AssetAllocationDataDto
            {
                CryptoId = g.Key,
                TickerSymbol = _cryptos.FirstOrDefault(c => c.CryptoId == g.Key).TickerSymbol,
                InvestmentPercentage = (decimal)g.Sum(t => t.TransactionTotal) / totalInvestment * 100
            });

            return assetAllocationData.ToList();
        }


        public async Task<List<AssetPerformanceDataDto>> GetAssetPerformanceDataAsync(int userId)
        {
            var prices = await _cryptoPriceRepository.GetLatestPricesAsync();
            var transactions = await _transactionRepository.GetAllForUserAsync(userId);

            var assetPerformanceData = from price in prices
                                       join transaction in transactions on price.Crypto equals transaction.Crypto into transGroup
                                       where transGroup.Count() > 0
                                       select new AssetPerformanceDataDto {

                                           CryptoId = price.CryptoId,
                                           TickerSymbol = price.Crypto.TickerSymbol,
                                           Investment = (decimal)transGroup.Sum(t => t.InvestmentTotal),
                                           UnrealizedProfit = (decimal)(transGroup.Sum(t => t.Quantity) * price.Price) - (transGroup.Sum(t => t.Quantity) * (transGroup.Sum(t => t.InvestmentTotal) / transGroup.Sum(t => t.Quantity))) - transGroup.Sum(t => t.Fee)

                                       };

            return assetPerformanceData.ToList();
        }


        public async Task<List<ExchangeUsageDataDto>> GetExchangeUsageDataAsync(int userId)
        {
            var transactions = await _transactionRepository.GetAllForUserAsync(userId);

            var groupedTransactions = transactions.GroupBy(t => t.FromExchangeId).ToList();

            decimal transactionCount = transactions.Where(t => t.ExchangeTransactionType.TransactionTypeId == (int)TransactionTypeEnum.Buy).Count();

            var exchangeUsageData = groupedTransactions.Select(g => new ExchangeUsageDataDto
            {
                ExchangeId = g.Key,
                ExchangeName =  g.FirstOrDefault(t => t.FromExchangeId == g.Key).FromExchange.ExchangeName,
                UsagePercentage = (decimal)g.Count() / transactionCount * 100
            });

            return exchangeUsageData.ToList();
        }
    }
}
 