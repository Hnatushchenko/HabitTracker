using Application.Interfaces.Creators;
using Domain.ToDoItem;
using MediatR;

namespace Application.ToDoItems.Get;

public sealed class GetToDoItemsQueryHandler : IRequestHandler<GetToDoItemsQuery, IEnumerable<ToDoItemResponse>>
{
    private readonly IHabitsBasedToDoItemsCreator _habitsBasedToDoItemsCreator;
    private readonly IToDoItemRepository _toDoItemRepository;

    public GetToDoItemsQueryHandler(IHabitsBasedToDoItemsCreator habitsBasedToDoItemsCreator,
        IToDoItemRepository toDoItemRepository)
    {
        _habitsBasedToDoItemsCreator = habitsBasedToDoItemsCreator;
        _toDoItemRepository = toDoItemRepository;
    }
    public async Task<IEnumerable<ToDoItemResponse>> Handle(GetToDoItemsQuery request, CancellationToken cancellationToken)
    {
        var targetDate = request.TargetDate;
        await _habitsBasedToDoItemsCreator.EnsureHabitsBasedToDoItemsCreatedAsync(targetDate, cancellationToken);
        var toDoItems = await _toDoItemRepository.GetByDueDateAndNotHiddenAsync(targetDate);
        var toDoItemResponseList = new List<ToDoItemResponse>(toDoItems.Count);
        foreach (var toDoItem in toDoItems)
        {
            var isToDoItemAssociatedWithHabit = toDoItem.HabitId is not null;
            var toDoItemResponse = new ToDoItemResponse(toDoItem.Id.Value,
                toDoItem.StartTime,
                toDoItem.EndTime,
                toDoItem.Description,
                toDoItem.IsDone,
                isToDoItemAssociatedWithHabit
            );
            toDoItemResponseList.Add(toDoItemResponse);
        }
        
        return toDoItemResponseList;
    }
}