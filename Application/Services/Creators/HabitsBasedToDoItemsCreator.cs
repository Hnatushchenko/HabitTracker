using Application.Interfaces.Creators;
using Domain;
using Domain.Habit;
using Domain.ToDoItem;

namespace Application.Services.Creators;


public class HabitsBasedToDoItemsCreator : IHabitsBasedToDoItemsCreator
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
        var habits = await _habitRepository.GetByTargetDateAsync(targetDate);
        var toDoItems = await _toDoItemRepository.GetByDueDateWithIncludedHabit(targetDate);
        foreach (var habit in habits)
        {
            if (toDoItems.All(toDoItem => toDoItem.Habit!.Id != habit.Id))
            {
                var toDoItem = new ToDoItem()
                {
                    Id = ToDoItemId.From(Guid.NewGuid()),
                    Description = habit.Description,
                    DueDate = targetDate,
                    Habit = habit,
                    StartTime = TimeOnly.MinValue,
                    EndTime = TimeOnly.MinValue,
                };
                _toDoItemRepository.Add(toDoItem);
            }
        }
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}