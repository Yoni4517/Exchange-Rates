using Microsoft.AspNetCore.Mvc;
using ExchangeRatesServer.Services;
using System.Net;

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
        try
        {
            var availableCurrencies = await _exchangeRatesService.GetAvailableCurrencies();
            if (availableCurrencies == null || !availableCurrencies.Any())
            {
                return NotFound("No currencies available.");
            }

            return Ok(availableCurrencies);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetching currencies.");
        }
    }

    [HttpGet("currencies/{currencyName}")]
    public async Task<ActionResult<Dictionary<string, double>>> GetExchangeRatesForCurrency(string currencyName)
    {
        if (string.IsNullOrWhiteSpace(currencyName))
        {
            return BadRequest("Currency name cannot be empty or null.");
        }

        try
        {
            var exchangeRates = await _exchangeRatesService.GetExchangeRatesForCurrency(currencyName);

            if (exchangeRates == null || !exchangeRates.Any())
            {
                return NotFound($"Currency '{currencyName}' not found.");
            }

            return Ok(exchangeRates);
        }

        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetching exchange rates.");
        }
    }

    [HttpGet]
    public ActionResult HandleInvalidRoute()
    {
        return NotFound("The requested API path is invalid.");
    }
}
