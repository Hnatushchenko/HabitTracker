using Domain;
using Domain.Habit;
using Domain.Habit.Errors;
using Domain.HabitArchivedPeriodEntity;
using Domain.HabitArchivedPeriodEntity.ValueObjects;
using Domain.ToDoItem;
using Helpers.Extensions;
using MediatR;
using OneOf.Types;

namespace Application.Habits.Archive;

public sealed class ArchiveHabitCommandHandler : IRequestHandler<ArchiveHabitCommand, SuccessOr<HabitIsAlreadyArchivedError>>
{
    private readonly IHabitArchivedPeriodRepository _habitArchivedPeriodRepository;
    private readonly IToDoItemRepository _toDoItemRepository;
    private readonly IHabitRepository _habitRepository;
    private readonly TimeProvider _timeProvider;
    private readonly IUnitOfWork _unitOfWork;

    public ArchiveHabitCommandHandler(IToDoItemRepository toDoItemRepository,
        IHabitRepository habitRepository,
        TimeProvider timeProvider,
        IUnitOfWork unitOfWork, IHabitArchivedPeriodRepository habitArchivedPeriodRepository)
    {
        _toDoItemRepository = toDoItemRepository;
        _habitRepository = habitRepository;
        _timeProvider = timeProvider;
        _unitOfWork = unitOfWork;
        _habitArchivedPeriodRepository = habitArchivedPeriodRepository;
    }
    
    public async Task<SuccessOr<HabitIsAlreadyArchivedError>> Handle(ArchiveHabitCommand request, CancellationToken cancellationToken)
    {
        var utcNow = _timeProvider.GetUtcNow();
        var habit = await _habitRepository.GetHabitByIdWithToDoItemsIncludedAsync(request.HabitId, cancellationToken);
        if (habit.IsArchived)
        {
            return new HabitIsAlreadyArchivedError(request.HabitId);
        }
        ArchiveHabit(habit, utcNow);
        RemoveToDoItemsThatWereGeneratedInAdvance(habit, utcNow);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new Success();
    }

    private void ArchiveHabit(IHabit habit, DateTimeOffset utcNow)
    {
        habit.IsArchived = true;
        var habitArchivedPeriod = new HabitArchivedPeriod
        {
            Id = new HabitArchivedPeriodId(Guid.NewGuid()),
            StartDate = utcNow,
            HabitId = habit.Id
        };
        _habitArchivedPeriodRepository.Add(habitArchivedPeriod);
    }

    private void RemoveToDoItemsThatWereGeneratedInAdvance(IHabitWithToDoItems habit, DateTimeOffset utcNow)
    {
        var toDoItemsToRemove = habit.ToDoItems.Where(toDoItem => 
            toDoItem.DueDate.HasUtcDateGreaterThanOrEqualTo(utcNow) && !toDoItem.IsDone);
        _toDoItemRepository.RemoveRange(toDoItemsToRemove);
    }
}