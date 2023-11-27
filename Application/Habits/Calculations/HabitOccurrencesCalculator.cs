using Domain.Habit;
using Helpers.Extensions;

namespace Application.Habits.Calculations;

public sealed class HabitOccurrencesCalculator : IHabitOccurrencesCalculator
{
    private static readonly Dictionary<DayOfWeek, DayOfWeekFrequency> DayOfWeekToDayOfWeekFrequencyMap = new()
    {
        { DayOfWeek.Sunday, DayOfWeekFrequency.OnSundays },
        { DayOfWeek.Monday, DayOfWeekFrequency.OnMondays },
        { DayOfWeek.Tuesday, DayOfWeekFrequency.OnTuesdays },
        { DayOfWeek.Wednesday, DayOfWeekFrequency.OnWednesdays },
        { DayOfWeek.Thursday, DayOfWeekFrequency.OnThursdays },
        { DayOfWeek.Friday, DayOfWeekFrequency.OnFridays },
        { DayOfWeek.Saturday, DayOfWeekFrequency.OnSaturdays },
    };
    /// <inheritdoc/>
    public bool ShouldHabitOccurOnSpecifiedDate(Habit habit, DateTimeOffset targetDate)
    {
        if (habit.DayOfWeekFrequency.HasFlag(DayOfWeekToDayOfWeekFrequencyMap[targetDate.DayOfWeek]))
        {
            return true;
        }
        
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