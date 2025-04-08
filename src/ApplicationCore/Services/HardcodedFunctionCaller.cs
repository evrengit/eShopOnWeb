using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Microsoft.eShopWeb.ApplicationCore.Services;

public class HardcodedFunctionCaller
{
    private readonly IConfiguration _configuration;

    public HardcodedFunctionCaller(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private static readonly HttpClient _httpClient = new HttpClient();

    public async Task<string> CallOrderItemsReserverAsync(string serializedOrderItems)
    {
        string functionUrl = "https://orderitemsreserverapp787688.azurewebsites.net/api/FunctionReserveOrder";

        string functionKey =  _configuration["OrderItemsReserveKey"] ?? string.Empty;

        string requestUrl = $"{functionUrl}?code={functionKey}";

        try
        {
            var content = new StringContent(serializedOrderItems, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(requestUrl, content);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Function CALL =  = = == = = = =======================");
            return responseBody;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error calling Azure Function: {ex.Message}");
            throw;
        }
    }
}
