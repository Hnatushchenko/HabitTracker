using Domain.Habit;
using MediatR;

namespace Application.Habits.Delete;

public sealed record DeleteHabitCommand(HabitId HabitId) : IRequest;
