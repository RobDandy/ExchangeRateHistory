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
            var rates = new List<ExchangeRate>();

            foreach (var date in dates) {
                var rate = await _exchangeRateService.GetRate(baseCurrency, targetCurrency, date);
                rates.Add(rate);
            }

            var minimumRate = rates.MinBy(r => r.Rate);
            var maximumRate = rates.MaxBy(r => r.Rate);
            var averageRate = rates.Average(r => r.Rate);

            return new AverageExchangeRate {
                MinimumRate = minimumRate.Rate,
                MinimumRateDate = minimumRate.Date,
                MaximumRate = maximumRate.Rate,
                MaximumRateDate = maximumRate.Date,
                AverageRate = averageRate
            };
        }
    }
}
