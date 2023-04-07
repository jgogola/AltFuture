

namespace AltFuture.BusinessLogicLayer.ExchangeTransactionCsvImport.Interfaces
{
    public interface IExchangeTransactionCsvParser
    {

        Task<IEnumerable<T>> ParseExchangeTransactionCsvToDto<T>(StreamReader csvData, string delimiter = ",") where T : IExchangeTransactionDto;
       
        Task<IEnumerable<T>> ParseExchangeTransactionCsvToDto<T>(StreamReader csvData, int[] transactionTypeFilter, string delimiter = ",") where T : IExchangeTransactionDto;

        Task<(IEnumerable<T1> type1Transactions, IEnumerable<T2> type2Transactions)> ParseExchangeTransactionCsvToDto<T1, T2>(StreamReader csvData,
                                                                                                                                      int exchageId,
                                                                                                                                      Dictionary<int, List<int>> transactionTypeFilter,
                                                                                                                                      string delimiter = ","
                                                                                                                                     ) where T1 : IExchangeTransactionDto
                                                                                                                                       where T2 : IExchangeTransactionDto;
    }
}
