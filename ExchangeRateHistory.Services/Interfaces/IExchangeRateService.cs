using ExchangeRateHistory.Domain;

namespace ExchangeRateHistory.Services.Interfaces;

public interface IExchangeRateService {
    Task<IEnumerable<ExchangeRate>> GetRates(string baseCurrency, string targetCurrency, DateTime[] dates);
    Task<ExchangeRate> GetRate(string baseCurrency, string targetCurrency, DateTime date);
}

