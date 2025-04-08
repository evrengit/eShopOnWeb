using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace OrderItemsReserverApp787688
{
    public class OrderItemsReserver
    {
        private readonly ILogger<OrderItemsReserver> _logger;

        public OrderItemsReserver(ILogger<OrderItemsReserver> logger)
        {
            _logger = logger;
        }

        [Function("FunctionReserveOrder")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
