namespace Domain.BadHabit;

public sealed class BadHabit
{
    private List<BadHabitOccurrence>? _occurrences;
    public required BadHabitId Id { get; set; }
    public required string Description { get; set; }
    public required DateTimeOffset StartDate { get; set; }
    public IReadOnlyList<BadHabitOccurrence> Occurrences => _occurrences ??= new List<BadHabitOccurrence>();
}