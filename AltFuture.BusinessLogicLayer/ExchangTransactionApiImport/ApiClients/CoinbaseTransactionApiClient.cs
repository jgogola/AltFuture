using AltFuture.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.DataAccessLayer.Data.Enums;

namespace AltFuture.BusinessLogicLayer.ExchangTransactionApiImport.ApiClients;

internal class CoinbaseTransactionApiClient : IExchangeTransactionApiClient
{

    private readonly HttpClient _httpClient;
    private readonly ITransactionRepository _transactionRepository;
    public CoinbaseTransactionApiClient(HttpClient httpClient, ITransactionRepository transactionRepository)
    {
        _httpClient = httpClient;
        _transactionRepository = transactionRepository;
    }


    public async Task<JsonArray> GetJsonDataAsync(ExchangeApiProfile exchangeApiProfile, DateTime? transactionsAfterDate = null)
    {
        //*** API Location
        _httpClient.BaseAddress = new Uri("https://api.coinbase.com");
        var endpointUrl = "/api/v3/brokerage/orders/historical/fills";

        //*** API Authentication
        var apiKey = exchangeApiProfile.ApiKey;
        var secretKey = exchangeApiProfile.ApiSecret;

        //*** API Authorization
        var unixTimestamp = ((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString();
        var signature = unixTimestamp + "GET" + endpointUrl;
        var signatureHmacHex = "";

        //* Create a byte array of the secret key
        byte[] secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

        //* Create a new HMAC-SHA256 object with the secret key
        using (HMACSHA256 hmacSha256 = new HMACSHA256(secretKeyBytes))
        {
            //* Compute the HMAC for the given message
            byte[] signatureBytes = Encoding.UTF8.GetBytes(signature);
            byte[] hmacBytes = hmacSha256.ComputeHash(signatureBytes);

            //* Convert the HMAC bytes to a hexadecimal string
            signatureHmacHex = BitConverter.ToString(hmacBytes).Replace("-", "").ToLowerInvariant();

        }

        //*** API Headers
        _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
        _httpClient.DefaultRequestHeaders.Add("CB-ACCESS-KEY", apiKey);
        _httpClient.DefaultRequestHeaders.Add("CB-ACCESS-SIGN", signatureHmacHex);
        _httpClient.DefaultRequestHeaders.Add("CB-ACCESS-TIMESTAMP", unixTimestamp.ToString());


        //*** API Params 
        DateTime startSequenceTimestamp = DateTime.Now; 
        if(transactionsAfterDate is not null)
        {
            startSequenceTimestamp = (DateTime)transactionsAfterDate;
        }
        else
        {
            //If transactionsAfterDate is null then find the max TransactionDate in the Transaction table.
            startSequenceTimestamp = await _transactionRepository.GetLatestTransactionDateAsync(exchangeApiProfile.AppUserId, (int)ExchangeEnum.Coinbase);
            startSequenceTimestamp = startSequenceTimestamp.AddSeconds(1);
        }

        var startSequenceTimestampUTC = startSequenceTimestamp.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
        int limit = 100;

        endpointUrl = endpointUrl.ToString() + $"?start_sequence_timestamp={startSequenceTimestampUTC}&limit={limit}";

        //********
        //* TODO: Handle Paging. The max return is 100.
        //*******

        //*** API Call
        HttpResponseMessage response = await _httpClient.GetAsync(endpointUrl);


        //*** API Response
        response.EnsureSuccessStatusCode();
        JsonObject jsonResponce = await response.Content.ReadFromJsonAsync<JsonObject>();


        //*** API Data
        return (JsonArray)jsonResponce["fills"];
    }
}
