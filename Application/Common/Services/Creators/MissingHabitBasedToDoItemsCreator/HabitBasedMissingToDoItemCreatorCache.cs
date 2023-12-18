using System.Collections.Concurrent;
using Application.Habits.Create;
using Application.Habits.Unarchive;
using Helpers.Extensions;
using MediatR;

namespace Application.Common.Services.Creators.MissingHabitBasedToDoItemsCreator;

public sealed class HabitBasedMissingToDoItemCreatorCache : IHabitBasedMissingToDoItemCreatorCache,
    INotificationHandler<HabitCreatedNotification>,
    INotificationHandler<HabitUnarchivedNotification>
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
        ClearCacheStartingFromDate(startDate);
        return Task.CompletedTask;
    }

    public Task Handle(HabitUnarchivedNotification notification, CancellationToken cancellationToken)
    {
        var unarchivingDate = notification.UnarchivingDate.ToDateOnly();
        ClearCacheStartingFromDate(unarchivingDate);
        return Task.CompletedTask;
    }

    private void ClearCacheStartingFromDate(DateOnly dateOnly)
    {
        _cache.RemoveAll(pair => pair.Key >= dateOnly);
    }
}