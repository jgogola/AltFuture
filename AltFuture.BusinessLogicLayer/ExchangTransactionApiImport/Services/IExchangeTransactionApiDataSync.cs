using AltFuture.DataAccessLayer.Models;

namespace AltFuture.BusinessLogicLayer.ExchangTransactionApiImport.Services
{
    public interface IExchangeTransactionApiDataSync
    {
        Task ImportDataAsync(int appUserId, int exchangeId = 0);
    }
}