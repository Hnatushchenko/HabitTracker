using Domain;
using Domain.Habit.Errors;
using Domain.Habit.ValueObjects;
using MediatR;

namespace Application.Habits.Unarchive;

public sealed record UnarchiveHabitCommand(HabitId HabitId) : IRequest<SuccessOr<HabitIsNotArchivedError>>;
