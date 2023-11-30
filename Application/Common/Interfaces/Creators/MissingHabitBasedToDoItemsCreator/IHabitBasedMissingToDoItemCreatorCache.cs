namespace Application.Common.Interfaces.Creators.MissingHabitBasedToDoItemsCreator;

public interface IHabitBasedMissingToDoItemCreatorCache
{
    bool TryAdd(DateTimeOffset dateTimeOffset);
}