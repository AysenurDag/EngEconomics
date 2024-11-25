using Binance.Net.Clients;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TradingBotApp.Api;

public class GetPrice(ILogger<GetPrice> logger)
{
    [Function("GetPrice")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        logger.LogInformation("C# HTTP trigger function processed a request.");
        var restClient = new BinanceRestClient();
        var tickerResult = await restClient.SpotApi.ExchangeData.GetTickerAsync("ETHUSDT");
        var lastPrice = tickerResult.Data.LastPrice;
        return new OkObjectResult($"last price : {lastPrice}");
        
    }

}