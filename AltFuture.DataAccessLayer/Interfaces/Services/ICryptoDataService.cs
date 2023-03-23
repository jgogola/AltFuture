using AltFuture.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltFuture.DataAccessLayer.Interfaces.Services
{
    public interface ICryptoDataService
    {

        List<Crypto> CryptoList { get; }

        Task UpdateDataAsync();
    }
}
