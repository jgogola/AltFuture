
namespace AltFuture.BusinessLogicLayer.ExchangeTransactionCsvImport.Interfaces
{
    public interface IExchangeTransactionCsvImport
    {

        Task<int> ImportCsvToDb<T>(StreamReader csvData, int appUserId, string delimiter = ",") where T : IExchangeTransactionDto;

        Task<int> ImportCsvToDb<T>(StreamReader csvData, int appUserId, int exchangeId, int[] transactionTypeFilter, string delimiter = ",") where T : IExchangeTransactionDto;

        Task<int> ImportCsvToDb<T1, T2>(StreamReader csvData,
                                            int appUserId,
                                            int exchageId,
                                            Dictionary<int, List<int>> transactionTypeFilter,
                                            string delimiter = ","
                                            ) where T1 : IExchangeTransactionDto
                                              where T2 : IExchangeTransactionDto;

    }
}
