using Application.Habits.Get;
using Domain.ToDoItem;
using MediatR;

namespace Application.ToDoItems.Get;

public class GetToDoItemsQueryHandler : IRequestHandler<GetToDoItemsQuery, IEnumerable<ToDoItemResponse>>
{
    private readonly IToDoItemRepository _toDoItemRepository;

    public GetToDoItemsQueryHandler(IToDoItemRepository toDoItemRepository)
    {
        _toDoItemRepository = toDoItemRepository;
    }
    public async Task<IEnumerable<ToDoItemResponse>> Handle(GetToDoItemsQuery request, CancellationToken cancellationToken)
    {
        var toDoItems = await _toDoItemRepository.GetAllAsync();
        var toDoItemResponseList = toDoItems.Select(toDoItem => new ToDoItemResponse(toDoItem.Id,
            toDoItem.StartTime,
            toDoItem.EndTime,
            toDoItem.Description,
            toDoItem.IsDone));
        return toDoItemResponseList;
    }
}