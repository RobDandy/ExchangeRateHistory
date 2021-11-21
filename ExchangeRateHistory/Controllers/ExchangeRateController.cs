using ExchangeRateHistory.Domain;
using ExchangeRateHistory.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRateHistory.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExchangeRateController : ControllerBase {
    private readonly IExchangeRateService _exchangeRateService;

    public ExchangeRateController(IExchangeRateService exchangeRateService) {
        _exchangeRateService = exchangeRateService;
    }

    [HttpGet]
    public async Task<IActionResult> GetHistoricalRates(string baseCurrency, string targetCurrency, [FromQuery] DateTime[] dates) {
        if (dates == null) {
            return BadRequest(new { Error = "No dates provided to constructor" });
        }

        IEnumerable<ExchangeRate> rates;

        try {
            rates = await _exchangeRateService.GetRates(baseCurrency, targetCurrency, dates);
        } catch (NullReferenceException ex) {
            return BadRequest(ex.Message);
        } catch (Exception ex) {
            // Logging
            Console.WriteLine($"{ex.Message} \n {ex.StackTrace}");
            return BadRequest("An unknown error occurred");
        }

        AverageExchangeRate averageRate;

        try {
            averageRate = AverageExchangeRate.Create(rates);
        } catch (ArgumentNullException) {
            return NotFound(new { Error = "No rates found for specified currencies and dates" });
        }

        return Ok(averageRate);
    }
}

