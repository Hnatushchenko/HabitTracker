using Domain;
using Domain.Habit;
using Domain.ToDoItem;

namespace Application.Common.Services.Creators.MissingHabitBasedToDoItemsCreator;

public sealed class MissingHabitBasedToDoItemsCreator : IMissingHabitsBasedToDoItemsCreator
{
    private readonly IToDoItemRepository _toDoItemRepository;
    private readonly IHabitRepository _habitRepository;
    private readonly IUnitOfWork _unitOfWork;

    public MissingHabitBasedToDoItemsCreator(IToDoItemRepository toDoItemRepository,
        IHabitRepository habitRepository,
        IUnitOfWork unitOfWork)
    {
        _habitRepository = habitRepository;
        _toDoItemRepository = toDoItemRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task CreateMissingToDoItemsAsync(DateTimeOffset targetDate, CancellationToken cancellationToken)
    {
        var activeHabits = await _habitRepository.GetActiveHabitsByTargetDateAsync(targetDate, cancellationToken);
        var toDoItems = await _toDoItemRepository.GetByDueDateWithIncludedHabitAsync(targetDate, cancellationToken);
        foreach (var habit in activeHabits)
        {
            if (toDoItems.TrueForAll(toDoItem => toDoItem.HabitId != habit.Id))
            {
                var toDoItem = new ToDoItem
                {
                    Id = new ToDoItemId(Guid.NewGuid()),
                    Description = habit.ToDoItemDescription,
                    DueDate = targetDate,
                    StartTime = habit.DefaultStartTime,
                    EndTime = habit.DefaultEndTime,
                    HabitId = habit.Id
                };
                _toDoItemRepository.Add(toDoItem);
            }
        }
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}