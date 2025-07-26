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
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Jose;
using Azure.Core;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AltFuture.BusinessLogicLayer.ExchangTransactionApiImport.ApiClients;

internal class CoinbaseTransactionApiClient : IExchangeTransactionApiClient
{

    private readonly HttpClient _httpClient;
    private readonly ITransactionRepository _transactionRepository;
    static Random random = new Random();
    public CoinbaseTransactionApiClient(HttpClient httpClient, ITransactionRepository transactionRepository)
    {
        _httpClient = httpClient;
        _transactionRepository = transactionRepository;
    }


    //public async Task<JsonArray> GetJsonDataAsync(ExchangeApiProfile exchangeApiProfile, DateTime? transactionsAfterDate = null)
    //{
    //    //*** API Location
    //    _httpClient.BaseAddress = new Uri("https://api.coinbase.com");
    //    var endpointUrl = "/api/v3/brokerage/orders/historical/fills";

    //    //*** API Authentication
    //    var apiKey = exchangeApiProfile.ApiKey;
    //    var secretKey = exchangeApiProfile.ApiSecret;

    //    //*** API Authorization
    //    var unixTimestamp = ((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString();
    //    var signature = unixTimestamp + "GET" + endpointUrl;
    //    var signatureHmacHex = "";

    //    //* Create a byte array of the secret key
    //    byte[] secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

    //    //* Create a new HMAC-SHA256 object with the secret key
    //    using (HMACSHA256 hmacSha256 = new HMACSHA256(secretKeyBytes))
    //    {
    //        //* Compute the HMAC for the given message
    //        byte[] signatureBytes = Encoding.UTF8.GetBytes(signature);
    //        byte[] hmacBytes = hmacSha256.ComputeHash(signatureBytes);

    //        //* Convert the HMAC bytes to a hexadecimal string
    //        signatureHmacHex = BitConverter.ToString(hmacBytes).Replace("-", "").ToLowerInvariant();

    //    }

    //    //*** API Headers
    //    _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
    //    _httpClient.DefaultRequestHeaders.Add("CB-ACCESS-KEY", apiKey);
    //    _httpClient.DefaultRequestHeaders.Add("CB-ACCESS-SIGN", signatureHmacHex);
    //    _httpClient.DefaultRequestHeaders.Add("CB-ACCESS-TIMESTAMP", unixTimestamp.ToString());


    //    //*** API Params 
    //    DateTime startSequenceTimestamp = DateTime.Now; 
    //    if(transactionsAfterDate is not null)
    //    {
    //        startSequenceTimestamp = (DateTime)transactionsAfterDate;
    //    }
    //    else
    //    {
    //        //If transactionsAfterDate is null then find the max TransactionDate in the Transaction table.
    //        startSequenceTimestamp = await _transactionRepository.GetLatestTransactionDateAsync(exchangeApiProfile.AppUserId, (int)ExchangeEnum.Coinbase);
    //        startSequenceTimestamp = startSequenceTimestamp.AddSeconds(1);
    //    }

    //    var startSequenceTimestampUTC = startSequenceTimestamp.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
    //    int limit = 100;

    //    endpointUrl = endpointUrl.ToString() + $"?start_sequence_timestamp={startSequenceTimestampUTC}&limit={limit}";

    //    //********
    //    //* TODO: Handle Paging. The max return is 100.
    //    //*******

    //    //*** API Call
    //    HttpResponseMessage response = await _httpClient.GetAsync(endpointUrl);


    //    //*** API Response
    //    response.EnsureSuccessStatusCode();
    //    JsonObject jsonResponce = await response.Content.ReadFromJsonAsync<JsonObject>();


    //    //*** API Data
    //    return (JsonArray)jsonResponce["fills"];
    //}


    public async Task<JsonArray> GetJsonDataAsync(ExchangeApiProfile exchangeApiProfile, DateTime? transactionsAfterDate = null)
    {
        //*** API Location
        _httpClient.BaseAddress = new Uri("https://api.coinbase.com");
        var endpointUrl = "/api/v3/brokerage/orders/historical/fills";

        //*** API Authentication
        var apiKey = exchangeApiProfile.ApiKey;
        var secretKey = exchangeApiProfile.ApiSecret;
        var bearerToken = generateToken(apiKey, secretKey, "GET api.coinbase.com" + endpointUrl);


        //*** API Params 
        DateTime startSequenceTimestamp = DateTime.Now;
        if (transactionsAfterDate is not null)
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

        //*** API Client
        HttpResponseMessage response;
        JsonObject jsonPayload;
       
        using (var request = new HttpRequestMessage(HttpMethod.Get, endpointUrl))
        {
            //*** API Request
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);            
            response = await _httpClient.SendAsync(request);

            //*** API Response
            response.EnsureSuccessStatusCode();
            jsonPayload = await response.Content.ReadFromJsonAsync<JsonObject>();
        }

        //*** API Data
        return (JsonArray)jsonPayload["fills"];
    }

    private static string generateToken(string name, string secret, string uri)
    {
        var parsedKSecret = parseKey(secret);

        try
        {
            var privateKeyBytes = Convert.FromBase64String(parsedKSecret); // Assuming PEM is base64 encoded

            using var key = ECDsa.Create();
            key.ImportECPrivateKey(privateKeyBytes, out _);

            var payload = new Dictionary<string, object>
             {
                 { "sub", name },
                 { "iss", "coinbase-cloud" },
                 { "nbf", Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds) },
                 { "exp", Convert.ToInt64((DateTime.UtcNow.AddMinutes(1) - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds) },
                 { "uri", uri }
             };

            var extraHeaders = new Dictionary<string, object>
            {
                { "alg", "ES256" },
                { "typ", "JWT"},
                { "kid", name },
                // add nonce to prevent replay attacks with a random 10 digit number
                { "nonce", randomHex(10) }                
             };

            var encodedToken = JWT.Encode(payload, key, JwsAlgorithm.ES256, extraHeaders);

            // print token
            Console.WriteLine(encodedToken);
            return encodedToken;
        } catch (Exception e)
        {
            var x = e;
        }

        return "";
    }

    public static bool isTokenValid(string token, string tokenId, string secret)
    {
        if (token == null)
            return false;

        var parsedKSecret = parseKey(secret);

        var key = ECDsa.Create();
        key?.ImportECPrivateKey(Convert.FromBase64String(parsedKSecret), out _);

        var securityKey = new ECDsaSecurityKey(key) { KeyId = tokenId };

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out var validatedToken);

            return true;
        }
        catch
        {
            return false;
        }
    }

    private static string parseKey(string key)
    {
        key = key.Replace("\\n", "\n");
        List<string> keyLines = new List<string>();
        keyLines.AddRange(key.Split('\n', StringSplitOptions.RemoveEmptyEntries));

        keyLines.RemoveAt(0);
        keyLines.RemoveAt(keyLines.Count - 1);

        return String.Join("", keyLines);
    }


    private static string randomHex(int digits)
    {
        byte[] buffer = new byte[digits / 2];
        random.NextBytes(buffer);
        string result = String.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
        if (digits % 2 == 0)
            return result;
        return result + random.Next(16).ToString("X");
    }


}
