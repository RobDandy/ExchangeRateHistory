using ExchangeRateHistory.Domain;

namespace ExchangeRateHistory.Services.Interfaces;

public interface IExchangeRateService {
    Task<ExchangeRate> GetRate(string baseCurrency, string targetCurrency, DateTime date);
}

