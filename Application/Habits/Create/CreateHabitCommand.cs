using Domain.Habit.Enums;
using MediatR;

namespace Application.Habits.Create;

public sealed record CreateHabitCommand(
    string Description,
    DateTimeOffset StartDate,
    TimeOnly DefaultStartTime,
    TimeOnly DefaultEndTime,
    TimeUnit TimeUnit,
    DayOfWeekFrequency DayOfWeekFrequency,
    int FrequencyCount,
    string ToDoItemDescription) : IRequest;