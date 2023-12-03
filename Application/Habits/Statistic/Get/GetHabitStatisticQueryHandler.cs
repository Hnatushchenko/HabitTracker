using Application.Data;
using Helpers.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Habits.Statistic.Get;

public sealed class GetHabitStatisticQueryHandler : IRequestHandler<GetHabitStatisticQuery, GetHabitStatisticResponse>
{
    private readonly IApplicationContext _applicationContext;
    private readonly TimeProvider _timeProvider;


    public GetHabitStatisticQueryHandler(IApplicationContext applicationContext, TimeProvider timeProvider)
    {
        _applicationContext = applicationContext;
        _timeProvider = timeProvider;
    }
    
    public async Task<GetHabitStatisticResponse> Handle(GetHabitStatisticQuery request, CancellationToken cancellationToken)
    {
        var allDateBasedHabitStatuses = _applicationContext.ToDoItems
            .Where(toDoItem => toDoItem.HabitId == request.HabitId)
            .Select(toDoItem => new DateBasedHabitStatus
            {
                Date = toDoItem.DueDate,
                IsCompleted = toDoItem.IsDone
            })
            .AsAsyncEnumerable()
            .WithCancellation(cancellationToken);
        var utcNow = _timeProvider.GetUtcNow();
        var filteredDateBasedHabitStatuses = new List<DateBasedHabitStatus>();
        await foreach (var dateBasedHabitStatus in allDateBasedHabitStatuses)
        {
            if (dateBasedHabitStatus.Date.HasUtcDateLessThan(utcNow) || 
                (dateBasedHabitStatus.Date.HasUtcDateEqualTo(utcNow) && dateBasedHabitStatus.IsCompleted))
            {
                filteredDateBasedHabitStatuses.Add(dateBasedHabitStatus);
            }
        }
        var getHabitStatisticResponse = new GetHabitStatisticResponse(filteredDateBasedHabitStatuses);
        return getHabitStatisticResponse;
    }
}