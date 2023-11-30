namespace Application.Common.Interfaces.Creators.MissingHabitBasedToDoItemsCreator;

public interface IMissingHabitsBasedToDoItemsCreator
{
    Task CreateMissingToDoItemsAsync(DateTimeOffset targetDate, CancellationToken cancellationToken);
}