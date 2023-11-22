namespace Domain.Habit;

public interface IHabitRepository : IRepository<IHabit, HabitId>
{
    Task<List<IHabit>> GetActiveHabitsByTargetDateAsync(DateTimeOffset targetDate);
    Task<IHabitWithToDoItems> GetHabitByIdWithToDoItemsIncluded(HabitId habitId, CancellationToken cancellationToken);
}