using Domain;
using Domain.Habit;
using Domain.OneOfTypes;
using MediatR;
using OneOf.Types;

namespace Application.Habits.Archive;

public sealed class ArchiveHabitCommandHandler : IRequestHandler<ArchiveHabitCommand, SuccessOrNotFound>
{
    private readonly IHabitRepository _habitRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ArchiveHabitCommandHandler(IHabitRepository habitRepository,
        IUnitOfWork unitOfWork)
    {
        _habitRepository = habitRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<SuccessOrNotFound> Handle(ArchiveHabitCommand request, CancellationToken cancellationToken)
    {
        var getHabitResult = await _habitRepository.GetByIdDeprecatedAsync(request.HabitId);
        if (!getHabitResult.TryPickT0(out var habit, out var notFound)) return notFound;
        habit.IsArchived = true;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new Success();
    }
}