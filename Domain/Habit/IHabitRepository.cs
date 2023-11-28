namespace Domain.Habit;

public interface IHabitRepository : IRepository<IHabit, HabitId>
{
    /// <summary>
    /// Gets a list of habits that should occur on a given target date.
    /// </summary>
    /// <param name="targetDate">The date to check for habits.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete. </param>
    /// <returns>A list of habits that match the target date.</returns>
    Task<List<IHabit>> GetActiveHabitsByTargetDateAsync(DateTimeOffset targetDate, CancellationToken cancellationToken);
    Task<IHabitWithToDoItems> GetHabitByIdWithToDoItemsIncludedAsync(HabitId habitId, CancellationToken cancellationToken);
    Task<List<IHabitWithToDoItems>> GetAllHabitsWithToDoItemsIncludedAsync(CancellationToken cancellationToken);
}