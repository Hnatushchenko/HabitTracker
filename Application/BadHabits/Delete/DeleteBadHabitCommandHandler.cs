using Domain;
using Domain.BadHabit;
using MediatR;

namespace Application.BadHabits.Delete;

public sealed class DeleteBadHabitCommandHandler : IRequestHandler<DeleteBadHabitCommand>
{
    private readonly IBadHabitRepository _badHabitRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBadHabitCommandHandler(IBadHabitRepository badHabitRepository,
        IUnitOfWork unitOfWork)
    {
        _badHabitRepository = badHabitRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(DeleteBadHabitCommand request, CancellationToken cancellationToken)
    {
        var badHabit = await _badHabitRepository.GetById(request.BadHabitId, cancellationToken);
        if (badHabit is null)
        {
            throw new Exception("bad habit not found");
        }
        _badHabitRepository.Remove(badHabit);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}