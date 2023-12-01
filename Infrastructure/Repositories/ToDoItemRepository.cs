using Application.Data;
using Domain.Habit;
using Domain.ToDoItem;
using Domain.ToDoItem.Exceptions;
using Helpers.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class ToDoItemRepository : IToDoItemRepository
{
    private readonly IApplicationContext _applicationContext;
    private readonly TimeProvider _timeProvider;

    public ToDoItemRepository(IApplicationContext applicationContext,
        TimeProvider timeProvider)
    {
        _applicationContext = applicationContext;
        _timeProvider = timeProvider;
    }
    
    public async Task<List<ToDoItem>> GetAllAsync(CancellationToken cancellationToken)
    {
        var toDoItems = await _applicationContext.ToDoItems
            .ToListAsync(cancellationToken: cancellationToken);
        return toDoItems;
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
    public async Task<List<ToDoItem>> GetByDueDateAndNotHiddenAsync(DateTimeOffset dueDate, CancellationToken cancellationToken)
    {
        var utcNow = _timeProvider.GetUtcNow();
        var toDoItems = (await _applicationContext.ToDoItems
            .ToListAsync(cancellationToken))
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
    
    public async Task<List<ToDoItem>> GetChildrenByParentToDoItemIdAsync(ToDoItemId parentToDoItemId, CancellationToken cancellationToken)
    {
        var children = await _applicationContext.ToDoItems
            .Where(toDoItem => toDoItem.ParentId == parentToDoItemId)
            .ToListAsync(cancellationToken);
        return children;
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
    
    public void RemoveRange(IEnumerable<ToDoItem> toDoItems)
    {
        _applicationContext.ToDoItems.RemoveRange(toDoItems);
    }
    
    public async Task<List<ToDoItem>> GetUncompletedToDoItemsWhereDueDateIsLessThenAsync(DateTimeOffset dateTimeOffset, CancellationToken cancellationToken)
    {
        var toDoItems = await _applicationContext.ToDoItems
            .Where(toDoItem => toDoItem.DueDate < dateTimeOffset && !toDoItem.IsDone)
            .ToListAsync(cancellationToken);
        return toDoItems;
    }
}