using Application.Data;
using Domain.BadHabit;
using Domain.Habit;
using Domain.ToDoItem;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public sealed class ApplicationContext : DbContext, IApplicationContext
{
    public DbSet<ToDoItem> ToDoItems => Set<ToDoItem>();
    public DbSet<Habit> Habits => Set<Habit>();
    public DbSet<BadHabit> BadHabits => Set<BadHabit>();
    public DbSet<BadHabitOccurrence> BadHabitOccurrences => Set<BadHabitOccurrence>();
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
    }
}