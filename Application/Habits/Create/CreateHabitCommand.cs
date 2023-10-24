using Domain.Habit;
using MediatR;

namespace Application.Habits.Create;

public sealed record CreateHabitCommand(
    string Description,
    TimeUnit TimeUnit,
    int FrequencyCount) : IRequest;