using MediatR;

namespace Application.ToDoItems.Get;

public sealed record GetToDoItemsQuery : IRequest<ToDoItemsResponse>
{
    /// <summary>
    /// Gets or sets the date for which ToDoItems are queried.
    /// This date is used to retrieve ToDoItems planned for a specific date.
    /// </summary>
    public required DateTimeOffset TargetDate { get; init; }
}