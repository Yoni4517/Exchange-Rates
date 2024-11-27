using Microsoft.Extensions.Configuration;
using System.Text.Json;
namespace ExchangeRatesServer.Services;
public class RealExchangeRatesService : IExchangeRatesService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public RealExchangeRatesService(HttpClient httpClient, IConfiguration configuration)
    {
        var handler = new HttpClientHandler()
        {
            AllowAutoRedirect = true  
        };

        _httpClient = new HttpClient(handler);
        _apiKey = configuration.GetValue<string>("ExchangeRatesApi:ApiKey");
    }

    private static readonly List<string> PopularCurrencies = new List<string>
    {
        "USD",
        "EUR",
        "GBP",
        "CNY",
        "ILS"
    };

    public async Task<List<string>?> GetAvailableCurrencies()
    {
        var url = $"https://v6.exchangerate-api.com/v6/{_apiKey}/latest/EUR";
        var response = await _httpClient.GetAsync(url);


        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Error: {response.StatusCode}, Content: {errorContent}");
            return null;
        }

        var content = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var data = JsonSerializer.Deserialize<ExchangeRatesResponse>(content, options);

        if (data?.Conversion_Rates != null)
        {
            var filteredCurrencies = data.Conversion_Rates.Keys
                .Where(currency => PopularCurrencies.Contains(currency))
                .ToList();

            return filteredCurrencies;
        }

        return null;
    }


    public async Task<Dictionary<string, double>?> GetExchangeRatesForCurrency(string currencyName)
    {
        var url = $"https://v6.exchangerate-api.com/v6/{_apiKey}/latest/{currencyName}";
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Error: {response.StatusCode}, Content: {errorContent}");
            return null; 
        }

        var content = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true  
        };

        var data = JsonSerializer.Deserialize<ExchangeRatesResponse>(content, options);

        if (data == null || data.Conversion_Rates == null)
        {
            Console.WriteLine("No conversion rates data found.");
            return null;
        }
        var filteredRates = new Dictionary<string, double>();

        foreach (var currency in PopularCurrencies)
        {
            if (data.Conversion_Rates.ContainsKey(currency))
                filteredRates[currency] = data.Conversion_Rates[currency];
            else
                Console.WriteLine($"Currency {currency} not found.");
        }
        if (filteredRates.ContainsKey(currencyName))
        {
            filteredRates.Remove(currencyName);
        }

        return filteredRates;
    }

    public class ExchangeRatesResponse
    {
        public bool Success { get; set; }
        public Dictionary<string, double>? Conversion_Rates { get; set; }
    }

}
