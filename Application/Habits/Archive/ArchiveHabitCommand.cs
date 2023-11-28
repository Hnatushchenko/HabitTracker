using Domain.Habit;
using MediatR;

namespace Application.Habits.Archive;

public sealed record ArchiveHabitCommand(HabitId HabitId) : IRequest;