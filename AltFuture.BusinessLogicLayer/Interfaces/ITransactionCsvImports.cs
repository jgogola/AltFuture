using AltFuture.BusinessLogicLayer.Models.DTOs;

namespace AltFuture.BusinessLogicLayer.Interfaces
{
    public interface ITransactionCsvImports
    {

        Task<IEnumerable<CoinbaseTransactionHistoryDto>> ImportCoinbaseTransactionHistory(StreamReader reader);
    }
}
