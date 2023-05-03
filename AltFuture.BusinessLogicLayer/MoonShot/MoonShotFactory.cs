using AltFuture.DataAccessLayer.Data;
using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.DataAccessLayer.Models;
using AltFuture.DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltFuture.BusinessLogicLayer.MoonShot;

public class MoonShotFactory : IMoonShotFactory
{
    private readonly AppDbContext _appDbContext;
    private readonly ITransactionRepository _transactionRepository;



    public MoonShotFactory(AppDbContext appDbContext, ITransactionRepository transactionRepository)
    {
        _appDbContext = appDbContext;
        _transactionRepository = transactionRepository;
    }

    public MoonShot Create(MoonShotTypeEnum moonShotType, int appUserId)
    {

        var moonShot = new MoonShot(_appDbContext, _transactionRepository, moonShotType, appUserId);
               
        return moonShot;
    }



}
