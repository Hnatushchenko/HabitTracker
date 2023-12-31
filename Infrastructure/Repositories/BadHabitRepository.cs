﻿using Application.Data;
using Domain.BadHabit;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class BadHabitRepository : IBadHabitRepository
{
    private readonly IApplicationContext _applicationContext;

    public BadHabitRepository(IApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    
    public void AddOccurrence(BadHabitId badHabitId, DateTimeOffset occurrenceDate)
    {
        var badHabitOccurrence = new BadHabitOccurrence
        {
            OccurrenceDate = occurrenceDate,
            BadHabitId = badHabitId
        };
        _applicationContext.BadHabitOccurrences.Add(badHabitOccurrence);
    }
    
    public void AddBadHabit(BadHabit badHabit)
    {
        _applicationContext.BadHabits.Add(badHabit);
    }

    public void Remove(BadHabit badHabit)
    {
        _applicationContext.BadHabits.Remove(badHabit);
    }

    public async Task<BadHabit> GetByIdAsync(BadHabitId badHabitId, CancellationToken cancellationToken)
    {
        var badHabit = await _applicationContext.BadHabits.FindAsync(badHabitId, cancellationToken);
        if (badHabit is null)
        {
            throw new BadHabitNotFoundException
            {
                ModelId = badHabitId.Value
            };
        }

        return badHabit;
    }

    public async Task RemoveOccurrenceAsync(BadHabitId badHabitId,
        DateTimeOffset occurrenceDate,
        CancellationToken cancellationToken)
    {
        await _applicationContext.BadHabitOccurrences.Where(badHabitOccurrence =>
                badHabitOccurrence.BadHabitId == badHabitId && badHabitOccurrence.OccurrenceDate == occurrenceDate)
            .ExecuteDeleteAsync(cancellationToken);
    }
}