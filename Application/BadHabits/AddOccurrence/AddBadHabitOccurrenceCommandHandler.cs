using Domain;
using Domain.BadHabit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf.Types;

namespace Application.BadHabits.AddOccurrence;

public sealed class AddBadHabitOccurrenceCommandHandler : IRequestHandler<AddBadHabitOccurrenceCommand, SuccessOrDuplicateBadHabitOccurrenceError>
{
    private readonly IBadHabitRepository _badHabitRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public AddBadHabitOccurrenceCommandHandler(IBadHabitRepository badHabitRepository,
        IUnitOfWork unitOfWork)
    {
        _badHabitRepository = badHabitRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<SuccessOrDuplicateBadHabitOccurrenceError> Handle(AddBadHabitOccurrenceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _badHabitRepository.AddOccurrence(request.BadHabitId, request.OccurrenceDate);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new Success();
        }
        catch (DbUpdateException)
        {
            return DuplicateBadHabitOccurrenceError.Instance;
        }
    }
}