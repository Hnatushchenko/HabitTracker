namespace Domain.BadHabit;

public interface IBadHabitRepository
{
    public void AddOccurrence(BadHabitId badHabitId, DateTimeOffset occurrenceDate);
    void AddBadHabit(BadHabit badHabit);
    void Remove(BadHabit badHabit);
    Task<BadHabit?> GetById(BadHabitId badHabitId, CancellationToken cancellationToken);
}