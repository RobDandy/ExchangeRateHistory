using ExchangeRateHistory.Domain;
using ExchangeRateHistory.Domain.ExchangeRateHost;
using ExchangeRateHistory.Services.Interfaces;
using Newtonsoft.Json;

namespace ExchangeRateHistory.Services;

public class ExchangeRateHostService : IExchangeRateService {
    private readonly HttpClient _http;
    public ExchangeRateHostService(HttpClient http) {
        _http = http;
    }

    public async Task<IEnumerable<ExchangeRate>> GetRates(string baseCurrency, string targetCurrency, DateTime[] dates) {
        var rateTasks = new List<Task<ExchangeRate>>();

        foreach (var date in dates) {
            var task = GetRate(baseCurrency, targetCurrency, date);
            rateTasks.Add(task);
        }

        return await Task.WhenAll(rateTasks);
    }

    public async Task<ExchangeRate> GetRate(string baseCurrency, string targetCurrency, DateTime date) {
        try {
            var rsp = await _http.GetAsync($"{date:yyyy-MM-dd}?base={baseCurrency}&symbols={targetCurrency}");
            var json = await rsp.Content.ReadAsStringAsync();
            var ratesRsp = JsonConvert.DeserializeObject<HistoricalRatesRsp>(json);

            if (ratesRsp?.Rates?.TryGetValue(targetCurrency, out var rate) ?? false) {
                return new ExchangeRate {
                    BaseCurrency = ratesRsp.Base,
                    TargetCurrency = targetCurrency,
                    Rate = rate,
                    Date = ratesRsp.Date
                };
            } else {
                throw new NullReferenceException("No rates were returned from ExchangeRateHost");
            }
        } catch (HttpRequestException ex) {
            // Logging
            Console.WriteLine($"{ex.Message} \n {ex.StackTrace}");
            throw;
        } catch (NullReferenceException ex) {
            // Logging
            Console.WriteLine($"{ex.Message} \n {ex.StackTrace}");
            throw;
        }
    }
}

