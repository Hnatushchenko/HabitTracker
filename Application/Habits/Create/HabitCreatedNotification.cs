using Domain.Habit;
using MediatR;

namespace Application.Habits.Create;

public sealed class HabitCreatedNotification : INotification
{
    public required Habit Habit { get; init; }
}
