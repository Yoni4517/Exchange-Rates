namespace ExchangeRatesServer.Services;

public interface IExchangeRatesService
{
    Task<List<string>> GetAvailableCurrenciesAsync();
    Task<Dictionary<string, double>> GetExchangeRatesForCurrencyAsync(string currencyName);
}
