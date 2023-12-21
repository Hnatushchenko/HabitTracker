namespace Application.BadHabits.Get;

public sealed record BadHabitsResponse(IEnumerable<BadHabitResponse> BadHabits);
