using Domain.ToDoItem;
using MediatR;

namespace Application.ToDoItems.Swap;

public sealed record SwapToDoItemsRequest : IRequest
{
    public required Guid FirstToDoItemId { get; init; }
    public required Guid SecondToDoItemId { get; init; }
}