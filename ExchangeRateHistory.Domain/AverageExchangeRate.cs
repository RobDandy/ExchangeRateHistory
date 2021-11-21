namespace ExchangeRateHistory.Domain;

public class AverageExchangeRate {
    public decimal MinimumRate { get; set; }
    public DateTime MinimumRateDate { get; set; }
    public decimal MaximumRate { get; set; }
    public DateTime MaximumRateDate { get; set; }
    public decimal AverageRate { get; set; }
}

