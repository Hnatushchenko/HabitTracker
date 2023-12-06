using Domain.Habit.Enums;
using Domain.Habit.ValueObjects;
using Domain.HabitArchivedPeriodEntity;

namespace Domain.Habit;

public sealed class Habit : IHabitWithToDoItems
{
    private List<HabitArchivedPeriod>? _habitArchivedPeriods;
    private List<ToDoItem.ToDoItem>? _toDoItems;
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
    public IEnumerable<HabitArchivedPeriod> HabitArchivedPeriods => _habitArchivedPeriods ??= new List<HabitArchivedPeriod>();
    public IEnumerable<ToDoItem.ToDoItem> ToDoItems => _toDoItems ??= new List<ToDoItem.ToDoItem>();
}