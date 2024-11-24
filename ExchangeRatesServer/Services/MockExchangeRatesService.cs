namespace ExchangeRatesServer.Services;
public class MockExchangeRatesService : IExchangeRatesService
{
    private static readonly List<string> AvailableCurrencies = new List<string>
    {
        "USD",
        "EUR",
        "GBP",
        "CNY",
        "ILS"
    };

    private static readonly Random RandomGenerator = new Random();

    public async Task<List<string>> GetAvailableCurrenciesAsync()
    {
        int numberOfCurrencies = RandomGenerator.Next(3, 5);

        var randomCurrencies = AvailableCurrencies.OrderBy(x => RandomGenerator.Next()).Take(numberOfCurrencies).ToList();

        return await Task.FromResult(randomCurrencies);
    }

    public async Task<Dictionary<string, double>> GetExchangeRatesForCurrencyAsync(string currencyName)
    {
        var exchangeRates = new Dictionary<string, double>();

        if (AvailableCurrencies.Contains(currencyName))
        {
            foreach (var currency in AvailableCurrencies)
            {
                if (currency != currencyName) 
                {
                    var randomRate = RandomGenerator.NextDouble() * (2.0 - 0.5) + 0.5;
                    exchangeRates[currency] = Math.Round(randomRate, 4); 
                }
            }

            return await Task.FromResult(exchangeRates);
        }

        return await Task.FromResult<Dictionary<string, double>>(null);
    }
}
