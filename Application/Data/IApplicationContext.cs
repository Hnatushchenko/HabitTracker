using Domain.BadHabit;
using Domain.Habit;
using Domain.HabitArchivedPeriodEntity;
using Domain.ToDoItem;
using Microsoft.EntityFrameworkCore;

namespace Application.Data;

public interface IApplicationContext
{
    DbSet<HabitArchivedPeriod> HabitArchivedPeriods { get; }
    DbSet<ToDoItem> ToDoItems { get; }
    DbSet<Habit> Habits { get; }
    DbSet<BadHabit> BadHabits { get; }
    DbSet<BadHabitOccurrence> BadHabitOccurrences { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}