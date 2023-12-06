using Application.Data;
using Domain.Habit.ValueObjects;
using Domain.HabitArchivedPeriodEntity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal sealed class HabitArchivedPeriodRepository : IHabitArchivedPeriodRepository
{
    private readonly IApplicationContext _applicationContext;

    public HabitArchivedPeriodRepository(IApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    
    public async Task<HabitArchivedPeriod> GetUnfinishedPeriodByHabitIdAsync(HabitId habitId, CancellationToken cancellationToken)
    {
        var habitArchivedPeriod = await _applicationContext.HabitArchivedPeriods
            .Where(period => period.HabitId == habitId && period.EndDate == null)
            .SingleAsync(cancellationToken);
        return habitArchivedPeriod;
    }

    public void Add(HabitArchivedPeriod habitArchivedPeriod)
    {
        _applicationContext.HabitArchivedPeriods.Add(habitArchivedPeriod);
    }
}
