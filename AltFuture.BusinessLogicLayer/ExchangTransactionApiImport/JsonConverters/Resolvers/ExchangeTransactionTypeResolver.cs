using AltFuture.BusinessLogicLayer.ExchangeTransactionCsvImport.Interfaces;
using AltFuture.DataAccessLayer.Data.Enums;
using AltFuture.DataAccessLayer.Interfaces.Services;
using AltFuture.DataAccessLayer.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltFuture.BusinessLogicLayer.ExchangTransactionApiImport.JsonConverters.Resolvers;

internal class ExchangeTransactionTypeResolver
{

    private readonly List<ExchangeTransactionType> _exchangeTransactionTypes;

    public ExchangeTransactionTypeResolver(List<ExchangeTransactionType> exchangeTransactionTypes)
    {
        _exchangeTransactionTypes = exchangeTransactionTypes;
    }

    public int Resolve(ExchangeEnum exchangeId, string exchangeTransactionTypeName)
    {
        return _exchangeTransactionTypes.FirstOrDefault(t => t.DataImportTypeId == (int)DataImportTypeEnum.API && 
                                                             t.ExchangeId == (int)exchangeId && 
                                                             t.ExchangeTransactionTypeName.ToLower() == exchangeTransactionTypeName.ToLower()).ExchangeTransactionTypeId;
    }

}
