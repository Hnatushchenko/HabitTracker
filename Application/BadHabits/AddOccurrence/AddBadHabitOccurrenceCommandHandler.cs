using Domain;
using Domain.BadHabit;
using MediatR;

namespace Application.BadHabits.AddOccurrence;

public class AddBadHabitOccurrenceCommandHandler : IRequestHandler<AddBadHabitOccurrenceCommand>
{
    private readonly IBadHabitRepository _badHabitRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public AddBadHabitOccurrenceCommandHandler(IBadHabitRepository badHabitRepository,
        IUnitOfWork unitOfWork)
    {
        _badHabitRepository = badHabitRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(AddBadHabitOccurrenceCommand request, CancellationToken cancellationToken)
    {
        _badHabitRepository.AddOccurrence(request.BadHabitId, request.OccurrenceDate);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}