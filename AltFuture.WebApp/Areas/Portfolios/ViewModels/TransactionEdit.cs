using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AltFuture.WebApp.Areas.Portfolios.ViewModels;

public class TransactionEdit
{

    public int TransactionId { get; set; }

    [DisplayName("Transaction Reference Number")]
    public int TransactionReferenceNum { get; set; } = 0;

    public int AppUserId { get; set; }

    [DisplayName("Crypto Currency")]
    public int CryptoId { get; set; }

    [DisplayName("Transaction Type")]
    [Required]
    public int ExchangeTransactionTypeId { get; set; }

    public decimal Price { get; set; } = decimal.Zero;

    public decimal Quantity { get; set; } = decimal.Zero;

    public decimal Fee { get; set; } = decimal.Zero;

    [DisplayName("Transaction Total")]
    public decimal TransactionTotal { get; set; } = decimal.Zero;

    [DisplayName("Transaction Date")]
    public DateTime TransactionDate { get; set; }

    [DisplayName("Exchange")]
    public int FromExchangeId { get; set; }

    private int? toExchangeId;
    [DisplayName("Transfered to Exchange")]
    public int? ToExchangeId { get => toExchangeId == 0 ? null : toExchangeId; set => toExchangeId = value; }

    [DisplayName("Date enterd into system")]
    public DateTime CreatedDate { get; set; }
}
