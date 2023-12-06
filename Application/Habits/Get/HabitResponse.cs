using Domain.Habit.Enums;

namespace Application.Habits.Get;

public sealed record HabitResponse
{
    public required Guid Id { get; init; }
    public required string Description { get; init; }
    public required string ToDoItemDescription { get; init; }
    public required TimeUnit FrequencyTimeUnit { get; init; }
    public required int FrequencyCount { get; init; }
    public required int Streak { get; init; }
    public required TimeOnly DefaultStartTime { get; init; }
    public required TimeOnly DefaultEndTime { get; init; }
    public required bool IsArchived { get; init; }
}
