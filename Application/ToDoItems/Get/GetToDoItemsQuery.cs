using Domain.ToDoItem;
using MediatR;

namespace Application.ToDoItems.Get;

public sealed class GetToDoItemsQuery : IRequest<IEnumerable<ToDoItemResponse>>
{}