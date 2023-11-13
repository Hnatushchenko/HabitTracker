using Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BadHabits.GetStatistic.GetBadHabitStatistic;

public sealed class GetBadHabitStatisticQueryHandler : IRequestHandler<GetBadHabitStatisticQuery, GetBadHabitStatisticResponse>
{
    private readonly IApplicationContext _applicationContext;

    public GetBadHabitStatisticQueryHandler(IApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    
    public async Task<GetBadHabitStatisticResponse> Handle(GetBadHabitStatisticQuery request, CancellationToken cancellationToken)
    {
        var badHabit = await _applicationContext.BadHabits.FindAsync(request.BadHabitId, cancellationToken);
        if (badHabit is null)
        {
            throw new Exception("Bad habit is not found");
        }
        var occurrences = await _applicationContext.BadHabitOccurrences
            .Where(badHabitOccurrence => badHabitOccurrence.BadHabitId == request.BadHabitId)
            .Select(badHabitOccurrence => badHabitOccurrence.OccurrenceDate)
            .ToListAsync(cancellationToken);
        var getBadHabitStatisticResponse = new GetBadHabitStatisticResponse()
        {
            StartDate = badHabit.StartDate,
            Occurrences = occurrences,
        };
        return getBadHabitStatisticResponse;
    }
}