using AutoMapper;
using AltFuture.DataAccessLayer.Models;
using AltFuture.WebApp.Areas.Portfolios.ViewModels;

namespace AltFuture.WebApp.Areas.Portfolios.AutoMapper;

public class TransactionTransactionEditProfile: Profile
{

    public TransactionTransactionEditProfile()
    {
        CreateMap<Transaction, TransactionEdit>()
            .ReverseMap();                
    }

}
