using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace AltFuture.BusinessLogicLayer.ExchangTransactionApiImport.ApiClients
{
    internal interface IExchangeTransactionApiClient 
    {
        Task<JsonArray> GetJsonDataAsync(ExchangeApiProfile exchangeApiProfile, DateTime? transactionsAfterDate = null);
    }
}
