using Domain.Habit;
using Domain.ToDoItem;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

namespace Infrastructure.Repositories;

public class ToDoItemRepository : IToDoItemRepository
{
    private readonly IApplicationContext _applicationContext;

    public ToDoItemRepository(IApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    
    public async Task<List<ToDoItem>> GetAllAsync()
    {
        var toDoItems = await _applicationContext.ToDoItems.ToListAsync();
        return toDoItems;
    }

    public async Task<OneOf<ToDoItem, NotFound>> GetByIdAsync(ToDoItemId toDoItemId)
    {
        var toDoItem = await _applicationContext.ToDoItems.FindAsync(toDoItemId);
        if (toDoItem is null)
        {
            return new NotFound();
        }

        return toDoItem;
    }

    /// <inheritdoc/>
    public async Task<List<ToDoItem>> GetByDueDateAsync(DateTimeOffset dueDate)
    {
        var toDoItems = (await _applicationContext.ToDoItems
            .ToListAsync())
            .Where(toDoItem => toDoItem.DueDate.UtcDateTime.Date == dueDate.Date).ToList();
        return toDoItems;
    }

    public async Task<List<HabitToDoItem>> GetByDueDateWithIncludedHabitAsync(DateTimeOffset dueDate)
    {
        var toDoItems = (await _applicationContext.HabitToDoItems
            .Include(habitToDoItem => habitToDoItem.Habit)
            .Include(habitToDoItem => habitToDoItem.ToDoItem)
            .ToListAsync())
            .Where(habitToDoItem => habitToDoItem.ToDoItem!.DueDate.UtcDateTime.Date == dueDate.Date).ToList();
        return toDoItems;
    }

    public void Add(ToDoItem toDoItem)
    {
        _applicationContext.ToDoItems.Add(toDoItem);
    }
    
    public void AddToDoItemForHabit(ToDoItem toDoItem, HabitId habitId)
    {
        Add(toDoItem);
        var habitToDoItem = new HabitToDoItem()
        {
            HabitId = habitId,
            ToDoItemId = toDoItem.Id
        };
        _applicationContext.HabitToDoItems.Add(habitToDoItem);
    }

    public void Remove(ToDoItem toDoItem)
    {
        _applicationContext.ToDoItems.Remove(toDoItem);
    }
}