using AltFuture.DataAccessLayer.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AltFuture.DataAccessLayer.Services
{

    public class TransactionTypeDataService
    {
        private readonly Func<IServiceScope> _scopeFactory;
        private readonly ReaderWriterLockSlim _rwLock = new ReaderWriterLockSlim();

        private Dictionary<int, string> _transactionTypeDictionary = new Dictionary<int, string>();
        public Dictionary<int, string> TransactionTypeDictionary
        {
            get
            {
                _rwLock.EnterReadLock();
                try
                {
                    return new Dictionary<int, string>(_transactionTypeDictionary);
                }
                finally
                {
                    _rwLock.ExitReadLock();
                }
            }
        }

        public TransactionTypeDataService(Func<IServiceScope> scopeFactory)
        {
            _scopeFactory = scopeFactory;

            Task.Run(() =>
            {
                _ = UpdateDataAsync();
            });
        }

        public async Task UpdateDataAsync()
        {
            Dictionary<int, string> newTransactionTypeDictionary;

            using (var scope = _scopeFactory())
            {
                var transactionTypeRepository = scope.ServiceProvider.GetRequiredService<ITransactionTypeRepository>();

                newTransactionTypeDictionary = (await transactionTypeRepository.GetAllAsync())
                                                                                .ToDictionary(t => t.TransactionTypeId, t => t.TransactionTypeName);
            }

            _rwLock.EnterWriteLock();
            try
            {
                _transactionTypeDictionary = newTransactionTypeDictionary;
            }
            finally
            {
                _rwLock.ExitWriteLock();
            }
        }
    }

}
