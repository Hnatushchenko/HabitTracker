using Domain.Habit;

namespace Application.Habits.Statistic.Get;

public sealed record GetHabitStatisticResponse
{
    public required IReadOnlyCollection<DateBasedHabitStatus> DateBasedHabitStatuses { get; init; }
}