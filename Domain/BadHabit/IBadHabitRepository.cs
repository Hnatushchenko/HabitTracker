namespace Domain.BadHabit;

public interface IBadHabitRepository
{
    void AddOccurrence(BadHabitId badHabitId, DateTimeOffset occurrenceDate);
    void AddBadHabit(BadHabit badHabit);
    void Remove(BadHabit badHabit);
    Task<BadHabit> GetByIdAsync(BadHabitId badHabitId, CancellationToken cancellationToken);
    Task RemoveOccurrenceAsync(BadHabitId badHabitId,
        DateTimeOffset occurrenceDate,
        CancellationToken cancellationToken);
}