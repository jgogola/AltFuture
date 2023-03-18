﻿using AltFuture.BusinessLogicLayer.Models.DTOs;
using AltFuture.DataAccessLayer.Data.Enums;
using AltFuture.DataAccessLayer.Services;
using AltFuture.Models;
using AutoMapper;

namespace AltFuture.BusinessLogicLayer.AutoMapper.CoinbaseAssetToCryptoResolver
{
    internal class CoinbaseCryptoResolver : IValueResolver<CoinbaseTransactionHistoryDto, Transaction, int>
    {
        private readonly List<Crypto> _cryptoLookup;

        public CoinbaseCryptoResolver(CryptoDataService cryptoDataService)
        {
            _cryptoLookup = cryptoDataService.CryptoList;
        }

        public int Resolve(CoinbaseTransactionHistoryDto source, Transaction destination, int destMember, ResolutionContext context)
        {
            return _cryptoLookup.FirstOrDefault(c => c.TickerSymbol == source.Asset).CryptoId;
        }
    }
}