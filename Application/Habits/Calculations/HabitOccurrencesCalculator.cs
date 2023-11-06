using Application.Extensions;
using Domain.Habit;

namespace Application.Habits.Calculations;

public sealed class HabitOccurrencesCalculator : IHabitOccurrencesCalculator
{
    /// <inheritdoc/>
    public bool ShouldHabitOccurOnSpecifiedDate(Habit habit, DateTimeOffset targetDate)
    {
        var startDate = habit.StartDate;
        if (startDate.HasUtcDateEqualTo(targetDate))
        {
            return true;
        }

        var runner = habit.StartDate;
        while (runner.HasUtcDateLessThen(targetDate))
        {
            runner = habit.FrequencyTimeUnit switch
            {
                TimeUnit.Day => runner.AddDays(habit.FrequencyCount.Value),
                TimeUnit.Week => runner.AddDays(habit.FrequencyCount.Value * 7),
                TimeUnit.Month => runner.AddMonths(habit.FrequencyCount.Value),
                TimeUnit.Year => runner.AddYears(habit.FrequencyCount.Value),
                _ => throw new ArgumentOutOfRangeException(nameof(targetDate))
            };
            if (runner.HasUtcDateEqualTo(targetDate))
            {
                return true;
            }
        }

        return false;
    }
}