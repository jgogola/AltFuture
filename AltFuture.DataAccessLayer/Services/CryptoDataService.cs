using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.DataAccessLayer.Interfaces.Services;
using AltFuture.DataAccessLayer.Models;
using Microsoft.Extensions.DependencyInjection;

namespace AltFuture.DataAccessLayer.Services
{
    public class CryptoDataService : ICryptoDataService
    {
        private readonly Func<IServiceScope> _scopeFactory;
        private readonly ReaderWriterLockSlim _rwLock = new ReaderWriterLockSlim();

        private List<Crypto> _cryptos = new List<Crypto>();
        public List<Crypto> CryptoList
        {
            get
            {
                _rwLock.EnterReadLock();
                try
                {
                     return new List<Crypto>(_cryptos);
                }
                finally
                {
                    _rwLock.ExitReadLock();
                }
            } 
        } 

        public CryptoDataService(Func<IServiceScope> scopeFactory)
        {
            _scopeFactory = scopeFactory;

            // Use GetAwaiter().GetResult() to make the call synchronous
            UpdateDataAsync().GetAwaiter().GetResult();
        }

        public async Task UpdateDataAsync()
        {
            List<Crypto> newCryptos = new List<Crypto>();

            using (var scope = _scopeFactory())
            {
                var cryptoRepository = scope.ServiceProvider.GetRequiredService<ICryptoRepository>();

                newCryptos = (List<Crypto>)await cryptoRepository.GetAllAsync(); 
            }


            _rwLock.EnterWriteLock();
            try
            {
                _cryptos = newCryptos;
            }
            finally
            {
                _rwLock.ExitWriteLock();
            }
        
        }
    }
}
