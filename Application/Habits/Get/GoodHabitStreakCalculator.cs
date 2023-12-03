using Domain.Habit;
using Domain.ToDoItem;
using Helpers.Extensions;

namespace Application.Habits.Get;

public sealed class GoodHabitStreakCalculator : IGoodHabitStreakCalculator
{
    private readonly TimeProvider _timeProvider;

    public GoodHabitStreakCalculator(TimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }
    
    public int GetHabitStreak(IHabitWithToDoItems habit)
    {
        var utcNow = _timeProvider.GetUtcNow();
        var toDoItems = habit.ToDoItems
            .Where(toDoItem => toDoItem.DueDate.HasUtcDateLessThan(utcNow) || 
                               (toDoItem.DueDate.HasUtcDateEqualTo(utcNow) && toDoItem.IsDone))
            .OrderByDescending(toDoItem => toDoItem.DueDate);
        return CalculateStreak(toDoItems);
    }

    private static int CalculateStreak(IEnumerable<ToDoItem> toDoItems)
    {
        var streak = 0;
        foreach (var toDoItem in toDoItems)
        {
            if (toDoItem.IsDone)
            {
                streak++;
            }
            else
            {
                break;
            }
        }

        return streak;
    }
}
