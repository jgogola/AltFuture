using AltFuture.DataAccessLayer.Models;

namespace AltFuture.BusinessLogicLayer.ExchangTransactionApiImport.Services
{
    public interface IExchangeTransactionApiDataSync
    {
        Task<(int ImportCount, string ImportMessage)> ImportDataAsync(int appUserId, int exchangeId = 0);
    }
}