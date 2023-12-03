using Domain;
using Domain.ToDoItem;

namespace Application.Common.Services.Updaters;

public sealed class OverdueTasksDueDateUpdater : IOverdueTasksDueDateUpdater
{
    private readonly IToDoItemRepository _toDoItemRepository;
    private readonly TimeProvider _timeProvider;
    private readonly IUnitOfWork _unitOfWork;

    public OverdueTasksDueDateUpdater(IToDoItemRepository toDoItemRepository,
        TimeProvider timeProvider, IUnitOfWork unitOfWork)
    {
        _toDoItemRepository = toDoItemRepository;
        _timeProvider = timeProvider;
        _unitOfWork = unitOfWork;
    }
    
    public async Task SetDueDateForTodayForOverdueTasks(CancellationToken cancellationToken)
    {
        var utcNow = _timeProvider.GetUtcNow();
        var utcNowWithoutTime = new DateTimeOffset(utcNow.Date);
        var overdueToDoItems = await _toDoItemRepository.GetUncompletedToDoItemsThatAreNotBasedOnHabitsWhereDueDateIsLessThenAsync(utcNowWithoutTime, cancellationToken);
        foreach (var overdueToDoItem in overdueToDoItems)
        {
            overdueToDoItem.DueDate = utcNowWithoutTime;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
