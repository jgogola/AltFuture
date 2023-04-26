using AutoMapper;
using AltFuture.DataAccessLayer.Models;
using AltFuture.BusinessLogicLayer.ViewModels.Transactions;

namespace AltFuture.BusinessLogicLayer.AutoMapper
{
    public class TransactionTransactionCreateProfile: Profile
    {

        public TransactionTransactionCreateProfile()
        {
            CreateMap<Transaction, TransactionCreate>()
                .ReverseMap();                
        }

    }
}
