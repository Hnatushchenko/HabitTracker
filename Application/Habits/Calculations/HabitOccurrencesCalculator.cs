using Domain.Habit;

namespace Application.Habits.Calculations;

public sealed class HabitOccurrencesCalculator : IHabitOccurrencesCalculator
{
    /// <inheritdoc/>
    public bool ShouldHabitOccurOnSpecifiedDate(Habit habit, DateTimeOffset date)
    {
        var startDate = habit.StartDate;
        if (startDate.UtcDateTime.Date == date.UtcDateTime.Date)
        {
            return true;
        }

        var runner = habit.StartDate;
        while (runner.UtcDateTime.Date < date.UtcDateTime.Date)
        {
            runner = habit.FrequencyTimeUnit switch
            {
                TimeUnit.Day => runner.AddDays(habit.FrequencyCount.Value),
                TimeUnit.Week => runner.AddDays(habit.FrequencyCount.Value * 7),
                TimeUnit.Month => runner.AddMonths(habit.FrequencyCount.Value),
                TimeUnit.Year => runner.AddYears(habit.FrequencyCount.Value),
                _ => throw new ArgumentOutOfRangeException(nameof(date))
            };
            if (runner.UtcDateTime.Date == date.UtcDateTime.Date)
            {
                return true;
            }
        }

        return false;
    }
}