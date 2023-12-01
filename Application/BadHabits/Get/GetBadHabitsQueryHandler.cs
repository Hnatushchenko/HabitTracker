using Application.Data;
using Domain.BadHabit;
using Helpers.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BadHabits.Get;

public sealed class GetBadHabitsQueryHandler : IRequestHandler<GetBadHabitsQuery, IEnumerable<BadHabitResponse>>
{
    private readonly IApplicationContext _applicationContext;
    private readonly TimeProvider _timeProvider;

    public GetBadHabitsQueryHandler(IApplicationContext applicationContext,
        TimeProvider timeProvider)
    {
        _applicationContext = applicationContext;
        _timeProvider = timeProvider;
    }
    
    public async Task<IEnumerable<BadHabitResponse>> Handle(GetBadHabitsQuery request, CancellationToken cancellationToken)
    {
        var badHabits = await _applicationContext.BadHabits
            .Include(badHabit => badHabit.Occurrences)
            .ToListAsync(cancellationToken);
        var badHabitResponseList = new BadHabitResponse[badHabits.Count];
        for (var i = 0; i < badHabits.Count; i++)
        {
            var badHabit = badHabits[i];
            var nowDate = _timeProvider.GetUtcNow().Date;
            var startDateForStreakCalculation = GetStartDateForStreakCalculation(badHabit);
            int streak = _timeProvider.GetUtcNow().HasUtcDateEqualTo(startDateForStreakCalculation) ? 0 : (nowDate - startDateForStreakCalculation).Days + 1;
            var badHabitResponse = new BadHabitResponse
            {
                Description = badHabit.Description,
                Streak = streak,
                BadHabitId = badHabit.Id.Value
            };
            badHabitResponseList[i] = badHabitResponse;
        }

        return badHabitResponseList;
    }

    private static DateTimeOffset GetStartDateForStreakCalculation(BadHabit badHabit)
    {
        var startDateForStreakCalculation = badHabit.Occurrences.Count == 0 ?
            badHabit.StartDate :
            badHabit.Occurrences.Max(occurrence => occurrence.OccurrenceDate);
        return startDateForStreakCalculation;
    }
}