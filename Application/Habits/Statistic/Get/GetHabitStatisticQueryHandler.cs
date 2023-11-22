using Application.Data;
using Application.Extensions;
using Domain.Habit;
using Domain.ToDoItem;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Habits.Statistic.Get;

public sealed class GetHabitStatisticQueryHandler : IRequestHandler<GetHabitStatisticQuery, GetHabitStatisticResponse>
{
    private readonly IApplicationContext _applicationContext;

    public GetHabitStatisticQueryHandler(IApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
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
        var utcNow = DateTimeOffset.Now;
        var filteredDateBasedHabitStatuses = new List<DateBasedHabitStatus>();
        await foreach (var dateBasedHabitStatus in allDateBasedHabitStatuses)
        {
            if (dateBasedHabitStatus.Date.HasUtcDateLessThen(utcNow) || 
                (dateBasedHabitStatus.Date.HasUtcDateEqualTo(utcNow) && dateBasedHabitStatus.IsCompleted))
            {
                filteredDateBasedHabitStatuses.Add(dateBasedHabitStatus);
            }
        }
        var getHabitStatisticResponse = new GetHabitStatisticResponse(filteredDateBasedHabitStatuses);
        return getHabitStatisticResponse;
    }
}