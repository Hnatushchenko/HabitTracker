using Domain;
using Domain.Habit;
using Domain.Habit.Errors;
using Domain.HabitArchivedPeriodEntity;
using MediatR;
using OneOf.Types;

namespace Application.Habits.Unarchive;

public sealed class UnarchiveHabitCommandHandler : IRequestHandler<UnarchiveHabitCommand, SuccessOr<HabitIsNotArchivedError>>
{
    private readonly IHabitArchivedPeriodRepository _habitArchivedPeriodRepository;
    private readonly IHabitRepository _habitRepository;
    private readonly TimeProvider _timeProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;

    public UnarchiveHabitCommandHandler(IHabitArchivedPeriodRepository habitArchivedPeriodRepository,
        IHabitRepository habitRepository,
        TimeProvider timeProvider,
        IUnitOfWork unitOfWork, 
        IPublisher publisher)
    {
        _habitArchivedPeriodRepository = habitArchivedPeriodRepository;
        _habitRepository = habitRepository;
        _timeProvider = timeProvider;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }
    
    public async Task<SuccessOr<HabitIsNotArchivedError>> Handle(UnarchiveHabitCommand request, CancellationToken cancellationToken)
    {
        var habit = await _habitRepository.GetByIdAsync(request.HabitId, cancellationToken);
        if (!habit.IsArchived)
        {
            return new HabitIsNotArchivedError(request.HabitId);
        }
        habit.IsArchived = false;
        await CompleteLastHabitArchivedPeriod(habit, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await PublishHabitUnarchivedNotification();
        return new Success();
    }

    private async Task CompleteLastHabitArchivedPeriod(IHabit habit, CancellationToken cancellationToken)
    {
        var unfinishedPeriod =
            await _habitArchivedPeriodRepository.GetUnfinishedPeriodByHabitIdAsync(habit.Id, cancellationToken);
        var utcNow = _timeProvider.GetUtcNow();
        unfinishedPeriod.EndDate = utcNow;
    }

    private async Task PublishHabitUnarchivedNotification()
    {
        var notification = new HabitUnarchivedNotification
        {
            UnarchivingDate = _timeProvider.GetUtcNow()
        };
        await _publisher.Publish(notification);
    }
}
