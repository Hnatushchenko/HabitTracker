using Domain.Habit;

namespace Application.Frequency.DayOfWeekToDayOfWeekFrequencyMapper;

public interface IDayOfWeekToDayOfWeekFrequencyMapper
{
    DayOfWeekFrequency Map(DayOfWeek dayOfWeek);
}