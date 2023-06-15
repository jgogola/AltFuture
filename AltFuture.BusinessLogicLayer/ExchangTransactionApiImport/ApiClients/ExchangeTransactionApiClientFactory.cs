using AltFuture.DataAccessLayer.Data.Enums;
using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AltFuture.BusinessLogicLayer.ExchangTransactionApiImport.ApiClients;

internal class ExchangeTransactionApiClientFactory
{

    public ExchangeTransactionApiClientFactory()
    {

    }

    public IExchangeTransactionApiClient CreateApiClient(ExchangeEnum exchangeApiType, HttpClient httpClient, ITransactionRepository transactionRepository)
    {
        switch (exchangeApiType)
            {
            case ExchangeEnum.Coinbase:
                return new CoinbaseTransactionApiClient(httpClient, transactionRepository);
            //case ExchangeEnum.Binance:
            //    return new BinanceTransactionApiClient(httpClient);
            default:
                throw new NotImplementedException();
        }

    }
}
