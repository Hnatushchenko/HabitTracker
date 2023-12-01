using System.Collections.Concurrent;
using Application.Habits.Create;
using Helpers.Extensions;
using MediatR;

namespace Application.Common.Services.Creators.MissingHabitBasedToDoItemsCreator;

public sealed class HabitBasedMissingToDoItemCreatorCache : IHabitBasedMissingToDoItemCreatorCache, INotificationHandler<HabitCreatedNotification>
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
        Cache.RemoveAll(pair => pair.Key >= startDate);
        return Task.CompletedTask;
    }
}