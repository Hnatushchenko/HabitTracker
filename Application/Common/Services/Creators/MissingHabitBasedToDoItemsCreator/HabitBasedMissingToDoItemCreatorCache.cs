using System.Collections.Concurrent;
using Application.Common.Interfaces.Creators.MissingHabitBasedToDoItemsCreator;
using Application.Habits.Create;
using Helpers.Extensions;
using MediatR;

namespace Application.Common.Services.Creators.MissingHabitBasedToDoItemsCreator;

public sealed class HabitBasedMissingToDoItemCreatorCache : IHabitBasedMissingToDoItemCreatorCache, INotificationHandler<HabitCreatedNotification>
{
    private readonly ConcurrentDictionary<DateOnly, bool> _cache = new();
    
    public bool TryAdd(DateTimeOffset date)
    {
        var dateOnly = date.ToDateOnly();
        var isAdded = _cache.TryAdd(dateOnly, true);
        return isAdded;
    }

    public Task Handle(HabitCreatedNotification notification, CancellationToken cancellationToken)
    {
        var startDate = notification.Habit.StartDate.ToDateOnly();
        _cache.RemoveAll(pair => pair.Key >= startDate);
        return Task.CompletedTask;
    }
}