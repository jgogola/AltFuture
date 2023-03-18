using AltFuture.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltFuture.DataAccessLayer.Interfaces.Services
{
    public interface IExchangeTransactionTypeDataService
    {
        List<ExchangeTransactionType> ExchangeTransactionTypeList { get; }

        Task UpdateDataAsync();
    }
}
