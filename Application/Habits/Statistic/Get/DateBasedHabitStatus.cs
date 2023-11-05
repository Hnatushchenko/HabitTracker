namespace Application.Habits.Statistic.Get;

public sealed record DateBasedHabitStatus
{
    public required bool IsCompleted { get; init; }
    public required DateTimeOffset Date { get; init; }
}