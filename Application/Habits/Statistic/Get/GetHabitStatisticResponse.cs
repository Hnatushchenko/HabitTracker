namespace Application.Habits.Statistic.Get;

public sealed record GetHabitStatisticResponse(IReadOnlyCollection<DateBasedHabitStatus> DateBasedHabitStatuses);