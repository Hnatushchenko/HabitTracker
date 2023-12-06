using Domain.Habit;

namespace Application.Habits.Calculations;

public sealed class DateTimeOffsetIncrementer : IDateTimeOffsetIncrementer
{
    /// <inheritdoc />
    public DateTimeOffset Increment(DateTimeOffset dateToIncrement, TimeUnit timeUnit, int numberOfUnits)
    {
        var result = timeUnit switch
        {
            TimeUnit.Day => dateToIncrement.AddDays(numberOfUnits),
            TimeUnit.Week => dateToIncrement.AddDays(numberOfUnits * 7),
            TimeUnit.Month => dateToIncrement.AddMonths(numberOfUnits),
            TimeUnit.Year => dateToIncrement.AddYears(numberOfUnits),
            _ => throw new ArgumentOutOfRangeException(nameof(timeUnit))
        };
        return result;
    }
}