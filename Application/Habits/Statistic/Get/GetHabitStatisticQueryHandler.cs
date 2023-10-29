using Application.Data;
using Domain.Habit;
using Domain.ToDoItem;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Habits.Statistic.Get;

public sealed class GetHabitStatisticQueryHandler : IRequestHandler<GetHabitStatisticQuery, GetHabitStatisticResponse>
{
    private readonly IApplicationContext _applicationContext;
    private readonly IToDoItemRepository _toDoItemRepository;
    private readonly IHabitRepository _habitRepository;

    public GetHabitStatisticQueryHandler(IToDoItemRepository toDoItemRepository,
        IHabitRepository habitRepository,
        IApplicationContext applicationContext)
    {
        _toDoItemRepository = toDoItemRepository;
        _habitRepository = habitRepository;
        _applicationContext = applicationContext;
    }
    
    public async Task<GetHabitStatisticResponse> Handle(GetHabitStatisticQuery request, CancellationToken cancellationToken)
    {
        var habitCompletionDates = await _applicationContext.HabitToDoItems
                .Include(habitToDoItem => habitToDoItem.ToDoItem)
                .Where(habitToDoItem => habitToDoItem.HabitId == request.HabitId && habitToDoItem.ToDoItem!.IsDone)
                .Select(habitToDoItem => habitToDoItem.ToDoItem!.DueDate)
                .ToListAsync(cancellationToken: cancellationToken);
        var getHabitStatisticResponse = new GetHabitStatisticResponse(habitCompletionDates);
        return getHabitStatisticResponse;
    }
}