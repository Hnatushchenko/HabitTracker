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
        var badHabits = await _applicationContext.BadHabits.Select(badHabit =>
            new BadHabitResponse
            {
                BadHabitId = badHabit.Id.Value,
                Description = badHabit.Description
            }
        ).ToListAsync(cancellationToken);
        return badHabits;
    }
}