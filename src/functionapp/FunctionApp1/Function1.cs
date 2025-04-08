using System.Text;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionApp1
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        [Function("FunctionReserveOrder")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("Processing order items reservation.");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                string blobName = $"orders/{Guid.NewGuid()}.json";

                string connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");

                var blobClient = new BlobContainerClient(connectionString, "orders");
                await blobClient.CreateIfNotExistsAsync();

                var blob = blobClient.GetBlobClient(blobName);

                using var stream = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));
                await blob.UploadAsync(stream, overwrite: true);

                return new OkObjectResult("Order items reservation processed.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing order items reservation.");
                return new StatusCodeResult(500);
            }
        }
    }
}
