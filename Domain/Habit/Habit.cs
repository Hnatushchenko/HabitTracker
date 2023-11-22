using System.Reflection.Emit;

namespace Domain.Habit;

public sealed class Habit : IHabitWithToDoItems
{
    public required HabitId Id { get; set; }
    public required string Description { get; set; }
    public required string ToDoItemDescription { get; set; }
    public required TimeOnly DefaultStartTime { get; set; }
    public required TimeOnly DefaultEndTime { get; set; }
    public required TimeUnit FrequencyTimeUnit { get; set; }
    public DayOfWeekFrequency DayOfWeekFrequency { get; set; }
    public required FrequencyCount FrequencyCount { get; set; } 
    public required DateTimeOffset StartDate { get; set; }
    public bool IsArchived { get; set; }
    public List<ToDoItem.ToDoItem> ToDoItems { get; } = new();
}