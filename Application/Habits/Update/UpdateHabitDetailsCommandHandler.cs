using Domain;
using Domain.Habit;
using Domain.ToDoItem;
using Helpers.Extensions;
using MediatR;

namespace Application.Habits.Update;

public sealed class UpdateHabitDetailsCommandHandler : IRequestHandler<UpdateHabitDetailsCommand>
{
    private readonly IToDoItemRepository _toDoItemRepository;
    private readonly IHabitRepository _habitRepository;
    private readonly TimeProvider _timeProvider;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateHabitDetailsCommandHandler(IToDoItemRepository toDoItemRepository, 
        IHabitRepository habitRepository,
        TimeProvider timeProvider,
        IUnitOfWork unitOfWork)
    {
        _toDoItemRepository = toDoItemRepository;
        _habitRepository = habitRepository;
        _timeProvider = timeProvider;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(UpdateHabitDetailsCommand request, CancellationToken cancellationToken)
    {
        var habit = await _habitRepository.GetHabitByIdWithToDoItemsIncludedAsync(request.HabitId, cancellationToken);
        habit.ToDoItemDescription = request.ToDoItemDescription;
        habit.DefaultStartTime = request.DefaultStartTime;
        habit.DefaultEndTime = request.DefaultEndTime;
        habit.Description = request.Description;
        var utcNow = _timeProvider.GetUtcNow();
        foreach (var toDoItem in habit.ToDoItems)
        {
            if (!toDoItem.DueDate.HasUtcDateLessThen(utcNow))
            {
                toDoItem.Description = request.ToDoItemDescription;
                toDoItem.StartTime = request.DefaultStartTime;
                toDoItem.EndTime = request.DefaultEndTime;
            }
        }
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}