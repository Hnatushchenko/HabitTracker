using Domain.Habit;

namespace Application.Frequency.DayOfWeekToDayOfWeekFrequencyMapper;

public class DayOfWeekToDayOfWeekFrequencyMapper : IDayOfWeekToDayOfWeekFrequencyMapper
{
    public DayOfWeekFrequency Map(DayOfWeek dayOfWeek)
    {
        return dayOfWeek switch
        {
            DayOfWeek.Sunday => DayOfWeekFrequency.OnSundays,
            DayOfWeek.Monday => DayOfWeekFrequency.OnMondays,
            DayOfWeek.Tuesday => DayOfWeekFrequency.OnTuesdays,
            DayOfWeek.Wednesday => DayOfWeekFrequency.OnWednesdays,
            DayOfWeek.Thursday => DayOfWeekFrequency.OnThursdays,
            DayOfWeek.Friday => DayOfWeekFrequency.OnFridays,
            DayOfWeek.Saturday => DayOfWeekFrequency.OnSaturdays,
            _ => throw new ArgumentOutOfRangeException(nameof(dayOfWeek))
        };
    }
}