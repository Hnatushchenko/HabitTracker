using Domain.Habit;
using Domain.OneOfTypes;
using MediatR;

namespace Application.Habits.Delete;

public sealed record DeleteHabitCommand(HabitId HabitId) : IRequest;
