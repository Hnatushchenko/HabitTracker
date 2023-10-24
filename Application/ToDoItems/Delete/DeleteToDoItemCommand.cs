using Domain.OneOfTypes;
using Domain.ToDoItem;
using MediatR;

namespace Application.ToDoItems.Delete;

public sealed record DeleteToDoItemCommand(ToDoItemId ToDoItemId) : IRequest<DeletedOrNotFound>;