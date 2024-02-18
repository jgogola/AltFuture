using AltFuture.BusinessLogicLayer.ExchangeTransactionCsvImport.Interfaces;
using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.DataAccessLayer.Models;


namespace AltFuture.BusinessLogicLayer.Services.ExchangeTransactions
{
    public class ExchangeTransactionCsvImport : IExchangeTransactionCsvImport
    {
        private readonly IExchangeTransactionCsvParser _exchangeTransactionCsvParser;
        private readonly IExchangeTransactionMapper _exchangeTransactionMapper;
        private readonly ITransactionRepository _transactionRepository;

        public ExchangeTransactionCsvImport(IExchangeTransactionCsvParser exchangeTransactionCsvParser,
                                            IExchangeTransactionMapper exchangeTransactionMapper,
                                            ITransactionRepository transactionRepository)
        {
            _exchangeTransactionCsvParser = exchangeTransactionCsvParser;
            _exchangeTransactionMapper = exchangeTransactionMapper;
            _transactionRepository = transactionRepository;
        }

        public async Task<int> ImportCsvToDb<T>(StreamReader csvData, int appUserId, string delimiter = ",") where T : IExchangeTransactionDto
        {

            // Step 1: CSV -> DTO
            var csvExchangeTransactionDtoList = (List<T>)await _exchangeTransactionCsvParser.ParseExchangeTransactionCsvToDto<T>(csvData, delimiter);

            // Step 2: DTO -> Map
            var mappedTransactionList = await _exchangeTransactionMapper.MapExchangeTransactionDtoToTransaction(csvExchangeTransactionDtoList, appUserId);

            // Step 3: Map -> DB
            var saved = false;
            if (mappedTransactionList.Any())
            {
                saved = await _transactionRepository.AddRangeAsync(mappedTransactionList);
            }

            // Return number of transactions imported
            if (saved)
                return mappedTransactionList.Count;
            else
                return 0;

        }

        public async Task<int> ImportCsvToDb<T>(StreamReader csvData, int appUserId, int exchangeId, int[] transactionTypeFilter, string delimiter = ",") where T : IExchangeTransactionDto
        {
            // Step 1: CSV -> DTO
            var csvExchangeTransactionDtoList = (List<T>)await _exchangeTransactionCsvParser.ParseExchangeTransactionCsvToDto<T>(csvData, exchangeId, transactionTypeFilter, delimiter);

            // Step 2: DTO -> Map
            var mappedTransactionList = await _exchangeTransactionMapper.MapExchangeTransactionDtoToTransaction(csvExchangeTransactionDtoList, appUserId);

            // Step 3: Map -> DB
            var saved = false;
            if (mappedTransactionList.Any())
            {
                saved = await _transactionRepository.AddRangeAsync(mappedTransactionList);
            }

            // Return number of transactions imported
            if (saved)
                return mappedTransactionList.Count;
            else
                return 0;
        }



        public async Task<int> ImportCsvToDb<T1, T2>(StreamReader csvData,
                                                         int appUserId,
                                                         int exchageId,
                                                         Dictionary<int, List<int>> transactionTypeFilter,
                                                         string delimiter = ",") where T1 : IExchangeTransactionDto
                                                                                  where T2 : IExchangeTransactionDto
        {
            // Step 1: CSV -> DTO
            (IEnumerable<T1> type1Transactions, IEnumerable<T2> type2Transactions) csvExchangeTransactionDtoList;
            csvExchangeTransactionDtoList = await _exchangeTransactionCsvParser.ParseExchangeTransactionCsvToDto<T1, T2>(csvData,
                                                                                                                                exchageId,
                                                                                                                                transactionTypeFilter,
                                                                                                                                delimiter);

            var mappedTransactionList = new List<Transaction>();
            // DTO1 -> Map
            if (csvExchangeTransactionDtoList.type1Transactions.Any())
            {
                mappedTransactionList = await _exchangeTransactionMapper.MapExchangeTransactionDtoToTransaction(csvExchangeTransactionDtoList.type1Transactions, appUserId);
            }

            // DTO2 -> Map
            if (csvExchangeTransactionDtoList.type2Transactions.Any())
            {
                mappedTransactionList.AddRange(await _exchangeTransactionMapper.MapExchangeTransactionDtoToTransaction(csvExchangeTransactionDtoList.type2Transactions, appUserId));
            }

            // Step 3: Map -> DB
            var saved = false;
            if (mappedTransactionList.Any())
            {
                saved = await _transactionRepository.AddRangeAsync(mappedTransactionList);
            }

            // Return number of transactions imported
            if (saved)
                return mappedTransactionList.Count;
            else
                return 0;

        }


    }
}
