using Domain.Habit;

namespace Application.Common.Services.Creators.InitialHabitToDoItemsCreator;

public interface IInitialHabitToDoItemsCreator
{
    Task CreateInitialToDoItemsAsync(Habit habit, CancellationToken cancellationToken);
}