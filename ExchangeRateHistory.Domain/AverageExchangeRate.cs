namespace ExchangeRateHistory.Domain;

public class AverageExchangeRate {
    public decimal MinimumRate { get; private init; }
    public DateTime MinimumRateDate { get; private init; }
    public decimal MaximumRate { get; private init; }
    public DateTime MaximumRateDate { get; private init; }
    public decimal AverageRate { get; private init; }

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

