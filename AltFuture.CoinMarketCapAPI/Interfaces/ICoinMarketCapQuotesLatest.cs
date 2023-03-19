using AltFuture.CoinMarketCapAPI.Models.EndPoints;
using AltFuture.DataAccessLayer.Interfaces.Services;
using AltFuture.DataAccessLayer.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AltFuture.Models;

namespace AltFuture.CoinMarketCapAPI.Interfaces
{
    public interface ICoinMarketCapQuotesLatest
    {

        Task<DateTime> SyncCacheAsync();

        Task<DateTime> SyncAsync();

    }
}
