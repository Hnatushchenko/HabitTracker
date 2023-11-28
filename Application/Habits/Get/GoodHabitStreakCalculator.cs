using Domain.Habit;
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
                .Where(toDoItem => toDoItem.DueDate.HasUtcDateLessThen(utcNow) || 
                                   (toDoItem.DueDate.HasUtcDateEqualTo(utcNow) && toDoItem.IsDone))
                .OrderByDescending(toDoItem => toDoItem.DueDate);
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