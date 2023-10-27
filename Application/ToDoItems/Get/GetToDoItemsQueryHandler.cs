using Application.Habits.Get;
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
        var toDoItems = await _toDoItemRepository.GetByDueDateAsync(targetDate);
        var toDoItemResponseList = toDoItems.Select(toDoItem => new ToDoItemResponse(toDoItem.Id.Value,
            toDoItem.StartTime,
            toDoItem.EndTime,
            toDoItem.Description,
            toDoItem.IsDone));
        return toDoItemResponseList;
    }
}