using Domain;
using Domain.Habit;
using Domain.ToDoItem;
using Helpers.Extensions;
using MediatR;

namespace Application.Habits.Archive;

public sealed class ArchiveHabitCommandHandler : IRequestHandler<ArchiveHabitCommand>
{
    private readonly IToDoItemRepository _toDoItemRepository;
    private readonly IHabitRepository _habitRepository;
    private readonly TimeProvider _timeProvider;
    private readonly IUnitOfWork _unitOfWork;

    public ArchiveHabitCommandHandler(IToDoItemRepository toDoItemRepository,
        IHabitRepository habitRepository,
        TimeProvider timeProvider,
        IUnitOfWork unitOfWork)
    {
        _toDoItemRepository = toDoItemRepository;
        _habitRepository = habitRepository;
        _timeProvider = timeProvider;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(ArchiveHabitCommand request, CancellationToken cancellationToken)
    {
        var habit = await _habitRepository.GetHabitByIdWithToDoItemsIncludedAsync(request.HabitId, cancellationToken);
        habit.IsArchived = true;
        RemoveToDoItemsThatWereGeneratedInAdvance(habit);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
    
    private void RemoveToDoItemsThatWereGeneratedInAdvance(IHabitWithToDoItems habit)
    {
        var utcNow = _timeProvider.GetUtcNow();
        var toDoItemsToRemove = habit.ToDoItems.Where(toDoItem => 
            toDoItem.DueDate.HasUtcDateGreaterThanOrEqualTo(utcNow) && !toDoItem.IsDone);
        _toDoItemRepository.RemoveRange(toDoItemsToRemove);
    }
}