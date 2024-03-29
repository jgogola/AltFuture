﻿using AltFuture.DataAccessLayer.Models;

namespace AltFuture.DataAccessLayer.Interfaces
{
    public interface IExchangeRepository
    {
        Task<IEnumerable<Exchange>> GetAllAsync();

        Task<Exchange> GetByIdAsync(int id);

        Task<int> GetCountAsync();
        bool Add(Exchange exchange);

        bool Update(Exchange exchange);

        bool Delete(Exchange exchange);

        bool Save();
    }
}
