using MediatR;

namespace Application.BadHabits.Create;

public sealed record CreateBadHabitCommand : IRequest
{
    public required DateTimeOffset StartDate { get; init; }
    public required string Description { get; init; }
}