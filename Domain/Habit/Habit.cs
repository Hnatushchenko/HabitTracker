namespace Domain.Habit;

public sealed class Habit
{
    public required HabitId Id { get; set; }
    public required string Description { get; set; }
    public required TimeUnit FrequencyTimeUnit { get; set; }
    public required FrequencyCount FrequencyCount { get; set; } 
}