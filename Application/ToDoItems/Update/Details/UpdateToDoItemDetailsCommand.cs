using Domain.ToDoItem;
using MediatR;

namespace Application.ToDoItems.Update.Details;

public sealed record UpdateToDoItemDetailsCommand(ToDoItemId ToDoItemId,
    TimeOnly StartTime,
    TimeOnly EndTime,
    string Description) : IRequest;