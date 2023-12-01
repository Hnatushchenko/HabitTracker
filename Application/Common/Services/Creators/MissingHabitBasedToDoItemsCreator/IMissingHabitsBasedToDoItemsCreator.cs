namespace Application.Common.Services.Creators.MissingHabitBasedToDoItemsCreator;

public interface IMissingHabitsBasedToDoItemsCreator
{
    Task CreateMissingToDoItemsAsync(DateTimeOffset targetDate, CancellationToken cancellationToken);
}