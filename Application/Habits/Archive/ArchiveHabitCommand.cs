using Domain.Habit;
using Domain.OneOfTypes;
using MediatR;

namespace Application.Habits.Archive;

public sealed record ArchiveHabitCommand(HabitId HabitId) : IRequest;