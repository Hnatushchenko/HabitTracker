namespace Domain.Habit;

public interface IHabitRepository : IRepository<Habit, HabitId>
{
    Task<List<Habit>> GetActiveHabitsByTargetDateAsync(DateTimeOffset targetDate);
}