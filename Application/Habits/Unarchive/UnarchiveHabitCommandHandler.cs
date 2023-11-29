using Application.Habits.Archive;
using Domain;
using Domain.Habit;
using MediatR;

namespace Application.Habits.Unarchive;

public sealed class UnarchiveHabitCommandHandler : IRequestHandler<UnarchiveHabitCommand>
{
    private readonly IHabitRepository _habitRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UnarchiveHabitCommandHandler(IHabitRepository habitRepository,
        IUnitOfWork unitOfWork)
    {
        _habitRepository = habitRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(UnarchiveHabitCommand request, CancellationToken cancellationToken)
    {
        var habit = await _habitRepository.GetByIdAsync(request.HabitId, cancellationToken);
        habit.IsArchived = false;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
