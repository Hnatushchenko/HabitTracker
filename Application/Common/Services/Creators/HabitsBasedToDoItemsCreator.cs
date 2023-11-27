using Application.Common.Interfaces.Creators;
using Domain;
using Domain.Habit;
using Domain.ToDoItem;

namespace Application.Common.Services.Creators;

public sealed class HabitsBasedToDoItemsCreator : IHabitsBasedToDoItemsCreator
{
    private readonly IToDoItemRepository _toDoItemRepository;
    private readonly IHabitRepository _habitRepository;
    private readonly IUnitOfWork _unitOfWork;

    public HabitsBasedToDoItemsCreator(IToDoItemRepository toDoItemRepository,
        IHabitRepository habitRepository,
        IUnitOfWork unitOfWork)
    {
        _habitRepository = habitRepository;
        _toDoItemRepository = toDoItemRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task EnsureHabitsBasedToDoItemsCreatedAsync(DateTimeOffset targetDate, CancellationToken cancellationToken)
    {
        var activeHabits = await _habitRepository.GetActiveHabitsByTargetDateAsync(targetDate);
        var toDoItems = await _toDoItemRepository.GetByDueDateWithIncludedHabitAsync(targetDate, cancellationToken);
        foreach (var habit in activeHabits)
        {
            if (toDoItems.All(toDoItem => toDoItem.HabitId != habit.Id))
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