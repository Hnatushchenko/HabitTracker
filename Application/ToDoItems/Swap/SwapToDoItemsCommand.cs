using Domain.ToDoItem;
using MediatR;

namespace Application.ToDoItems.Swap;

public sealed record SwapToDoItemsCommand : IRequest
{
    public required ToDoItemId FirstToDoItemId { get; init; }
    public required ToDoItemId SecondToDoItemId { get; init; }
}