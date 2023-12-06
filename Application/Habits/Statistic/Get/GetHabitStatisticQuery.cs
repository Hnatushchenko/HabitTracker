using Domain.Habit.ValueObjects;
using MediatR;

namespace Application.Habits.Statistic.Get;

public sealed record GetHabitStatisticQuery(HabitId HabitId) : IRequest<GetHabitStatisticResponse>;