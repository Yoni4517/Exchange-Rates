namespace ExchangeRatesServer.Services;

public interface IExchangeRatesService
{
    Task<List<string>?> GetAvailableCurrencies();
    Task<Dictionary<string, double>?> GetExchangeRatesForCurrency(string currencyName);
}
