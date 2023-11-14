using Application.Data;
using Domain.BadHabit;

namespace Infrastructure.Repositories;

public class BadHabitRepository : IBadHabitRepository
{
    private readonly IApplicationContext _applicationContext;

    public BadHabitRepository(IApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    
    public void AddOccurrence(BadHabitId badHabitId, DateTimeOffset occurrenceDate)
    {
        var badHabitOccurence = new BadHabitOccurrence()
        {
            OccurrenceDate = occurrenceDate,
            BadHabitId = badHabitId
        };
        _applicationContext.BadHabitOccurrences.Add(badHabitOccurence);
    }

    public void AddBadHabit(BadHabit badHabit)
    {
        _applicationContext.BadHabits.Add(badHabit);
    }

    public void Remove(BadHabit badHabit)
    {
        _applicationContext.BadHabits.Remove(badHabit);
    }

    public async Task<BadHabit?> GetById(BadHabitId badHabitId, CancellationToken cancellationToken)
    {
        return await _applicationContext.BadHabits.FindAsync(badHabitId, cancellationToken);
    }
}