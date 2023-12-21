namespace Application.Habits.Get;

public sealed record HabitsResponse(IEnumerable<HabitResponse> Habits);
