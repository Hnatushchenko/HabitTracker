using Domain.ToDoItem;

namespace Domain.Habit;

public class HabitToDoItem
{
    public required HabitId HabitId { get; set; }
    public required ToDoItemId ToDoItemId { get; set; }
    
    public Habit? Habit { get; set; }
    public ToDoItem.ToDoItem? ToDoItem { get; set; }
}