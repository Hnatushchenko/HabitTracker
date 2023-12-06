using Domain.Habit.Enums;

namespace Application.Frequency.DayOfWeekToDayOfWeekFrequencyMapper;

public interface IDayOfWeekToDayOfWeekFrequencyMapper
{
    DayOfWeekFrequency Map(DayOfWeek dayOfWeek);
}