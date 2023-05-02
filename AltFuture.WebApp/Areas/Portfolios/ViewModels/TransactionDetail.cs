using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AltFuture.WebApp.Areas.Portfolios.ViewModels;

public class TransactionDetail
{

    public int TransactionId { get; set; }


    [DisplayName("Transaction Reference Number")]
    public int TransactionReferenceNum { get; set; } 


    public int AppUserId { get; set; }

    [DisplayName("User Name")]
    public string UserName { get; set; } = string.Empty;


    public int CryptoId { get; set; }

    [DisplayName("Crypto Currency")]
    public string CryptoName { get; set; } = string.Empty;

    public string CryptoIcon { get; set; } = string.Empty;



    public int ExchangeTransactionTypeId { get; set; }

    [DisplayName("Transaction Type")]
    public string ExchangeTransactionTypeName { get; set; } = string.Empty;


    [DisplayFormat(DataFormatString = "{0:C}")]
    public decimal Price { get; set; } = decimal.Zero;

  
    [DisplayFormat(DataFormatString = "{0:N6}")]
    //[DisplayFormat(DataFormatString = "{#######0.##########}")]
    public decimal Quantity { get; set; } = decimal.Zero;

   
    [DisplayName("Investment Total"), DisplayFormat(DataFormatString = "{0:C}")]
    public decimal InvestmentTotal { get; set; } = decimal.Zero;

  
    [DisplayFormat(DataFormatString = "{0:C}")]
    public decimal Fee { get; set; } = decimal.Zero;

   
    [DisplayName("Transaction Total"), DisplayFormat(DataFormatString = "{0:C}")]
    
    public decimal TransactionTotal { get; set; } = decimal.Zero;

   
    [DisplayName("Transaction Date")]
    public DateTime TransactionDate { get; set; }

    
    public int FromExchangeId { get; set; }

    [DisplayName("Exchange")]
    public string FromExchangeName { get; set; } = string.Empty;


    public int? ToExchangeId { get; set; }

    [DisplayName("Transfered to Exchange")]
    public string ToExchangeName { get; set; } = string.Empty;

    [DisplayName("Date enterd into system")]
    public DateTime CreatedDate { get; set; }

}
