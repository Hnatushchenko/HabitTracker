﻿namespace Domain.Habit;

public interface IHabitRepository : IRepository<IHabit, HabitId>
{
    /// <summary>
    /// Gets a list of habits that should occur on a given target date.
    /// </summary>
    /// <param name="targetDate">The date to check for habits.</param>
    /// <returns>A list of habits that match the target date.</returns>
    Task<List<IHabit>> GetActiveHabitsByTargetDateAsync(DateTimeOffset targetDate);
    Task<IHabitWithToDoItems> GetHabitByIdWithToDoItemsIncluded(HabitId habitId, CancellationToken cancellationToken);
}