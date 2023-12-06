using Domain;
using Domain.Habit.Errors;
using Domain.Habit.ValueObjects;
using MediatR;

namespace Application.Habits.Archive;

public sealed record ArchiveHabitCommand(HabitId HabitId) : IRequest<SuccessOr<HabitIsAlreadyArchivedError>>;
