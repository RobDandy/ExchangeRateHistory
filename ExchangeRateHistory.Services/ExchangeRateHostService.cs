using ExchangeRateHistory.Services.Interfaces;

namespace ExchangeRateHistory.Services;

public class ExchangeRateHostService : IExchangeRateService {
    private readonly HttpClient _http;
    public ExchangeRateHostService(HttpClient http) {
        _http = http;
    }

    public async Task<decimal> GetRate(string baseCurrency, string targetCurrency, DateTime date) {
        try {
            var rsp = await _http.GetAsync($"{date:yyyy-MM-dd}?base={baseCurrency}&symbols={targetCurrency}");
        } catch (HttpRequestException ex) {
            // Logging
            Console.WriteLine($"{ex.Message} \n {ex.StackTrace}");
        }

        throw new NotImplementedException();
    }
}

