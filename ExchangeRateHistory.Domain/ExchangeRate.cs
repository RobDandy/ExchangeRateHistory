namespace ExchangeRateHistory.Domain;

public class ExchangeRate {
    public string BaseCurrency { get; set; }
    public string TargetCurrency { get; set; }
    public decimal Rate { get; set; }
    public DateTime Date { get; set; }
}

