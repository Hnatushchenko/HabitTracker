using Domain.Habit;
using MediatR;

namespace Application.Habits.Update;

public sealed record UpdateHabitDetailsRequest(
    string Description,
    string ToDoItemDescription,
    TimeOnly DefaultStartTime,
    TimeOnly DefaultEndTime) : IRequest;

