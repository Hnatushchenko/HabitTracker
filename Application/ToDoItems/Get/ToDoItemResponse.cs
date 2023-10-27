using Domain.ToDoItem;

namespace Application.ToDoItems.Get;

public sealed record ToDoItemResponse(Guid Id,
    TimeOnly StartTime,
    TimeOnly EndTime,
    string Description,
    bool IsDone);