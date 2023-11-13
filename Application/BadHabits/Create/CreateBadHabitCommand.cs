using MediatR;

namespace Application.BadHabits.Create;

public sealed record CreateBadHabitCommand : IRequest
{
    public required string Description { get; init; }
}