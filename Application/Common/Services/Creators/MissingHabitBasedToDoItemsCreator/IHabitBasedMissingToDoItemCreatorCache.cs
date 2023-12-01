namespace Application.Common.Services.Creators.MissingHabitBasedToDoItemsCreator;

public interface IHabitBasedMissingToDoItemCreatorCache
{
    bool TryAdd(DateTimeOffset dateTimeOffset);
}