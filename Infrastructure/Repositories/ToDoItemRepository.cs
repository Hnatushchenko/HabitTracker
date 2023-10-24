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

    public void Add(ToDoItem toDoItem)
    {
        _applicationContext.ToDoItems.Add(toDoItem);
    }

    public void Remove(ToDoItem toDoItem)
    {
        _applicationContext.ToDoItems.Remove(toDoItem);
    }
}