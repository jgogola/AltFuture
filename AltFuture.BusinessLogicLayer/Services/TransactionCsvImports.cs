using AltFuture.BusinessLogicLayer.Interfaces;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using AltFuture.BusinessLogicLayer.Models.DTOs;

namespace AltFuture.BusinessLogicLayer.Services
{
    public class TransactionCsvImports : ITransactionCsvImports
    {
        public async Task<IEnumerable<CoinbaseTransactionHistoryDto>> ImportCoinbaseTransactionHistory(StreamReader reader)
        {
            return await Task.Run(()=>
            { 
                using var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
                var incomingTransactions = csvReader.GetRecords<CoinbaseTransactionHistoryDto>().ToList();

                return incomingTransactions;
            });
        }
    }
}