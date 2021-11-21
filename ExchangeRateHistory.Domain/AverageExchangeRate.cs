namespace ExchangeRateHistory.Domain;

public class AverageExchangeRate {
    public decimal MinimumRate { get; }
    public DateTime MinimumRateDate { get; }
    public decimal MaximumRate { get; }
    public DateTime MaximumRateDate { get; }
    public decimal AverageRate { get; }

    public AverageExchangeRate(IEnumerable<ExchangeRate> rates) {
        if (rates == null || !rates.Any()) {
            throw new ArgumentNullException("null or empty ExchangeRate enumerable provided to constructor");
        }

        var minimumRate = rates.MinBy(r => r.Rate);
        var maximumRate = rates.MaxBy(r => r.Rate);
        var averageRate = rates.Average(r => r.Rate);

        MinimumRate = minimumRate.Rate;
        MinimumRateDate = minimumRate.Date;
        MaximumRate = maximumRate.Rate;
        MaximumRateDate = maximumRate.Date;
        AverageRate = averageRate;
    }
}

