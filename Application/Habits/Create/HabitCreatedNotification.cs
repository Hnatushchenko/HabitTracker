using Domain.Habit;
using MediatR;

namespace Application.Habits.Create;

public class HabitCreatedNotification : INotification
{
    public required Habit Habit { get; init; }
}
