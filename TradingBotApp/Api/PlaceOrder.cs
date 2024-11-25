using Binance.Net.Clients;
using Binance.Net.Enums;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TradingBotApp.Api;

public class PlaceOrder(ILogger<PlaceOrder> logger)
{
    [Function("PlaceOrder")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        dynamic data = JsonConvert.DeserializeObject(requestBody);
        string symbol = data.symbol;
        var isBuy = (bool)data.isBuy?Binance.Net.Enums.OrderSide.Buy : Binance.Net.Enums.OrderSide.Sell;;
        decimal quantity = data.quantity;
        logger.LogInformation("C# HTTP trigger function processed a request.");
        var restClient = new BinanceRestClient();
        var result = await restClient.SpotApi.Trading.PlaceOrderAsync(
            symbol: symbol,
            side: OrderSide.Buy,
            type: Binance.Net.Enums.SpotOrderType.Market,
            quantity: quantity);
        return new OkObjectResult($"result: {result.Success}");
        
    }

}