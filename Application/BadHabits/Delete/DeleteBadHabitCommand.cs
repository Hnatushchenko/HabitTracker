using Domain.BadHabit;
using MediatR;

namespace Application.BadHabits.Delete;

public sealed record DeleteBadHabitCommand : IRequest
{
    public required BadHabitId BadHabitId { get; init; }
}