namespace Application.Common.Services.Creators.MissingHabitBasedToDoItemsCreator;

public class OptimizedMissingHabitBasedToDoItemCreator : IMissingHabitsBasedToDoItemsCreator
{
    private readonly IMissingHabitsBasedToDoItemsCreator _missingHabitsBasedToDoItemsCreator;
    private readonly IHabitBasedMissingToDoItemCreatorCache _cache;

    public OptimizedMissingHabitBasedToDoItemCreator(IMissingHabitsBasedToDoItemsCreator missingHabitsBasedToDoItemsCreator,
        IHabitBasedMissingToDoItemCreatorCache cache)
    {
        _missingHabitsBasedToDoItemsCreator = missingHabitsBasedToDoItemsCreator;
        _cache = cache;
    }
    
    public async Task CreateMissingToDoItemsAsync(DateTimeOffset targetDate, CancellationToken cancellationToken)
    {
        if (_cache.TryAdd(targetDate))
        {
            await _missingHabitsBasedToDoItemsCreator.CreateMissingToDoItemsAsync(targetDate, cancellationToken);
        }
    }
}