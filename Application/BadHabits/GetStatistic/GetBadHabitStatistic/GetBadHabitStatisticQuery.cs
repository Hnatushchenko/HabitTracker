using Domain.BadHabit;
using MediatR;

namespace Application.BadHabits.GetStatistic.GetBadHabitStatistic;

public sealed record GetBadHabitStatisticQuery : IRequest<GetBadHabitStatisticResponse>
{
    public required BadHabitId BadHabitId { get; init; }
}