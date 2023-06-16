using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;


namespace AltFuture.BusinessLogicLayer.ExchangTransactionApiImport.Dtos;

public interface IExchangeTransactionApiDto
{
    public int ExchangeId { get; }

    public string CryptoAsset { get; set; }

    public string ExchangeTransactionTypeName { get; set; }

    [Column(TypeName = "decimal(18,10)")]
    [DefaultValue(0.00)]
    public decimal Price { get; set; }

    [Column(TypeName = "decimal(18,10)")]
    [DefaultValue(0.00)]
    public decimal Quantity { get; set; }

    [Column(TypeName = "decimal(18,10)")]
    [DefaultValue(0.00)]
    public decimal Fee { get; set; }

    [Column(TypeName = "decimal(18,10)")]
    [DefaultValue(0.00)]
    public decimal TransactionTotal { get; set; }

    DateTime TransactionDate { get; set; }




}
