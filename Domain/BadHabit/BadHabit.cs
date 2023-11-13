namespace Domain.BadHabit;

public sealed class BadHabit
{
    public required BadHabitId Id { get; set; }
    public List<BadHabitOccurrence> Occurrences { get; } = new();
}