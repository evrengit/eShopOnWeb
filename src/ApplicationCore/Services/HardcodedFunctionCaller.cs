using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Services;
public class HardcodedFunctionCaller
{
    private static readonly HttpClient _httpClient = new HttpClient();

    public async Task<string> CallOrderItemsReserverAsync(string serializedOrderItems)
    {
        string functionUrl = "https://orderitemsreserverapp787688.azurewebsites.net/api/FunctionReserveOrder";
        string functionKey = "FunctionReserveOrder";

        string requestUrl = $"{functionUrl}?code={functionKey}";

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode(); // Throw if not a success code.
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error calling Azure Function: {ex.Message}");
            throw;
        }
    }
}
