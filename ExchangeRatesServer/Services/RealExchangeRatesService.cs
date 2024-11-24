using System.Text.Json;
namespace ExchangeRatesServer.Services;
public class RealExchangeRatesService : IExchangeRatesService
{
    private readonly HttpClient _httpClient;

    public RealExchangeRatesService(HttpClient httpClient)
    {
        var handler = new HttpClientHandler()
        {
            AllowAutoRedirect = true  
        };

        _httpClient = new HttpClient(handler);
    }

    public async Task<List<string>> GetAvailableCurrenciesAsync()
    {
        var url = "https://api.exchangeratesapi.io/v1/latest?access_key=9cef343033266bae9948c65a58955d1b";
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Error: {response.StatusCode}, Content: {errorContent}");
            return null;
        }

        var content = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<SymbolsResponse>(content);

        return data?.Symbols != null ? new List<string>(data.Symbols.Keys) : null;
    }

    public async Task<Dictionary<string, double>> GetExchangeRatesForCurrencyAsync(string currencyName)
    {
        var url = $"https://v6.exchangerate-api.com/v6/805a029d961b82554f6533c3/latest/{currencyName}";
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Error: {response.StatusCode}, Content: {errorContent}");
            return null; 
        }

        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Response Content: {content}");

        var data = JsonSerializer.Deserialize<ExchangeRatesResponse>(content);

        if (data == null || data.ConversionRates == null)
        {
            Console.WriteLine("No conversion rates data found.");
            return null;
        }
        var selectedCurrencies = new List<string> { "USD", "EUR", "GBP", "CNY", "ILS" };
        var filteredRates = new Dictionary<string, double>();

        foreach (var currency in selectedCurrencies)
        {
            if (data.ConversionRates.ContainsKey(currency))
            {
                filteredRates[currency] = data.ConversionRates[currency];
            }
            else
            {
                Console.WriteLine($"Currency {currency} not found.");
            }
        }

        return filteredRates;
    }

    public class SymbolsResponse
    {
        public string Result { get; set; }
        public Dictionary<string, string> Symbols { get; set; }
    }

    public class ExchangeRatesResponse
    {
        public string Result { get; set; }
        public Dictionary<string, double> ConversionRates { get; set; }
    }
}
