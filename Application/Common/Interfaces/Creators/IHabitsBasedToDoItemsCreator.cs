namespace Application.Common.Interfaces.Creators;

public interface IHabitsBasedToDoItemsCreator
{
    Task EnsureHabitsBasedToDoItemsCreatedAsync(DateTimeOffset targetDate, CancellationToken cancellationToken);
}