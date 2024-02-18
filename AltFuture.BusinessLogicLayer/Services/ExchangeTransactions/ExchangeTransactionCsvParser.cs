using AltFuture.BusinessLogicLayer.ExchangeTransactionCsvImport.Interfaces;
using AltFuture.DataAccessLayer.Interfaces.Services;
using AltFuture.DataAccessLayer.Models;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using AltFuture.DataAccessLayer.Data.Enums;

namespace AltFuture.BusinessLogicLayer.Services.ExchangeTransactions
{
    public class ExchangeTransactionCsvParser : IExchangeTransactionCsvParser
    {

        private readonly List<ExchangeTransactionType> _exchangeTransactionTypeLookup;

        public ExchangeTransactionCsvParser(IExchangeTransactionTypeDataService exchangeTransactionTypeDataService)
        {
            _exchangeTransactionTypeLookup = exchangeTransactionTypeDataService.ExchangeTransactionTypeList;
        }


        public async Task<IEnumerable<T>> ParseExchangeTransactionCsvToDto<T>(StreamReader csvData, string delimiter = ",") where T : IExchangeTransactionDto
        {
            return await Task.Run(() =>
            {
                using var csvReader = new CsvReader(csvData, new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = delimiter });
                var incomingExchangeTransactionDtos = csvReader.GetRecords<T>().ToList();

                return incomingExchangeTransactionDtos;
            });
        }


        public async Task<IEnumerable<T>> ParseExchangeTransactionCsvToDto<T>(StreamReader csvData, int exchageId, int[] transactionTypeFilter, string delimiter = ",") where T : IExchangeTransactionDto
        {
            return await Task.Run(() =>
            {

                using var csvReader = new CsvReader(csvData, new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = delimiter });

                var exchangeTransactionTypeFilter = _exchangeTransactionTypeLookup.Where(t => t.DataImportTypeId == (int)DataImportTypeEnum.CSV
                                                                                              && t.ExchangeId == exchageId
                                                                                              && transactionTypeFilter.Contains(t.TransactionTypeId))
                                                                                  .Select(t => t.ExchangeTransactionTypeName).ToList();

                var incomingExchangeTransactionDtos = csvReader.GetRecords<T>().Where(t => exchangeTransactionTypeFilter.Contains(t.ExchangeTransactionTypeName)).ToList();

                return incomingExchangeTransactionDtos;
            });
        }

        public async Task<(IEnumerable<T1> type1Transactions, IEnumerable<T2> type2Transactions)> ParseExchangeTransactionCsvToDto<T1, T2>(StreamReader csvData,
                                                                                                                                                   int exchageId,
                                                                                                                                                   Dictionary<int, List<int>> transactionTypeFilter,
                                                                                                                                                   string delimiter = ","
                                                                                                                                                   ) where T1 : IExchangeTransactionDto
                                                                                                                                                     where T2 : IExchangeTransactionDto
        {
            return await Task.Run(() =>
            {


                var exchangeTransactionTypeFilter1 = _exchangeTransactionTypeLookup.Where(t => t.DataImportTypeId == (int)DataImportTypeEnum.CSV
                                                                                               && t.ExchangeId == exchageId
                                                                                               && transactionTypeFilter[1].Contains(t.TransactionTypeId))
                                                                                    .Select(t => t.ExchangeTransactionTypeName).ToList();

                var exchangeTransactionTypeFilter2 = _exchangeTransactionTypeLookup.Where(t => t.DataImportTypeId == (int)DataImportTypeEnum.CSV
                                                                                               && t.ExchangeId == exchageId
                                                                                               && transactionTypeFilter[2].Contains(t.TransactionTypeId))
                                                                                    .Select(t => t.ExchangeTransactionTypeName).ToList();

                var incomingType1ExchangeTransactionDtos = new List<T1>();
                var incomingType2ExchangeTransactionDtos = new List<T2>();


                using (var csvReader = new CsvReader(csvData, new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = delimiter }))
                {
                    csvReader.Read();
                    csvReader.ReadHeader();

                    while (csvReader.Read())
                    {
                        var exchangeTransactionType = csvReader.GetField("Transaction Kind");

                        if (exchangeTransactionTypeFilter1.Contains(exchangeTransactionType))
                        {
                            var record = csvReader.GetRecord<T1>();
                            incomingType1ExchangeTransactionDtos.Add(record);
                        }
                        else if (exchangeTransactionTypeFilter2.Contains(exchangeTransactionType))
                        {
                            var record = csvReader.GetRecord<T2>();
                            incomingType2ExchangeTransactionDtos.Add(record);
                        }
                    }
                }
                return (incomingType1ExchangeTransactionDtos, incomingType2ExchangeTransactionDtos);

            });
        }


    }

}

