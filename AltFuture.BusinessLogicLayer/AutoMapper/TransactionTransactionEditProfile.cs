using AutoMapper;
using AltFuture.DataAccessLayer.Models;
using AltFuture.BusinessLogicLayer.ViewModels.Transactions;

namespace AltFuture.BusinessLogicLayer.AutoMapper
{
    public class TransactionTransactionEditProfile: Profile
    {

        public TransactionTransactionEditProfile()
        {
            CreateMap<Transaction, TransactionEdit>()
                .ReverseMap();                
        }

    }
}
