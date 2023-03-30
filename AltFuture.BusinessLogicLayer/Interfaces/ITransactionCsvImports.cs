using AltFuture.BusinessLogicLayer.Models.ExchangeTransactions;

namespace AltFuture.BusinessLogicLayer.Interfaces
{
    public interface ITransactionCsvImports
    {

        Task<IEnumerable<CoinbaseTransactionHistoryDto>> ImportCoinbaseTransactionHistory(StreamReader reader);
    }
}
