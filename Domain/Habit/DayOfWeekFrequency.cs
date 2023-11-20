namespace Domain.Habit;

[Flags]
public enum DayOfWeekFrequency
{
    None = 0,
    
    OnSundays = 1 << 0,
    OnMondays = 1 << 1,
    OnTuesdays = 1 << 2,
    OnWednesdays = 1 << 3,
    OnThursdays = 1 << 4,
    OnFridays = 1 << 5,
    OnSaturdays = 1 << 6,
    
    All = ~(~0 << 7)
}