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

    /// <summary>
    /// Gets a list of habits that should occur on a given target date.
    /// </summary>
    /// <param name="targetDate">The date to check for habits.</param>
    /// <returns>A list of habits that match the target date.</returns>
    public async Task<List<Habit>> GetByTargetDateAsync(DateTimeOffset targetDate)
    {
        var shouldHabitOccur = (Habit habit, DateTimeOffset date) =>
        {
            var startDate = habit.StartDate;
            if (startDate.UtcDateTime.Date == date.UtcDateTime.Date)
            {
                return true;
            }
            else
            {
                var runner = habit.StartDate;
                while (runner.UtcDateTime.Date < targetDate.UtcDateTime.Date)
                {
                    var nextDate = habit.FrequencyTimeUnit switch
                    {
                        TimeUnit.Day => runner.AddDays(habit.FrequencyCount.Value),
                        TimeUnit.Week => runner.AddDays(habit.FrequencyCount.Value * 7),
                        TimeUnit.Month => runner.AddMonths(habit.FrequencyCount.Value),
                        TimeUnit.Year => runner.AddYears(habit.FrequencyCount.Value),
                        _ => throw new ArgumentOutOfRangeException(nameof(targetDate))
                    };
                    if (nextDate.UtcDateTime.Date == targetDate.UtcDateTime.Date)
                    {
                        return true;
                    }
                }

                return false;
            }
        };
        var allHabits = await _applicationContext.Habits.ToListAsync();
        var filteredHabits = allHabits.Where(habit => shouldHabitOccur(habit, targetDate)).ToList();
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