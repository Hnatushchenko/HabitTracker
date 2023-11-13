namespace Domain.BadHabit;

public sealed class BadHabitOccurrence
{
    public required DateTimeOffset OccurrenceDate { get; set; }
    public required BadHabitId BadHabitId { get; set; }
    public BadHabit? BadHabit { get; set; }
}