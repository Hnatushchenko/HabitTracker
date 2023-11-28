using Application.Data;
using Domain.BadHabit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BadHabits.GetStatistic.GetBadHabitStatistic;

public sealed class GetBadHabitStatisticQueryHandler : IRequestHandler<GetBadHabitStatisticQuery, GetBadHabitStatisticResponse>
{
    private readonly IApplicationContext _applicationContext;
    private readonly IBadHabitRepository _badHabitRepository;

    public GetBadHabitStatisticQueryHandler(IApplicationContext applicationContext,
        IBadHabitRepository badHabitRepository)
    {
        _applicationContext = applicationContext;
        _badHabitRepository = badHabitRepository;
    }
    
    public async Task<GetBadHabitStatisticResponse> Handle(GetBadHabitStatisticQuery request, CancellationToken cancellationToken)
    {
        var badHabit = await _badHabitRepository.GetByIdAsync(request.BadHabitId, cancellationToken);
        var occurrences = await _applicationContext.BadHabitOccurrences
            .Where(badHabitOccurrence => badHabitOccurrence.BadHabitId == request.BadHabitId)
            .Select(badHabitOccurrence => badHabitOccurrence.OccurrenceDate)
            .ToListAsync(cancellationToken);
        
        var getBadHabitStatisticResponse = new GetBadHabitStatisticResponse
        {
            StartDate = badHabit.StartDate,
            Occurrences = occurrences,
        };
        return getBadHabitStatisticResponse;
    }
}