using Application.Data;
using Application.Extensions;
using Domain.Habit;
using Domain.ToDoItem;
using Domain.ToDoItem.Exceptions;
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

    public async Task<OneOf<ToDoItem, NotFound>> GetByIdDeprecatedAsync(ToDoItemId toDoItemId)
    {
        var toDoItem = await _applicationContext.ToDoItems.FindAsync(toDoItemId);
        return toDoItem is null ? new NotFound() : toDoItem;
    }
    
    public async Task<ToDoItem> GetByIdAsync(ToDoItemId toDoItemId, CancellationToken cancellationToken)
    {
        var toDoItem = await _applicationContext.ToDoItems.FindAsync(toDoItemId, cancellationToken);
        if (toDoItem is null)
        {
            throw new ToDoItemNotFoundException
            {
                ModelId = toDoItemId.Value
            };
        }

        return toDoItem;
    }
    
    /// <inheritdoc/>
    public async Task<List<ToDoItem>> GetByDueDateAndNotHiddenAsync(DateTimeOffset dueDate)
    {
        var utcNow = DateTimeOffset.UtcNow;
        var toDoItems = (await _applicationContext.ToDoItems
            .ToListAsync())
            .Where(toDoItem => toDoItem.DueDate.HasUtcDateEqualTo(dueDate) &&
                               !(toDoItem.IsHiddenOnDueDate &&
                                 toDoItem.DueDate.HasUtcDateEqualTo(utcNow)))
            .ToList();
        return toDoItems;
    }
    
    public async Task<List<ToDoItem>> GetByDueDateWithIncludedHabitAsync(DateTimeOffset dueDate, CancellationToken cancellationToken)
    {
        var allToDoItems = await _applicationContext.ToDoItems
            .Include(toDoItem => toDoItem.Habit)
            .Where(toDoItem => toDoItem.HabitId != null)
            .ToListAsync(cancellationToken);
        var filteredToDoItems = allToDoItems
            .Where(toDoItem => toDoItem.DueDate.HasUtcDateEqualTo(dueDate))
            .ToList();
        return filteredToDoItems;
    }

    public void Add(ToDoItem toDoItem)
    {
        _applicationContext.ToDoItems.Add(toDoItem);
    }
    
    public async Task RemoveToDoItemsByTheirHabitAsync(HabitId habitId, CancellationToken cancellationToken)
    {
        await _applicationContext.ToDoItems
            .Where(toDoItem => toDoItem.HabitId == habitId)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public void Remove(ToDoItem toDoItem)
    {
        _applicationContext.ToDoItems.Remove(toDoItem);
    }
}