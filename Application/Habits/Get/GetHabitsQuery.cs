using MediatR;

namespace Application.Habits.Get;

public sealed record GetHabitsQuery : IRequest<IEnumerable<HabitResponse>>;