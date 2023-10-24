using Domain;
using Domain.Habit;
using Domain.OneOfTypes;
using Domain.Primitives;
using MediatR;

namespace Application.Habits.Delete;

public sealed class DeleteHabitCommandHandler : IRequestHandler<DeleteHabitCommand, DeletedOrNotFound>
{
    private readonly IHabitRepository _habitRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteHabitCommandHandler(IHabitRepository habitRepository,
        IUnitOfWork unitOfWork)
    {
        _habitRepository = habitRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<DeletedOrNotFound> Handle(DeleteHabitCommand request, CancellationToken cancellationToken)
    {
        var queryResult = await _habitRepository.GetByIdAsync(request.HabitId);
        if (!queryResult.TryPickT0(out var habit, out var notFound)) return notFound;
        _habitRepository.Remove(habit);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new Deleted();
    }
}