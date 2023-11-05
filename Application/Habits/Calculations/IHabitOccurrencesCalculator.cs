using Domain.Habit;

namespace Application.Habits.Calculations;

public interface IHabitOccurrencesCalculator
{
    ///<summary>
    /// Determines whether a habit should occur on a specified date, based on the habit's frequency and start date.
    /// </summary>
    /// <param name="habit">The habit entity to check.</param>
    /// <param name="date">The date to determine the habit occurrence.</param>
    /// <returns>True if the habit should occur on the specified date, false otherwise.</returns>
    bool ShouldHabitOccurOnSpecifiedDate(Habit habit, DateTimeOffset date);
}