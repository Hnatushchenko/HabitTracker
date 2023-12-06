using Domain.Habit.ValueObjects;
using Domain.HabitArchivedPeriodEntity.ValueObjects;

namespace Domain.HabitArchivedPeriodEntity;

public sealed class HabitArchivedPeriod
{
    public required HabitArchivedPeriodId Id { get; set; }
    public required DateTimeOffset StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }
    public required HabitId HabitId { get; set; }
    public Habit.Habit? Habit { get; set; }
}
