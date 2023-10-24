using Domain.ToDoItem;

namespace Application.ToDoItems.Get;

public sealed record ToDoItemResponse(ToDoItemId Id,
    TimeOnly StartTime,
    TimeOnly EndTime,
    string Description,
    bool IsDone);