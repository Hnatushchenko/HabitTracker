namespace Domain.Habit;

public interface IHabit
{
    HabitId Id { get; set; }
    string Description { get; set; }
    string ToDoItemDescription { get; set; }
    TimeOnly DefaultStartTime { get; set; }
    TimeOnly DefaultEndTime { get; set; }
    TimeUnit FrequencyTimeUnit { get; set; }
    DayOfWeekFrequency DayOfWeekFrequency { get; set; }
    FrequencyCount FrequencyCount { get; set; } 
    DateTimeOffset StartDate { get; set; }
    bool IsArchived { get; set; }
}