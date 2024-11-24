using Microsoft.AspNetCore.Mvc;
using ExchangeRatesServer.Services;

namespace ExchangeRatesServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExchangeRatesController : ControllerBase
{
    private readonly IExchangeRatesService _exchangeRatesService;

    public ExchangeRatesController(IExchangeRatesService exchangeRatesService)
    {
        _exchangeRatesService = exchangeRatesService;
    }

    [HttpGet("currencies")]
    public async Task<ActionResult<List<string>>> GetAvailableCurrencies()
    {
        var availableCurrencies = await _exchangeRatesService.GetAvailableCurrenciesAsync();
        return Ok(availableCurrencies);
    }

    [HttpGet("currencies/{currencyName}")]
    public async Task<ActionResult<Dictionary<string, double>>> GetExchangeRatesForCurrency(string currencyName)
    {
        var exchangeRates = await _exchangeRatesService.GetExchangeRatesForCurrencyAsync(currencyName);
        if (exchangeRates == null)
        {
            return NotFound($"Currency '{currencyName}' not found.");
        }
        return Ok(exchangeRates);
    }
}
