using Domain.Habit;

namespace Application.Habits.Calculations;

public interface IDateTimeOffsetIncrementer
{
    /// <summary>
    /// Increments the specified <see cref="DateTimeOffset"/> by a given number of time units.
    /// </summary>
    /// <param name="dateToIncrement">The date to be incremented.</param>
    /// <param name="timeUnit">The unit of time to increment by (Day, Week, Month, Year).</param>
    /// <param name="numberOfUnits">The number of time units to increment by.</param>
    /// <returns>A <see cref="DateTimeOffset"/> that has been incremented by the specified number of time units.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when an undefined <see cref="TimeUnit"/> is passed.</exception>
    DateTimeOffset Increment(DateTimeOffset dateToIncrement, TimeUnit timeUnit, int numberOfUnits);
}