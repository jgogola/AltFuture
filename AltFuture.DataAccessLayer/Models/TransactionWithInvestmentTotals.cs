using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltFuture.DataAccessLayer.Models
{
    public class TransactionWithInvestmentTotals
    {
        public int TransactionId { get; set; }
        public int TransactionReferenceNum { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int CryptoId { get; set; }
        public Crypto Crypto { get; set; }
        public int ExchangeTransactionTypeId { get; set; }
        public ExchangeTransactionType ExchangeTransactionType { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal Fee { get; set; }
        public decimal TransactionTotal { get; set; }
        public DateTime TransactionDate { get; set; }
        public int FromExchangeId { get; set; }
        public Exchange FromExchange { get; set; }
        public int? ToExchangeId { get; set; }
        public Exchange? ToExchange { get; set; }
        public DateTime CreatedDate { get; set; }
        public int TransactionTypeId { get; set; }
        public decimal InvestmentTotal { get; set; }
    }
}
