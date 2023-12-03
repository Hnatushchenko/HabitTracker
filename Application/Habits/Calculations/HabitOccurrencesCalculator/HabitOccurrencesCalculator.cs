using Application.Frequency.DayOfWeekToDayOfWeekFrequencyMapper;
using Application.Habits.Calculations.DateTimeOffsetIncrementer;
using Domain.Habit;
using Helpers.Extensions;

namespace Application.Habits.Calculations.HabitOccurrencesCalculator;

public sealed class HabitOccurrencesCalculator : IHabitOccurrencesCalculator
{
    private readonly IDayOfWeekToDayOfWeekFrequencyMapper _dayOfWeekToDayOfWeekFrequencyMapper;
    private readonly IDateTimeOffsetIncrementer _dateTimeOffsetIncrementer;

    public HabitOccurrencesCalculator(IDayOfWeekToDayOfWeekFrequencyMapper dayOfWeekToDayOfWeekFrequencyMapper,
        IDateTimeOffsetIncrementer dateTimeOffsetIncrementer)
    {
        _dayOfWeekToDayOfWeekFrequencyMapper = dayOfWeekToDayOfWeekFrequencyMapper;
        _dateTimeOffsetIncrementer = dateTimeOffsetIncrementer;
    }
    
    /// <inheritdoc/>
    public bool ShouldHabitOccurOnSpecifiedDate(Habit habit, DateTimeOffset targetDate)
    {
        var dayOfWeekFrequency = _dayOfWeekToDayOfWeekFrequencyMapper.Map(targetDate.DayOfWeek);
        if (habit.DayOfWeekFrequency.HasFlag(dayOfWeekFrequency))
        {
            return true;
        }
        
        var startDate = habit.StartDate;
        if (startDate.HasUtcDateEqualTo(targetDate))
        {
            return true;
        }

        var runner = habit.StartDate;
        while (runner.HasUtcDateLessThan(targetDate))
        {
            var frequencyCount = habit.FrequencyCount.Value;
            runner = _dateTimeOffsetIncrementer.Increment(runner, habit.FrequencyTimeUnit, frequencyCount);
            if (runner.HasUtcDateEqualTo(targetDate))
            {
                return true;
            }
        }

        return false;
    }
    
    public IEnumerable<DateTimeOffset> GetHabitOccurrences(Habit habit, DateTimeOffset upToDateInclusive)
    {
        var runner = habit.StartDate;
        while (runner.HasUtcDateLessThanOrEqualTo(upToDateInclusive))
        {
            if (habit.DayOfWeekFrequency == DayOfWeekFrequency.None)
            {
                yield return runner;
                var frequencyCount = habit.FrequencyCount.Value;
                runner = _dateTimeOffsetIncrementer.Increment(runner, habit.FrequencyTimeUnit, frequencyCount);
            }
            else
            {
                if (ShouldHabitOccurOnSpecifiedDate(habit, runner))
                {
                    yield return runner;
                }

                runner = runner.AddDays(1);
            }
        }
    }
}