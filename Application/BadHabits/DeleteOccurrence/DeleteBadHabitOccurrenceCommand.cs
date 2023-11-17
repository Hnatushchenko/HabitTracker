using Domain.BadHabit;
using MediatR;

namespace Application.BadHabits.DeleteOccurrence;

public sealed record DeleteBadHabitOccurrenceCommand : IRequest
{
    public required BadHabitId BadHabitId { get; init; }
    public required DateTimeOffset OccurrenceDate { get; init; }
}