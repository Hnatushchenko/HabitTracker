using Domain.Habit;

namespace Domain.ToDoItem;

public sealed class ToDoItem
{
    public required ToDoItemId Id { get; set; }
    public required TimeOnly StartTime { get; set; }
    public required TimeOnly EndTime { get; set; }
    public required string Description { get; set; }
    public required DateTimeOffset DueDate { get; set; }
    public bool IsHiddenOnDueDate { get; set; }
    public bool IsDone { get; set; }
    public HabitId? HabitId { get; set; }
    public Habit.Habit? Habit { get; set; }
}