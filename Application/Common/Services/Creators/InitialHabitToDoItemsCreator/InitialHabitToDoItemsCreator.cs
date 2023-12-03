using Application.Habits.Calculations.HabitOccurrencesCalculator;
using Domain;
using Domain.Habit;
using Domain.ToDoItem;

namespace Application.Common.Services.Creators.InitialHabitToDoItemsCreator;

public sealed class InitialHabitToDoItemsCreator : IInitialHabitToDoItemsCreator
{
    private readonly IHabitOccurrencesCalculator _habitOccurrencesCalculator;
    private readonly IToDoItemRepository _toDoItemRepository;
    private readonly TimeProvider _timeProvider;
    private readonly IUnitOfWork _unitOfWork;

    public InitialHabitToDoItemsCreator(IHabitOccurrencesCalculator habitOccurrencesCalculator,
        IToDoItemRepository toDoItemRepository,
        TimeProvider timeProvider,
        IUnitOfWork unitOfWork)
    {
        _habitOccurrencesCalculator = habitOccurrencesCalculator;
        _toDoItemRepository = toDoItemRepository;
        _timeProvider = timeProvider;
        _unitOfWork = unitOfWork;
    }
    
    public async Task CreateInitialToDoItemsAsync(Habit habit, CancellationToken cancellationToken)
    {
        var utcNow = _timeProvider.GetUtcNow();
        var occurrences = _habitOccurrencesCalculator.GetHabitOccurrences(habit, utcNow);
        foreach (var occurrence in occurrences)
        {
            var toDoItem = new ToDoItem
            {
                Id = new ToDoItemId(Guid.NewGuid()),
                Description = habit.ToDoItemDescription,
                DueDate = occurrence,
                StartTime = habit.DefaultStartTime,
                EndTime = habit.DefaultEndTime,
                HabitId = habit.Id
            };
            _toDoItemRepository.Add(toDoItem);
        }
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}