namespace ExchangeRateHistory.Services.Interfaces;

public interface IExchangeRateService {
    Task<decimal> GetRate(string baseCurrency, string targetCurrency, DateTime date);
}

