using Domain.Habit;
using MediatR;

namespace Application.Habits.Update;

public sealed record UpdateHabitDetailsCommand : IRequest
{
    public required HabitId HabitId { get; init; }
    public required string Description { get; init; }
    public required string ToDoItemDescription { get; init; }
    public required TimeOnly DefaultStartTime { get; init; }
    public required TimeOnly DefaultEndTime { get; init; }
}

