using Domain.Habit;

namespace Application.Habits.Get;

public interface IGoodHabitStreakCalculator
{
    int GetHabitStreak(IHabitWithToDoItems habit);
}