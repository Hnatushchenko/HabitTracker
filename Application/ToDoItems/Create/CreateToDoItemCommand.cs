using MediatR;

namespace Application.ToDoItems.Create;

public sealed record CreateToDoItemCommand(string Description,
    TimeOnly StartTime,
    TimeOnly EndTime) : IRequest;