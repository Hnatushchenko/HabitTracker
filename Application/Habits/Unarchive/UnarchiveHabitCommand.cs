using Domain.Habit;
using MediatR;

namespace Application.Habits.Unarchive;

public sealed record UnarchiveHabitCommand(HabitId HabitId) : IRequest;
