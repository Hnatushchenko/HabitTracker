namespace Application.BadHabits.GetStatistic.GetBadHabitStatistic;

public sealed record GetBadHabitStatisticResponse
{
    public required DateTimeOffset StartDate { get; init; }
    public required List<DateTimeOffset> Occurrences { get; init; }
}