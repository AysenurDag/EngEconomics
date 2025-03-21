﻿using Binance.Net.Clients;
using CryptoExchange.Net.Objects;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Binance.Spot;
using Binance.Spot.Models;
using CryptoExchange.Net.Authentication;

namespace TradingBotApp.Api;

public class GetStrategies(ILogger<GetStrategies> logger)
{
    [Function("GetStrategies")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        logger.LogInformation("C# HTTP trigger function processed a request.");
        string apiKey = "Your binance apiKey";
        string apiSecret = "Your binance secret apiKey";
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        dynamic data = JsonConvert.DeserializeObject(requestBody);
        string symbol = data?.symbol;
        var restClient = new BinanceRestClient();
        ApiCredentials apiCredentials = new ApiCredentials(apiKey, apiSecret);
        restClient.SetApiCredentials(apiCredentials);
        var endTime = DateTime.Now.ToFileTime();
        var startTime = DateTime.Now.AddDays(-90).ToFileTime();
        Market market = new Market();
        
        var bars = await market.KlineCandlestickData(symbol, Interval.ONE_DAY, startTime, endTime);
        
        return new OkObjectResult(JsonConvert.SerializeObject(bars));
        
    }

}
