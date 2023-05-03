using AltFuture.DataAccessLayer.Data;
using AltFuture.DataAccessLayer.Data.Enums;
using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltFuture.BusinessLogicLayer.MoonShot;

public class MoonShot : IMoonShot
{
    private readonly AppDbContext _appDbContext;
    private readonly ITransactionRepository _transactionRepository;
    private readonly int _appUserId;
    private record MoonShotData(MoonShotTypeEnum MoonShotType, int CryptoId, decimal Price, decimal Quantity, DateTime TransactionDate);

    public MoonShotTypeEnum MoonShotType { get; private set; }
    public int MoonShotTransactionReferenceNum { get; private set; }
    public bool MoonShotExists { get; private set; }


    public MoonShot(AppDbContext appDbContext, ITransactionRepository transactionRepository, MoonShotTypeEnum moonShotType, int appUserId)
    {
        _appDbContext = appDbContext;
        _transactionRepository = transactionRepository;
        _appUserId = appUserId;

        MoonShotType = moonShotType;
        MoonShotTransactionReferenceNum = (int)moonShotType * 1111;
        var count = _appDbContext.Transactions.Where(t => t.TransactionReferenceNum == MoonShotTransactionReferenceNum & t.AppUserId == appUserId).Count();
        MoonShotExists = count > 0 ? true : false;
    }

    public bool Add()
    {

        if (MoonShotExists)
        {
            return false;
        }

        var moonShot = GetMoonShotData();
        if (moonShot is null)
        {
            return false;
        }

        int CoinbaseBuyTypeId = 1;

        var transaction = new Transaction
        {
            TransactionReferenceNum = MoonShotTransactionReferenceNum
            ,
            AppUserId = _appUserId
            ,
            CryptoId = moonShot.CryptoId
            ,
            ExchangeTransactionTypeId = CoinbaseBuyTypeId
            ,
            Price = moonShot.Price
            ,
            Quantity = moonShot.Quantity
            ,
            Fee = 0
            ,
            TransactionTotal = moonShot.Price * moonShot.Quantity
            ,
            TransactionDate = moonShot.TransactionDate
            ,
            FromExchangeId = (int)ExchangeEnum.Coinbase
            ,
            ToExchangeId = null
            ,
            CreatedDate = DateTime.Now
        };

        return _transactionRepository.Add(transaction);
    }

    public bool Remove()
    {

        Transaction? transaction = _appDbContext.Transactions.Where(t => t.TransactionReferenceNum == MoonShotTransactionReferenceNum & t.AppUserId == _appUserId).FirstOrDefault();

        if (transaction is null)
        {
            return false;
        }
        else
        {
            return _transactionRepository.Delete(transaction);
        }

    }

    private MoonShotData? GetMoonShotData()
    {
        var MoonShots = new List<MoonShotData>()
        {
            new MoonShotData(MoonShotTypeEnum.Bitcoin, 1, 0, 0, new DateTime(2021,09,01)),
            new MoonShotData(MoonShotTypeEnum.ShibaInu, 6, 0.0000075m, 1333400000.00m, new DateTime(2021, 9, 15))
        };

        return MoonShots.Where(m => m.MoonShotType == MoonShotType).FirstOrDefault();

    }


}
 