using Domain.Habit;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

namespace Infrastructure.Repositories;

public class HabitRepository : IHabitRepository
{
    private readonly IApplicationContext _applicationContext;

    public HabitRepository(IApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    public async Task<List<Habit>> GetAllAsync()
    {
        var habits = await _applicationContext.Habits.ToListAsync();
        return habits;
    }

    public async Task<OneOf<Habit, NotFound>> GetByIdAsync(HabitId toDoItemId)
    {
        var habit = await _applicationContext.Habits.FindAsync(toDoItemId);
        if (habit is null)
        {
            return new NotFound();
        }

        return habit;
    }

    public void Add(Habit habit)
    {
        _applicationContext.Habits.Add(habit);
    }

    public void Remove(Habit habit)
    {
        _applicationContext.Habits.Remove(habit);
    }
}