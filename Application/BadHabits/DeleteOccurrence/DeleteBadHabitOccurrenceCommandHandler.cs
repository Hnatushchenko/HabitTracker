using Domain.BadHabit;
using MediatR;

namespace Application.BadHabits.DeleteOccurrence;

public class DeleteBadHabitOccurrenceCommandHandler : IRequestHandler<DeleteBadHabitOccurrenceCommand>
{
    private readonly IBadHabitRepository _badHabitRepository;
    
    public DeleteBadHabitOccurrenceCommandHandler(IBadHabitRepository badHabitRepository)
    {
        _badHabitRepository = badHabitRepository;
    }
    
    public async Task Handle(DeleteBadHabitOccurrenceCommand request, CancellationToken cancellationToken)
    {
        await _badHabitRepository.RemoveOccurrenceAsync(request.BadHabitId, request.OccurrenceDate, cancellationToken);
    }
}