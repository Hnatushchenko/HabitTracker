using Domain.BadHabit;
using MediatR;

namespace Application.BadHabits.Delete;

public sealed record DeleteBadHabitCommand(BadHabitId BadHabitId) : IRequest;
