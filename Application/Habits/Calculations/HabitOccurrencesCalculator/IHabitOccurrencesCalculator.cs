using Domain.Habit;

namespace Application.Habits.Calculations.HabitOccurrencesCalculator;

public interface IHabitOccurrencesCalculator
{
    ///<summary>
    /// Determines whether a habit should occur on a specified date, based on the habit's frequency and start date.
    /// </summary>
    /// <param name="habit">The habit entity to check.</param>
    /// <param name="targetDate">The date to determine the habit occurrence.</param>
    /// <returns>True if the habit should occur on the specified date, false otherwise.</returns>
    bool ShouldHabitOccurOnSpecifiedDate(Habit habit, DateTimeOffset targetDate);

    IEnumerable<DateTimeOffset> GetHabitOccurrences(Habit habit, DateTimeOffset upToDateInclusive);
}