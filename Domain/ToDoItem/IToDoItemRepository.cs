using Domain.Habit;

namespace Domain.ToDoItem;

public interface IToDoItemRepository : IRepository<ToDoItem, ToDoItemId>
{
    /// <summary>
    /// Gets a list of ToDoItem objects that have a specified due date.
    /// </summary>
    /// <param name="dueDate">The date by which the ToDoItems are expected to be completed or delivered.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>A list of ToDoItem objects that have the same due date, or an empty list if none are found.</returns>
    Task<List<ToDoItem>> GetByDueDateAndNotHiddenAsync(DateTimeOffset dueDate, CancellationToken cancellationToken);
    Task<List<ToDoItem>> GetByDueDateWithIncludedHabitAsync(DateTimeOffset dueDate, CancellationToken cancellationToken);
    Task RemoveToDoItemsByTheirHabitAsync(HabitId habitId, CancellationToken cancellationToken);
    Task<List<ToDoItem>> GetChildrenByParentToDoItemIdAsync(ToDoItemId parentToDoItemId, CancellationToken cancellationToken);
    void RemoveRange(IEnumerable<ToDoItem> toDoItems);
    Task<List<ToDoItem>> GetUncompletedToDoItemsWhereDueDateIsLessThenAsync(DateTimeOffset dateTimeOffset, CancellationToken cancellationToken);
    Task<ToDoItem?> GetByIdOrDefaultAsync(ToDoItemId id, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(ToDoItemId id, CancellationToken cancellationToken);
}
