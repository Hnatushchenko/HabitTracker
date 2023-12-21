using Domain.BadHabit;
using MediatR;

namespace Application.BadHabits.GetStatistic.GetBadHabitStatistic;

public sealed record GetBadHabitStatisticQuery(BadHabitId BadHabitId) : IRequest<GetBadHabitStatisticResponse>;
