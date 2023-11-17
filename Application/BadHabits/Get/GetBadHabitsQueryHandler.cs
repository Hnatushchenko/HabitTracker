using Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BadHabits.Get;

public sealed class GetBadHabitsQueryHandler : IRequestHandler<GetBadHabitsQuery, IEnumerable<BadHabitResponse>>
{
    private readonly IApplicationContext _applicationContext;

    public GetBadHabitsQueryHandler(IApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    
    public async Task<IEnumerable<BadHabitResponse>> Handle(GetBadHabitsQuery request, CancellationToken cancellationToken)
    {
        var badHabits = await _applicationContext.BadHabits
            .Include(badHabit => badHabit.Occurrences)
            .ToListAsync(cancellationToken);
        var badHabitResponseList = new List<BadHabitResponse>(badHabits.Count);
        foreach (var badHabit in badHabits)
        {
            var nowDate = DateTimeOffset.Now;
            var lastOccurrence = badHabit.Occurrences.Max();
            var lastOccurrenceDate = lastOccurrence?.OccurrenceDate ?? badHabit.StartDate;
            int streak = (nowDate - lastOccurrenceDate).Days;
            var badHabitResponse = new BadHabitResponse
            {
                Description = badHabit.Description,
                Streak = streak,
                BadHabitId = badHabit.Id.Value,
            };
            badHabitResponseList.Add(badHabitResponse);
        }
        
        return badHabitResponseList;
    }
}