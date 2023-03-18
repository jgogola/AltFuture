using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.DataAccessLayer.Interfaces.Services;
using AltFuture.Models;
using Microsoft.Extensions.DependencyInjection;

namespace AltFuture.DataAccessLayer.Services
{
    public class ExchangeTransactionTypeDataService// : IExchangeTransactionTypeDataService
    {
        private readonly Func<IServiceScope> _scopeFactory;
        private readonly ReaderWriterLockSlim _rwLock = new ReaderWriterLockSlim();

        private List<ExchangeTransactionType> _exchangeTransactionTypes = new List<ExchangeTransactionType>();
        public List<ExchangeTransactionType> ExchangeTransactionTypeList {
            get
            {
                _rwLock.EnterReadLock();
                try
                {
                     return new List<ExchangeTransactionType>(_exchangeTransactionTypes);
                }
                finally
                {
                    _rwLock.ExitReadLock();
                }
            } 
        } 

        public ExchangeTransactionTypeDataService(Func<IServiceScope> scopeFactory)
        {
            _scopeFactory = scopeFactory;

            // Use GetAwaiter().GetResult() to make the call synchronous
            UpdateDataAsync().GetAwaiter().GetResult();
        }

        public async Task UpdateDataAsync()
        {
            List<ExchangeTransactionType> newExchangeTransactionTypes = new List<ExchangeTransactionType>();

            using (var scope = _scopeFactory())
            {
                var exchangeTransactionTypeRepository = scope.ServiceProvider.GetRequiredService<IExchangeTransactionTypeRepository>();

                newExchangeTransactionTypes = (List<ExchangeTransactionType>)await exchangeTransactionTypeRepository.GetAllAsync(); 
            }


            _rwLock.EnterWriteLock();
            try
            {
                _exchangeTransactionTypes = newExchangeTransactionTypes;
            }
            finally
            {
                _rwLock.ExitWriteLock();
            }
        
        }
    }
}
