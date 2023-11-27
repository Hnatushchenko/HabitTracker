namespace Domain.Habit;

public interface IHabitWithToDoItems : IHabit
{
    IEnumerable<ToDoItem.ToDoItem> ToDoItems { get; }
}