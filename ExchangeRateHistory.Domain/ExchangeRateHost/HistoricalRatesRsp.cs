namespace ExchangeRateHistory.Domain.ExchangeRateHost;

public class HistoricalRatesRsp {
    public bool Success { get; set; }
    public string Base { get; set; }
    public DateTime Date { get; set; }
    public Dictionary<string, decimal> Rates { get; set; }
}

