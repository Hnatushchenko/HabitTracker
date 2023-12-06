using Domain.Habit.ValueObjects;

namespace Domain.HabitArchivedPeriodEntity;

public interface IHabitArchivedPeriodRepository
{
    public Task<HabitArchivedPeriod> GetUnfinishedPeriodByHabitIdAsync(HabitId habitId, CancellationToken cancellationToken);
    void Add(HabitArchivedPeriod habitArchivedPeriod);
}
