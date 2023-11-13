namespace Application.BadHabits.Get;

public sealed record BadHabitResponse
{
    public required Guid BadHabitId { get; init; }
    public required string Description { get; init; }
}