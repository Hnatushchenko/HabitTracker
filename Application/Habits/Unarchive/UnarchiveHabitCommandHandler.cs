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

    public UnarchiveHabitCommandHandler(IHabitArchivedPeriodRepository habitArchivedPeriodRepository,
        IHabitRepository habitRepository,
        TimeProvider timeProvider,
        IUnitOfWork unitOfWork)
    {
        _habitArchivedPeriodRepository = habitArchivedPeriodRepository;
        _habitRepository = habitRepository;
        _timeProvider = timeProvider;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<SuccessOr<HabitIsNotArchivedError>> Handle(UnarchiveHabitCommand request, CancellationToken cancellationToken)
    {
        var utcNow = _timeProvider.GetUtcNow();
        var habit = await _habitRepository.GetByIdAsync(request.HabitId, cancellationToken);
        if (!habit.IsArchived)
        {
            return new HabitIsNotArchivedError(request.HabitId);
        }
        habit.IsArchived = false;
        var unfinishedPeriod = await _habitArchivedPeriodRepository.GetUnfinishedPeriodByHabitIdAsync(habit.Id, cancellationToken);
        unfinishedPeriod.EndDate = utcNow;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new Success();
    }
}
