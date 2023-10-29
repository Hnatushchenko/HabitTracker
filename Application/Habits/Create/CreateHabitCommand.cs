using Domain.Habit;
using MediatR;

namespace Application.Habits.Create;

public sealed record CreateHabitCommand(
    string Description,
    DateTimeOffset StartDate,
    TimeUnit TimeUnit,
    int FrequencyCount,
    string ToDoItemDescription) : IRequest;