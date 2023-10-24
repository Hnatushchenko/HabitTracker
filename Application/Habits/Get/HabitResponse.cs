using Domain.Habit;

namespace Application.Habits.Get;

public sealed record HabitResponse(Guid Id, string Description, TimeUnit FrequencyTimeUnit, int FrequencyCount);
