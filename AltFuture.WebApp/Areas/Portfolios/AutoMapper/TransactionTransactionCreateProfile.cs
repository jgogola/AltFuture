using AutoMapper;
using AltFuture.DataAccessLayer.Models;
using AltFuture.WebApp.Areas.Portfolios.ViewModels;

namespace AltFuture.WebApp.Areas.Portfolios.AutoMapper;

public class TransactionTransactionCreateProfile: Profile
{

    public TransactionTransactionCreateProfile()
    {
        CreateMap<Transaction, TransactionCreate>()
            .ReverseMap();                
    }

}
