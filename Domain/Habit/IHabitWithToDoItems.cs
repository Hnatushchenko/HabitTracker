namespace Domain.Habit;

public interface IHabitWithToDoItems : IHabit
{
    List<ToDoItem.ToDoItem> ToDoItems { get; }
}