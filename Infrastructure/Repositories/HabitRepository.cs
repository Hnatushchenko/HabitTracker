using Application.Data;
using Application.Habits.Calculations;
using Domain.Habit;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

namespace Infrastructure.Repositories;

public class HabitRepository : IHabitRepository
{
    private readonly IHabitOccurrencesCalculator _habitOccurrencesCalculator;
    private readonly IApplicationContext _applicationContext;

    public HabitRepository(IHabitOccurrencesCalculator habitOccurrencesCalculator,
        IApplicationContext applicationContext)
    {
        _habitOccurrencesCalculator = habitOccurrencesCalculator;
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
        return habit is null ? new NotFound() : habit;
    }

    /// <summary>
    /// Gets a list of habits that should occur on a given target date.
    /// </summary>
    /// <param name="targetDate">The date to check for habits.</param>
    /// <returns>A list of habits that match the target date.</returns>
    public async Task<List<Habit>> GetActiveHabitsByTargetDateAsync(DateTimeOffset targetDate)
    {
        var activeHabits = await _applicationContext.Habits
            .Where(habit => !habit.IsArchived)
            .ToListAsync();
        var filteredHabits = activeHabits
            .Where(habit => _habitOccurrencesCalculator.ShouldHabitOccurOnSpecifiedDate(habit, targetDate))
            .ToList();
        return filteredHabits;
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