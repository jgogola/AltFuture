using AltFuture.BusinessLogicLayer.Interfaces;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using AltFuture.BusinessLogicLayer.Models.ExchangeTransactions;
using AltFuture.DataAccessLayer.Interfaces.Services;
using AltFuture.DataAccessLayer.Models;
using AltFuture.BusinessLogicLayer.Interfaces.Models;

namespace AltFuture.BusinessLogicLayer.Services
{
    public class TransactionCsvImports : ITransactionCsvImports
    {

        private readonly List<ExchangeTransactionType> _exchangeTransactionTypeLookup;

        public TransactionCsvImports(IExchangeTransactionTypeDataService exchangeTransactionTypeDataService)
        {
            _exchangeTransactionTypeLookup = exchangeTransactionTypeDataService.ExchangeTransactionTypeList;
        }



        public async Task<IEnumerable<CoinbaseTransactionHistoryDto>> ImportCoinbaseTransactionHistory(StreamReader reader)
        {
            return await Task.Run(()=>
            { 
                using var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
                var incomingTransactions = csvReader.GetRecords<CoinbaseTransactionHistoryDto>().ToList();

                return incomingTransactions;
            });
        }

        public async Task<IEnumerable<T>> ImportExchangeTransactionHistory<T>(StreamReader reader) where T : IExchangeTransactionHistoryDto
        {
            return await Task.Run(() =>
            {
                using var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
                var incomingTransactions = csvReader.GetRecords<T>().ToList();

                return incomingTransactions;
            });
        }


        public async Task<IEnumerable<T>> ImportExchangeTransactionHistory<T>(StreamReader reader, int[]? transactionTypeFilter = null) where T : IExchangeTransactionHistoryDto
        {
            return await Task.Run(() =>
            {

                using var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));

                var exchangeTransactionTypeFilter = _exchangeTransactionTypeLookup.Where(t => transactionTypeFilter.Contains(t.TransactionTypeId))
                                                                                  .Select(t => t.ExchangeTransactionTypeName).ToList();

                 var incomingTransactions = csvReader.GetRecords<T>().Where(t => exchangeTransactionTypeFilter.Contains(t.ExchangeTransactionTypeName)).ToList();

                return incomingTransactions;
            });
        }

        public async Task<(IEnumerable<T1> type1Transactions, IEnumerable<T2> type2Transactions)> ImportExchangeTransactionHistory<T1, T2>(StreamReader reader,
                                                                                                                                           int exchageId,
                                                                                                                                           Dictionary<int, List<int>> transactionTypeFilter
                                                                                                                                          ) where T1 : IExchangeTransactionHistoryDto
                                                                                                                                            where T2 : IExchangeTransactionHistoryDto
        {
            return await Task.Run(() =>
            {
                

                var exchangeTransactionTypeFilter1 = _exchangeTransactionTypeLookup.Where(t => transactionTypeFilter[1].Contains(t.TransactionTypeId) 
                                                                                               && t.ExchangeId == exchageId)
                                                                                   .Select(t => t.ExchangeTransactionTypeName).ToList();

                var exchangeTransactionTypeFilter2 = _exchangeTransactionTypeLookup.Where(t => transactionTypeFilter[2].Contains(t.TransactionTypeId) 
                                                                                               && t.ExchangeId == exchageId)
                                                                                   .Select(t => t.ExchangeTransactionTypeName).ToList();

                var type1Transactions = new List<T1>();
                var type2Transactions = new List<T2>();


                using (var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    csvReader.Read();
                    csvReader.ReadHeader();

                    while (csvReader.Read())
                    {
                        var exchangeTransactionType = csvReader.GetField("Transaction Kind");

                        if (exchangeTransactionTypeFilter1.Contains(exchangeTransactionType))
                        {
                            var record = csvReader.GetRecord<T1>();
                            type1Transactions.Add(record);
                        }
                        else if (exchangeTransactionTypeFilter2.Contains(exchangeTransactionType))
                        {
                            var record = csvReader.GetRecord<T2>();
                            type2Transactions.Add(record);
                        }
                    }
                }
                return (type1Transactions, type2Transactions);

            });
        }

    }
}