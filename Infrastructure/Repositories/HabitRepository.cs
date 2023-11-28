using Application.Data;
using Application.Habits.Calculations;
using Domain.Habit;
using Domain.Habit.Exceptions;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

namespace Infrastructure.Repositories;

public sealed class HabitRepository : IHabitRepository
{
    private readonly IHabitOccurrencesCalculator _habitOccurrencesCalculator;
    private readonly IApplicationContext _applicationContext;

    public HabitRepository(IHabitOccurrencesCalculator habitOccurrencesCalculator,
        IApplicationContext applicationContext)
    {
        _habitOccurrencesCalculator = habitOccurrencesCalculator;
        _applicationContext = applicationContext;
    }
    public async Task<List<IHabit>> GetAllAsync(CancellationToken cancellationToken)
    {
        var habits = await _applicationContext.Habits.Cast<IHabit>()
            .ToListAsync(cancellationToken: cancellationToken);
        return habits;
    }

    public async Task<OneOf<IHabit, NotFound>> GetByIdDeprecatedAsync(HabitId toDoItemId)
    {
        var habit = await _applicationContext.Habits.FindAsync(toDoItemId);
        return habit is null ? new NotFound() : habit;
    }

    public async Task<IHabitWithToDoItems> GetHabitByIdWithToDoItemsIncludedAsync(HabitId habitId, CancellationToken cancellationToken)
    {
        var habit = await _applicationContext.Habits
            .Include(habit => habit.ToDoItems)
            .FirstOrDefaultAsync(habit => habit.Id == habitId, cancellationToken);
        if (habit is null)
        {
            throw new HabitNotFoundException
            {
                ModelId = habitId.Value
            };
        }

        return habit;
    }

    public async Task<IHabit> GetByIdAsync(HabitId habitId, CancellationToken cancellationToken)
    {
        var habit = await _applicationContext.Habits.FindAsync(habitId, cancellationToken);
        if (habit is null)
        {
            throw new HabitNotFoundException
            {
                ModelId = habitId.Value
            };
        }

        return habit;
    }

    public void Add(IHabit iHabit)
    {
        if (iHabit is Habit habit)
        {
            Add(habit);
        }
    }

    public void Remove(IHabit entity)
    {
        if (entity is Habit habit)
        {
            Remove(habit);
        }
    }

    /// <inheritdoc />
    public async Task<List<IHabit>> GetActiveHabitsByTargetDateAsync(DateTimeOffset targetDate)
    {
        var activeHabits = await _applicationContext.Habits
            .Where(habit => !habit.IsArchived)
            .ToListAsync();
        var filteredHabits = activeHabits
            .Where(habit => _habitOccurrencesCalculator.ShouldHabitOccurOnSpecifiedDate(habit, targetDate))
            .Cast<IHabit>()
            .ToList();
        return filteredHabits;
    }

    private void Add(Habit habit)
    {
        _applicationContext.Habits.Add(habit);
    }

    private void Remove(Habit habit)
    {
        _applicationContext.Habits.Remove(habit);
    }
}