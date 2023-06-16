using AltFuture.MarketDataConsumer.Models.CoinMarketCap;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Security.Cryptography;
using System.Text;
using AltFuture.BusinessLogicLayer.ExchangTransactionApiImport.Services;

namespace AltFuture.WebApp.Areas.Portfolios.Controllers;

[Area("Portfolios")]
public class TransactionApiProfileController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly IExchangeTransactionApiDataSync _exchangeTransactionApiDataSync;

    public TransactionApiProfileController(IHttpClientFactory httpClientFactory, IExchangeTransactionApiDataSync exchangeTransactionApiDataSync)
    {
        _httpClient = httpClientFactory.CreateClient();
        _exchangeTransactionApiDataSync = exchangeTransactionApiDataSync;
    }
    public async Task<IActionResult> Index()
    {
        var appUserId = 1;


        await _exchangeTransactionApiDataSync.ImportDataAsync(appUserId);


        ////* API Location
        //_httpClient.BaseAddress = new Uri("https://api.coinbase.com");
        //var endpointUrl = $"/api/v3/brokerage/orders/historical/fills";

        ////* API Authentication
        //var apiKey = "KeyGoesHere";
        //var secretKey = "SecretGoesHere";

        ////* API Params
        //var limit = 100;

        ////* API Authorization
        //var unixTimestamp = ((int)DateTime.UtcNow.Subtract(new DateTime(1970,1,1)).TotalSeconds).ToString();
        //var signature = unixTimestamp + "GET" + endpointUrl;
        //var signatureHmacHex = "";

        //// Create a byte array of the secret key
        //byte[] secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

        //// Create a new HMAC-SHA256 object with the secret key
        //using (HMACSHA256 hmacSha256 = new HMACSHA256(secretKeyBytes))
        //{
        //    // Compute the HMAC for the given message
        //    byte[] signatureBytes = Encoding.UTF8.GetBytes(signature);
        //    byte[] hmacBytes = hmacSha256.ComputeHash(signatureBytes);

        //    // Convert the HMAC bytes to a hexadecimal string
        //    signatureHmacHex = BitConverter.ToString(hmacBytes).Replace("-", "").ToLowerInvariant();

        //}

        ////* API Headers
        //_httpClient.DefaultRequestHeaders.Add("accept", "application/json");
        //_httpClient.DefaultRequestHeaders.Add("CB-ACCESS-KEY", apiKey);
        //_httpClient.DefaultRequestHeaders.Add("CB-ACCESS-SIGN", signatureHmacHex);
        //_httpClient.DefaultRequestHeaders.Add("CB-ACCESS-TIMESTAMP", unixTimestamp.ToString());

        ////* API Call
        //HttpResponseMessage response = await _httpClient.GetAsync(endpointUrl.ToString() + $"?limit ={ limit}");


        ////* API Response
        //response.EnsureSuccessStatusCode();
        //JsonObject jsonResponce = await response.Content.ReadFromJsonAsync<JsonObject>();

        ////* API Data
        ////var fills = JsonConvert.DeserializeObject<CoinMarketCapPlanUsage>(jsonResponce["data"].ToString());
        //ViewBag.Fills = jsonResponce;

        return View();
    }
}
