using Domain.Habit;

namespace Domain.ToDoItem;

public interface IToDoItemRepository : IRepository<ToDoItem, ToDoItemId>
{
    /// <summary>
    /// Gets a list of ToDoItem objects that have a specified due date.
    /// </summary>
    /// <param name="dueDate">The date by which the ToDoItems are expected to be completed or delivered.</param>
    /// <returns>A list of ToDoItem objects that have the same due date, or an empty list if none are found.</returns>
    Task<List<ToDoItem>> GetByDueDateAsync(DateTimeOffset dueDate);

    Task<List<HabitToDoItem>> GetByDueDateWithIncludedHabitAsync(DateTimeOffset dueDate);
    void AddToDoItemForHabit(ToDoItem toDoItem, HabitId habitId);
}
