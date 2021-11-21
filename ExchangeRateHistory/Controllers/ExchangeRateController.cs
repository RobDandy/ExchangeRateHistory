using ExchangeRateHistory.Domain;
using ExchangeRateHistory.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExchangeRateHistory.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRateController : ControllerBase {
        private IExchangeRateService _exchangeRateService;

        public ExchangeRateController(IExchangeRateService exchangeRateService) {
            _exchangeRateService = exchangeRateService;
        }

        [HttpGet]
        public async Task<AverageExchangeRate> GetHistoricalRates(string baseCurrency, string targetCurrency, [FromQuery] DateTime[] dates) {
            var rates = await _exchangeRateService.GetRates(baseCurrency, targetCurrency, dates);
            return new AverageExchangeRate(rates);
        }
    }
}
