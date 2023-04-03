using AltFuture.BusinessLogicLayer.Interfaces.Models;
using AltFuture.BusinessLogicLayer.Models.ExchangeTransactions;

namespace AltFuture.BusinessLogicLayer.Interfaces
{
    public interface ITransactionCsvImports
    {

        Task<IEnumerable<CoinbaseTransactionHistoryDto>> ImportCoinbaseTransactionHistory(StreamReader reader);

        Task<IEnumerable<T>> ImportExchangeTransactionHistory<T>(StreamReader reader) where T : IExchangeTransactionHistoryDto;
        Task<IEnumerable<T>> ImportExchangeTransactionHistory<T>(StreamReader reader, int[]? transactionTypeFilter = null) where T : IExchangeTransactionHistoryDto;

        Task<(IEnumerable<T1> type1Transactions, IEnumerable<T2> type2Transactions)> ImportExchangeTransactionHistory<T1, T2>(StreamReader reader,
                                                                                                                              int exchageId,
                                                                                                                              Dictionary<int, List<int>> transactionTypeFilter
                                                                                                                             ) where T1 : IExchangeTransactionHistoryDto
                                                                                                                               where T2 : IExchangeTransactionHistoryDto;
    }
}
