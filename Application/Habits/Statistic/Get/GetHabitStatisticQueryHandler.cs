using Application.Data;
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
        var allDateBasedHabitStatuses = _applicationContext.HabitToDoItems
            .Include(habitToDoItem => habitToDoItem.ToDoItem)
            .Where(habitToDoItem => habitToDoItem.HabitId == request.HabitId)
            .Select(habitToDoItem => new DateBasedHabitStatus
            {
                Date = habitToDoItem.ToDoItem!.DueDate,
                IsCompleted = habitToDoItem.ToDoItem.IsDone
            })
            .AsAsyncEnumerable()
            .WithCancellation(cancellationToken);
        var filteredDateBasedHabitStatuses = new List<DateBasedHabitStatus>();
        await foreach (var dateBasedHabitStatus in allDateBasedHabitStatuses)
        {
            if (dateBasedHabitStatus.Date.UtcDateTime.Date <= DateTimeOffset.UtcNow.Date)
            {
                filteredDateBasedHabitStatuses.Add(dateBasedHabitStatus);
            }
        }
        var getHabitStatisticResponse = new GetHabitStatisticResponse(filteredDateBasedHabitStatuses);
        return getHabitStatisticResponse;
    }
}