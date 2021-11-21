namespace ExchangeRateHistory.Domain;

public class AverageExchangeRate {
    public decimal MinimumRate { get; init; }
    public DateTime MinimumRateDate { get; init; }
    public decimal MaximumRate { get; init; }
    public DateTime MaximumRateDate { get; init; }
    public decimal AverageRate { get; init; }

    public static AverageExchangeRate Create(IEnumerable<ExchangeRate> rates) {
        if (rates == null || !rates.Any()) {
            throw new ArgumentNullException("null or empty ExchangeRate enumerable provided to constructor");
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
    private AverageExchangeRate() { }
}

