using MediatR;

namespace Application.Habits.Unarchive;

public sealed class HabitUnarchivedNotification : INotification
{
    public required DateTimeOffset UnarchivingDate { get; init; }
}
