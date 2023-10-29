using Domain.Habit;
using Domain.ToDoItem;
using Microsoft.EntityFrameworkCore;

namespace Application.Data;

public interface IApplicationContext
{
    DbSet<ToDoItem> ToDoItems { get; }
    DbSet<Habit> Habits { get; }
    DbSet<HabitToDoItem> HabitToDoItems { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}