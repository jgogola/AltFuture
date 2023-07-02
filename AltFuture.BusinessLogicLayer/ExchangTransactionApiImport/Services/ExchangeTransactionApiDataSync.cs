using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.BusinessLogicLayer.ExchangTransactionApiImport.ApiClients;
using AltFuture.DataAccessLayer.Data.Enums;
using AltFuture.DataAccessLayer.Models;
using Newtonsoft.Json;
using AltFuture.BusinessLogicLayer.ExchangTransactionApiImport.JsonConverters;
using AltFuture.DataAccessLayer.Interfaces.Services;

namespace AltFuture.BusinessLogicLayer.ExchangTransactionApiImport.Services;

public class ExchangeTransactionApiDataSync : IExchangeTransactionApiDataSync
{

    private readonly IExchangeApiProfileRepository _exchangeApiProfileRepository;
    private readonly HttpClient _httpClient;
    private readonly List<Crypto> _cryptoAssets;
    private readonly List<ExchangeTransactionType> _exchangeTransactionTypes;
    private readonly ITransactionRepository _transactionRepository;

    public ExchangeTransactionApiDataSync(HttpClient httpClient,
                                          IExchangeApiProfileRepository exchangeApiProfileRepository,
                                          ICryptoDataService cryptoDataService,
                                          IExchangeTransactionTypeDataService exchangeTransactionTypeDataService,
                                          ITransactionRepository transactionRepository)
    {
        _httpClient = httpClient;
        _exchangeApiProfileRepository = exchangeApiProfileRepository;
        _cryptoAssets = cryptoDataService.CryptoList;
        _exchangeTransactionTypes = exchangeTransactionTypeDataService.ExchangeTransactionTypeList;
        _transactionRepository = transactionRepository;
    }


    //* Remove the return object for this method and only return "Task".
    //* Temporarily returning List<Transaction> for demo purposes.
    public async Task ImportDataAsync(int appUserId, int exchangeId = 0)
    {
        var createdDate = DateTime.Now;
        var exchangeApiProfiles = _exchangeApiProfileRepository.GetAllByUserId(appUserId)
                                                               .Where(p => p.IsEnabled == true
                                                                      && (exchangeId == 0 || p.ExchangeId == exchangeId)).ToList();


       foreach (var exchangeApiProfile in exchangeApiProfiles)
       {
            //*** Create ApiClient based on ExchangeId
            var exchangeType = (ExchangeEnum)exchangeApiProfile.ExchangeId;

            var exchangeTransactionApiClientFactory = new ExchangeTransactionApiClientFactory();
            var exchangeTransactionApiClient = exchangeTransactionApiClientFactory.CreateApiClient(exchangeType, _httpClient, _transactionRepository);

            //*** Create Factory for delivering JsonConverter based on ExchangeId
            var exchangeTransactionJsonConverterFactory = new ExchangeTransactionJsonConverterFactory();
            var exchangeTransactionJsonConverter = exchangeTransactionJsonConverterFactory.CreateJsonConverter(exchangeType, appUserId, createdDate, _cryptoAssets, _exchangeTransactionTypes);

            //*** Get Json Data from ApiClient
            var jsonExchangeTransactions = await exchangeTransactionApiClient.GetJsonDataAsync(exchangeApiProfile);

            //*** Parse Json Data
            List<Transaction> transactions = JsonConvert.DeserializeObject<List<Transaction>>(jsonExchangeTransactions.ToString(), exchangeTransactionJsonConverter);

            //*** Save Transactions to Database                     
            if(transactions is not null && transactions.Count() > 0){ 
                await _transactionRepository.AddRangeAsync(transactions);
                await _transactionRepository.SaveAsync();
            }
    } 

        
    }


}
