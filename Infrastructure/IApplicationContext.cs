using Domain.Habit;
using Domain.ToDoItem;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public interface IApplicationContext
{
    DbSet<ToDoItem> ToDoItems { get; }
    DbSet<Habit> Habits { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}