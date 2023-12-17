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
    private static readonly ConcurrentDictionary<DateOnly, bool> Cache = new();
    
    public bool TryAdd(DateTimeOffset date)
    {
        var dateOnly = date.ToDateOnly();
        var isAdded = Cache.TryAdd(dateOnly, true);
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

    private static void ClearCacheStartingFromDate(DateOnly dateOnly)
    {
        Cache.RemoveAll(pair => pair.Key >= dateOnly);
    }
}